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

            var fileNextId = NextId(
                "file",
                connection, 
                transaction
            );

            file.Id = fileNextId;

            connection.Execute(
                @"insert into file (Id) values (@Id);",
                file,
                transaction
            );

            UpdateNextId(
                fileNextId + 1,
                "file",
                connection, 
                transaction
            );

            foreach (var part in file.Parts) {
                var partNextId = NextId(
                    "part",
                    connection,
                    transaction
                );

                connection.Execute(
                    @"insert into part (Id, FileId, BlockId) values (@Id, @FileId, @BlockId)",
                    new {
                        Id = partNextId,
                        FileId = fileNextId,
                        BlockId = part.BlockId
                    }
                );

                UpdateNextId(
                    partNextId + 1,
                    "part",
                    connection, 
                    transaction
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

        /// <inheritdoc/>
        private void UpdateNextId(int nextId, string tableName, IDbConnection connection, IDbTransaction transaction)
        {
            connection.Execute(
                "update counters set next_id = @NextId where table_name = @TableName",
                new {
                    NextId = nextId,
                    TableName = tableName
                }
            );
        }

        /// <inheritdoc/>
        private int NextId(string tableName, IDbConnection connection, IDbTransaction transaction) 
        {
            return connection.QueryFirst<int>(
                "select next_id from counters where table_name = @TableName",
                new {
                    TableName = tableName
                }
            );
        }
    }
}
