namespace BlueBox.FileMeta.Impl
{
    using BlueBox.FileMeta.Api;
    using BlueBox.FileMeta.Dto;
    using Microsoft.Extensions.Options;
    using System;

    /// <inheritxmldoc/>
    public class FileMetaServiceImpl : IFileMetaService
    {
        private readonly FileMetaServiceSettings fileMetaServiceSettings;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileMetaServiceSettingsOptions">configuration settings for the <code>IFileMetaService</code></param>
        public FileMetaServiceImpl(IOptions<FileMetaServiceSettings> fileMetaServiceSettingsOptions)
        {
            fileMetaServiceSettings = fileMetaServiceSettingsOptions.Value;
        }

        /// <inheritxmldoc/>
        public void CreateFileRecord(File file)
        {
            if (file == null)
            {
                throw new ArgumentException("File argument must not be null");
            }
        }

        /// <inheritxmldoc/>
        public File GetFileRecord(int file)
        {
            throw new System.NotImplementedException();
        }
    }
}