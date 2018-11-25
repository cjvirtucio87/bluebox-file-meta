namespace BlueBox.FileMeta.Sql
{
    using BlueBox.FileMeta.Sql;
    using BlueBox.FileMeta.Dto;
    using Dapper;
    using System;
    using System.Data;
    using System.Linq;
    using System.Collections.Generic;

    /// <inheritxmldoc/>
    public class FileRegistryImpl : IFileRegistry
    {
        private readonly IFileMetaDbFactory fileMetaDbFactory;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileMetaDbFactory"></param>
        public FileRegistryImpl(
            IFileMetaDbFactory fileMetaDbFactory
        ) {
            this.fileMetaDbFactory = fileMetaDbFactory;
        }

        /// <inheritdoc/>
        public Dto.File LastFile()
        {
            using (var connection = fileMetaDbFactory.CreateConnection()) 
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var lastFile = LastFile(
                        connection,
                        transaction
                    );

                    transaction.Commit();

                    return lastFile;
                }
            }
        }

        /// <inheritdoc/>
        public Dto.File LastFile(IDbConnection connection, IDbTransaction transaction)
        {
            return connection.QueryFirst<Dto.File>(
                "select * from file order by id desc"
            );
        }

        /// <inheritxmldoc/>
        public void RegisterFile(File file)
        {
            using (var connection = fileMetaDbFactory.CreateConnection()) 
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction()) 
                {
                    try {
                        RegisterFile(file, connection, transaction);

                        transaction.Commit();
                    } catch (Exception e) {
                        transaction.Rollback();

                        throw e;
                    }
                }
            }
        }

        /// <inheritxmldoc/>
        public void RegisterFile(File file, IDbConnection connection, IDbTransaction transaction)
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

            connection.Execute(
                @"insert into file (Id) values (@Id);",
                new {
                    Id = fileNextId
                },
                transaction
            );

            UpdateNextId(
                fileNextId + 1,
                "file",
                connection, 
                transaction
            );
        }

        /// <inheritdoc/>
        public void RegisterParts(int fileId, IEnumerable<Part> parts)
        {
            using (var connection = fileMetaDbFactory.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    transaction.Commit();
                }
            }
        }

        /// <inheritdoc/>
        public void RegisterParts(int fileId, IEnumerable<Part> parts, IDbConnection connection, IDbTransaction transaction)
        {
            foreach (var part in parts) {
                var partNextId = NextId(
                    "part",
                    connection,
                    transaction
                );

                connection.Execute(
                    @"insert into part (Id, FileId, BlockId) values (@Id, @FileId, @BlockId)",
                    new {
                        Id = partNextId,
                        FileId = fileId,
                        BlockId = part.BlockId
                    },
                    transaction
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
        public File GetFile(int fileId)
        {
            using (var connection = fileMetaDbFactory.CreateConnection()) 
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction()) 
                {
                    try {
                        var resultfile = GetFile(fileId, connection, transaction);

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
        public File GetFile(int fileId, IDbConnection connection, IDbTransaction transaction)
        {
            File resultFile = null;
            
            connection.Query<File, Part, File>(@"
                select
                    *
                from 
                    file join part on part.FileId = file.Id
                where 
                    file.id = @Id
                ",
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
