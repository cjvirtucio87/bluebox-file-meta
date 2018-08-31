using BlueBox.FileMeta.Api;
using BlueBox.FileMeta.Dto;
using System;

namespace BlueBox.FileMeta.Impl
{

    /// <inheritxmldoc/>
    public class FileMetaServiceImpl : IFileMetaService
    {

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