namespace Bluebox.FileMeta.Api
{
    /// <summary>
    /// <para>
    /// Business logic pertaining to file metadata.
    /// </para>
    /// </summary>
    public interface FileMetaService
    {
        /// <summary>
        /// <para>
        /// Create a journal record representing a file and its part list.
        /// </para>
        /// </summary>
        void Create(Dto.File file);
    }
}
