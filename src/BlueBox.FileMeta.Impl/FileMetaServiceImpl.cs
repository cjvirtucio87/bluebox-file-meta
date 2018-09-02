namespace BlueBox.FileMeta.Impl
{
    using BlueBox.FileMeta.Api;
    using BlueBox.FileMeta.Dto;
    using System;

    /// <inheritxmldoc/>
    public class FileMetaServiceImpl : IFileMetaService
    {
        private readonly IFileRepository fileRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileRepository">the domain repository for interacting with the databse for <code>File</code> information</param>
        public FileMetaServiceImpl(
            IFileRepository fileRepository
        )
        {
            this.fileRepository = fileRepository;
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