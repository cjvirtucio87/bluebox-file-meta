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
    using System.Linq;
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
        }

        /// <summary>
        /// Retrieve an instance of the <code>IFileMetaDbFactory</code> implementation.
        /// </summary>
        /// <returns>an instance of the <code>IFileMetaDbFactory</code> implementation</returns>
        public IFileMetaDbFactory GetFileMetaDbFactory()
        {
            return serviceProvider.GetService<IFileMetaDbFactory>();
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
                    FileId = 1,
                    Parts = new List<Part>
                    {
                        new Part
                        {
                            PartId = 1,
                            FileId = 1,
                            BlockId = "DR21BDGFL%"
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
            return serviceProvider.GetService<IEmbeddedResourceResolver>()
                        .GetStream($"sql/{name}");
        }

        /// <summary>
        /// Dispose of the fixture.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
