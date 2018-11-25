namespace BlueBox.FileMeta.Sql
{
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// <para>Domain object for persisting/retrieving <code>File</code> information.</para>
    /// <para>If the method takes an instance of <code>IDbConnection</code>, then the connection is assumed to be open.</para>
    /// </summary>
    public interface IFileRegistry
    {
        /// <summary>
        /// Register a new <code>File</code> in the database.
        /// </summary>
        /// <param name="file">the file to persist</param>

        void RegisterFile(Dto.File file);

        /// <summary>
        /// Register a new <code>File</code> in the database.
        /// </summary>
        /// <param name="file">the file to persist</param>
        /// <param name="connection">the connection to persist</param>
        /// <param name="transaction">the transaction to associate the command with</param>

        void RegisterFile(Dto.File file, IDbConnection connection, IDbTransaction transaction);

        /// <summary>
        /// Register parts for a specific <code>File</code> in the database.
        /// </summary>
        /// <param name="fileId">the file that the parts will be mapped to</param>
        /// <param name="parts">the parts to be registered</param>
        void RegisterParts(int fileId, IEnumerable<Dto.Part> parts);

        /// <summary>
        /// Register parts for a specific <code>File</code> in the database.
        /// </summary>
        /// <param name="fileId">the file that the parts will be mapped to</param>
        /// <param name="parts">the parts to be registered</param>
        /// <param name="connection">the connection to persist</param>
        /// <param name="transaction">the transaction to associate the command with</param>
        void RegisterParts(int fileId, IEnumerable<Dto.Part> parts, IDbConnection connection, IDbTransaction transaction);

        /// <summary>
        /// Retrieve a new <code>File</code> in the database.
        /// </summary>
        /// <param name="fileId">the identifier for the file to be retrieved</param>
        /// <returns>the <code>File</code> identified by <code>fileId</code></returns>
        Dto.File GetFile(int fileId);

        /// <summary>
        /// Retrieve a new <code>File</code> in the database.
        /// </summary>
        /// <param name="fileId">the identifier for the file to be retrieved</param>
        /// <param name="connection">the connection to leverage for persisting the information</param>
        /// <param name="transaction">the transaction to associate the command with</param>
        /// <returns>the <code>File</code> identified by <code>fileId</code></returns>
        Dto.File GetFile(int fileId, IDbConnection connection, IDbTransaction transaction);
    }
}
