namespace BlueBox.FileMeta.Tests.Integration
{
    using System.Data;

    /// <summary>
    /// Utility methods for setting up/changing a test environment.
    /// </summary>
    public class TestUtil
    {
        /// <summary>
        /// Execute a sql script.
        /// </summary>
        /// <param name="sql">the script to execute</param>
        /// <param name="connection">the connection to the database</param>
        public static void ExecuteSql(string sql, IDbConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }
    }
}
