namespace BlueBox.FileMeta.Impl
{
    using BlueBox.FileMeta.Api;
    using BlueBox.FileMeta.Dto;
    using Dapper;
    using System;
    using System.Data;

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
        }

        /// <inheritxmldoc/>
        public File Get(int fileId, IDbConnection connection)
        {
            return connection.QueryFirst<File>(
                string.Join(
                    "\n",
                    "select * ",
                    "from file join part on part.FileId = file.Id ",
                    "where file.id = @Id;"
                ),
                new {
                    @Id = fileId
                }
            );
        }
    }
}
