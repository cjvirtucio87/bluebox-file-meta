namespace BlueBox.FileMeta.Api
{
    /// <summary>
    /// <para>
    /// Business logic pertaining to file metadata.
    /// </para>
    /// </summary>
    public interface IFileMetaService
    {
        /// <summary>
        /// <para>
        /// Create a journal record representing a file and its part list.
        /// </para>
        /// </summary>
        void CreateFileRecord(Dto.File file);

        /// <summary>
        /// <para>
        /// Get a journal record representing a file and its part list.
        /// </para>
        /// </summary>
        Dto.File GetFileRecord(int fileId);
    }
}
