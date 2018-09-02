namespace BlueBox.FileMeta.Api
{
    /// <summary>
    /// Domain object for persisting/retrieving <code>File</code> information.
    /// </summary>
    public interface IFileRepository
    {
        /// <summary>
        /// Create a new <code>File</code> in the database.
        /// </summary>
        /// <param name="file"></param>
        void Create(Dto.File file);
    }
}
