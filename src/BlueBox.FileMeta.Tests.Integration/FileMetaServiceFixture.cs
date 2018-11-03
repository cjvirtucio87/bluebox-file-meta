namespace BlueBox.FileMeta.Tests.Integration
{
    using BlueBox.FileMeta.Api;
    using BlueBox.FileMeta.Dto;
    using BlueBox.FileMeta.Impl;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// <para>
    /// Fixture class for the <code>FileMetaService</code> integration tests. 
    /// </para>
    /// <para>
    /// All instances of the implementations retrieved from this class are shared. For instance, if test A calls <code>GetFooService()</code> from this fixture, and test B calls the same method, the two tests get the exact same instance in memory.
    /// </para>
    /// </summary>
    public class FileMetaServiceFixture : IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IEmbeddedResourceResolver embeddedResourceResolver;
        private readonly IFileMetaDbFactory fileMetaDbFactory;
        private Dto.File file;

        /// <summary>
        /// Constructor
        /// </summary>
        public FileMetaServiceFixture()
        {
            serviceProvider = new ServiceCollection()
                .AddOptions()
                .Configure<FileMetaServiceSettings>(
                    fileMetaServiceSettings => new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", false, false)
                        .Build()
                        .GetSection("FileMetaService")
                        .Bind(fileMetaServiceSettings)
                )
                .AddSingleton(Assembly.GetExecutingAssembly())
                .AddSingleton<IEmbeddedResourceResolver, EmbeddedResourceResolver>()
                .AddSingleton<IFileMetaDbFactory, FileMetaDbFactoryImpl>()
                .AddSingleton<IFileRepository, FileRepositoryImpl>()
                .AddSingleton<IFileMetaService, FileMetaServiceImpl>()
                .BuildServiceProvider();

            embeddedResourceResolver = serviceProvider.GetService<IEmbeddedResourceResolver>();
            fileMetaDbFactory = serviceProvider.GetService<IFileMetaDbFactory>();

            using (var stream = embeddedResourceResolver.GetStream($"sql/init.sql"))
            using (var connection = fileMetaDbFactory.CreateConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = new StreamReader(stream).ReadToEnd();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Retrieve an instance of the <code>IFileMetaService</code> implementation.
        /// </summary>
        /// <returns>an implementation of <code>IFileMetaService</code></returns>
        public IFileMetaService GetFileMetaService()
        {
            return serviceProvider.GetService<IFileMetaService>();
        }

        /// <summary>
        /// Get the <code>Dto.File</code> containing File metadata.
        /// </summary>
        /// <returns>a singleton instance of <code>Dto.File</code></returns>
        public Dto.File GetFile()
        {
            if (file == null)
            {
                file = new Dto.File
                {
                    Id = 1,
                    Parts = new List<Part>
                    {
                        new Part
                        {
                            Id = 1,
                            BlockId = "DR21BDGFL%"
                        },
                        new Part {
                            Id = 2,
                            BlockId = "MQ59AN34"
                        },
                        new Part {
                            Id = 3,
                            BlockId = "2PSO90FNB5"
                        }
                    }
                };
            }

            return file;
        }

        /// <summary>
        /// Retrieve the file stream for an embedded SQL script.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>the <code>Stream</code> for the embedded SQL script</returns>
        public Stream GetSqlScript(string name)
        {
            return embeddedResourceResolver.GetStream($"sql/{name}");
        }

        /// <summary>
        /// Dispose of the fixture.
        /// </summary>
        public void Dispose()
        {
            using (var stream = embeddedResourceResolver.GetStream($"sql/cleanup.sql"))
            using (var connection = fileMetaDbFactory.CreateConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = new StreamReader(stream).ReadToEnd();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
