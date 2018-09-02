namespace BlueBox.FileMeta.Impl
{
    using BlueBox.FileMeta.Api;
    using BlueBox.FileMeta.Dto;
    using Microsoft.Extensions.Options;
    using System;
    using System.Data.SqlClient;

    /// <inheritxmldoc/>
    public class FileMetaServiceImpl : IFileMetaService
    {
        private readonly FileMetaServiceSettings fileMetaServiceSettings;
        private readonly IFileRepository fileRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileMetaServiceSettingsOptions">configuration options</param>
        /// <param name="fileRepository">the domain repository for interacting with the databse for <code>File</code> information</param>
        public FileMetaServiceImpl(
            IOptions<FileMetaServiceSettings> fileMetaServiceSettingsOptions,
            IFileRepository fileRepository
        )
        {
            fileMetaServiceSettings = fileMetaServiceSettingsOptions.Value;
            this.fileRepository = fileRepository;
        }

        /// <inheritxmldoc/>
        public void CreateFileRecord(File file)
        {
            if (file == null)
            {
                throw new ArgumentException("File argument must not be null");
            }

            using (var connection = new SqlConnection(fileMetaServiceSettings.DbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    fileRepository.Create(file, connection);

                    transaction.Commit();
                }
            }
        }

        /// <inheritxmldoc/>
        public File GetFileRecord(int file)
        {
            throw new NotImplementedException();
        }
    }
}