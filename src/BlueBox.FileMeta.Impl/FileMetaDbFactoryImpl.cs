namespace BlueBox.FileMeta.Impl
{
    using System;
    using System.Data;
    using System.Data.Common;
    using BlueBox.FileMeta.Api;
    using BlueBox.FileMeta.Dto;
    using Microsoft.Extensions.Options;
    using MySql.Data.MySqlClient;

    /// <inheritxmldoc/>
    public class FileMetaDbFactoryImpl : IFileMetaDbFactory
    {
        private readonly DbProviderFactory dbProviderFactory;
        private readonly string connectionString;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileMetaServiceSettingsOptions">configuration options</param>
        public FileMetaDbFactoryImpl(
            IOptions<FileMetaServiceSettings> fileMetaServiceSettingsOptions
        )
        {
            dbProviderFactory = ResolveDbFactory(
                fileMetaServiceSettingsOptions.Value.DbProviderName
            );
            connectionString = fileMetaServiceSettingsOptions.Value.DbConnectionString;
        }

        /// <inheritxmldoc/>
        public IDbConnection CreateConnection()
        {
            var connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = connectionString;

            return connection;
        }

        private DbProviderFactory ResolveDbFactory(string providerName)
        {
            switch (providerName)
            {
                case "MySql.Data.MySqlClient":
                    return MySqlClientFactory.Instance;
                default:
                    throw new ArgumentException($"{providerName} not a valid dbProviderFactory name!");
            }
        }
    }
}
