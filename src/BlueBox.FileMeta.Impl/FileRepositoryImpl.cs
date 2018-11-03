namespace BlueBox.FileMeta.Impl
{
    using BlueBox.FileMeta.Api;
    using BlueBox.FileMeta.Dto;
    using Dapper;
    using System;
    using System.Data;
    using System.Linq;

    /// <inheritxmldoc/>
    public class FileRepositoryImpl : IFileRepository
    {

        /// <inheritxmldoc/>
        public void Create(File file, IDbConnection connection)
        {
            if (file == null)
            {
                throw new ArgumentException("File cannot be null");
            }

            connection.Execute(
                @"insert into file (Id) values (@Id);",
                file
            );

            foreach (var part in file.Parts) {
                connection.Execute(
                    @"insert into part (Id, FileId, BlockId) values (@Id, @FileId, @BlockId)",
                    new {
                        Id = part.Id,
                        FileId = file.Id,
                        BlockId = part.BlockId
                    }
                );
            }
        }

        /// <inheritxmldoc/>
        public File Get(int fileId, IDbConnection connection)
        {
            File resultFile = null;
            
            connection.Query<File, Part, File>(
                string.Join(
                    "\n",
                    "select * ",
                    "from file join part on part.FileId = file.Id ",
                    "where file.id = @Id;"
                ),
                (file, part) => {
                    if (resultFile == null) {
                        resultFile = file;
                    }

                    resultFile.Parts.Add(part);

                    return file;
                },
                new {
                    @Id = fileId
                }
            );

            return resultFile;
        }
    }
}
