namespace BlueBox.FileMeta.Api
{
    using System.Data;

    /// <summary>
    /// Pass-through for the <code>DbProviderFactory</code> class.
    /// </summary>
    public interface IFileMetaDbFactory
    {
        /// <summary>
        /// Create a new database connection with the underlying <code>DbProviderFactory</code>
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateConnection();
    }
}
