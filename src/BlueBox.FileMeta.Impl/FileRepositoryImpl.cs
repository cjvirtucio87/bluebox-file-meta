namespace BlueBox.FileMeta.Impl
{
    using BlueBox.FileMeta.Api;
    using BlueBox.FileMeta.Dto;
    using Microsoft.Extensions.Options;
    using System;

    /// <inheritxmldoc/>
    public class FileRepositoryImpl : IFileRepository
    {
        private readonly FileMetaServiceSettings fileMetaServiceSettings;

        /// <param name="fileMetaServiceSettingsOptions">configuration settings for the <code>IFileMetaService</code></param>
        public FileRepositoryImpl(
            IOptions<FileMetaServiceSettings> fileMetaServiceSettingsOptions
        )
        {
            fileMetaServiceSettings = fileMetaServiceSettingsOptions.Value;
        }

        /// <inheritxmldoc/>
        public void Create(File file)
        {
            if (file == null)
            {
                throw new ArgumentException("File cannot be null");
            }
        }
    }
}
