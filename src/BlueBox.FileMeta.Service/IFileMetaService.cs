namespace BlueBox.FileMeta.Service
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
        /// <returns>the identifier of the file record that was created</returns>
        int CreateFileRecord(Dto.File file);

        /// <summary>
        /// <para>
        /// Get a journal record representing a file and its part list.
        /// </para>
        /// </summary>
        /// <returns>the <code>File</code> identified by <code>fileId</code></returns>
        Dto.File GetFileRecord(int fileId);
    }
}
