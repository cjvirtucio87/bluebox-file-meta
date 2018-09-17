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

        void Create(Dto.File file, IDbConnection connection);

        /// <summary>
        /// Retrieve a new <code>File</code> in the database.
        /// </summary>
        /// <param name="fileId">the identifier for the file to be retrieved</param>
        /// <param name="connection">the connection to leverage for persisting the information</param>
        /// <returns></returns>
        Dto.File Get(int fileId, IDbConnection connection);
    }
}
