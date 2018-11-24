namespace BlueBox.FileMeta.Service
{
    using BlueBox.FileMeta.Sql;
    using BlueBox.FileMeta.Dto;
    using Microsoft.Extensions.Options;
    using System;
    using System.Data.SqlClient;

    /// <inheritxmldoc/>
    public class FileMetaServiceImpl : IFileMetaService
    {
        private readonly IFileMetaDbFactory fileMetaDbFactory;
        private readonly IFileRepository fileRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileMetaDbFactory">factory class for creating database connections</param>
        /// <param name="fileRepository">the domain repository for interacting with the databse for <code>File</code> information</param>
        public FileMetaServiceImpl(
            IFileMetaDbFactory fileMetaDbFactory,
            IFileRepository fileRepository
        )
        {
            this.fileMetaDbFactory = fileMetaDbFactory;
            this.fileRepository = fileRepository;
        }

        /// <inheritxmldoc/>
        public void CreateFileRecord(File file)
        {
            if (file == null)
            {
                throw new ArgumentException("File argument must not be null");
            }

            using (var connection = fileMetaDbFactory.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        fileRepository.Create(file, connection, transaction);

                        transaction.Commit();
                    } catch (Exception e)
                    {
                        transaction.Rollback();

                        throw e;
                    }
                }
            }
        }

        /// <inheritxmldoc/>
        public File GetFileRecord(int fileId)
        {
            if (fileId.Equals(null))
            {
                throw new ArgumentException("File argument must not be null");
            }

            using (var connection = fileMetaDbFactory.CreateConnection())
            {
                connection.Open();
                        
                return fileRepository.Get(fileId);
            }
        }
    }
}