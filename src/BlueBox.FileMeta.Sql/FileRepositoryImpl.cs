namespace BlueBox.FileMeta.Sql
{
    using BlueBox.FileMeta.Sql;
    using BlueBox.FileMeta.Dto;
    using Dapper;
    using System;
    using System.Data;
    using System.Linq;

    /// <inheritxmldoc/>
    public class FileRepositoryImpl : IFileRepository
    {
        private readonly IFileMetaDbFactory fileMetaDbFactory;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileMetaDbFactory"></param>
        public FileRepositoryImpl(
            IFileMetaDbFactory fileMetaDbFactory
        ) {
            this.fileMetaDbFactory = fileMetaDbFactory;
        }

        /// <inheritxmldoc/>
        public void Create(File file)
        {
            using (var connection = fileMetaDbFactory.CreateConnection()) 
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction()) 
                {
                    try {
                        Create(file, connection, transaction);

                        transaction.Commit();
                    } catch (Exception e) {
                        transaction.Rollback();

                        throw e;
                    }
                }
            }
        }

        /// <inheritxmldoc/>
        public void Create(File file, IDbConnection connection, IDbTransaction transaction)
        {
            if (file == null)
            {
                throw new ArgumentException("File cannot be null");
            }

            if (transaction == null) {
                throw new ArgumentException("Transaction cannot be null");
            }

            connection.Execute(
                @"insert into file (Id) values (@Id);",
                file,
                transaction
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
        public File Get(int fileId)
        {
            using (var connection = fileMetaDbFactory.CreateConnection()) 
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction()) 
                {
                    try {
                        var resultfile = Get(fileId, connection, transaction);

                        transaction.Commit();

                        return resultfile;
                    } catch (Exception e) {
                        transaction.Rollback();

                        throw e;
                    }
                }
            }
        }

        /// <inheritxmldoc/>
        public File Get(int fileId, IDbConnection connection, IDbTransaction transaction)
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
                },
                transaction
            );

            return resultFile;
        }
    }
}
