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
        }

        /// <inheritxmldoc/>
        public IDbConnection CreateConnection()
        {
            return dbProviderFactory.CreateConnection();
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
