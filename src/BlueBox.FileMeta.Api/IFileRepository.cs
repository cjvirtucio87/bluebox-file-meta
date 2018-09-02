using System.Data;

namespace BlueBox.FileMeta.Api
{
    /// <summary>
    /// <para>Domain object for persisting/retrieving <code>File</code> information.</para>
    /// <para>If the method takes an instance of <code>IDbConnection</code>, then the connection is assumed to be open.</para>
    /// </summary>
    public interface IFileRepository
    {
        /// <summary>
        /// Create a new <code>File</code> in the database.
        /// </summary>
        /// <param name="file">the file to persist</param>
        /// <param name="connection">the connection to leverage for persisting the information</param>
        void Create(Dto.File file, IDbConnection connection);
    }
}
