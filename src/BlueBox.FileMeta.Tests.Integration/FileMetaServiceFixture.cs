namespace BlueBox.FileMeta.Tests.Integration
{
    using BlueBox.FileMeta.Api;
    using BlueBox.FileMeta.Dto;
    using BlueBox.FileMeta.Impl;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;

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
                )
                .AddSingleton<IFileRepository, FileRepositoryImpl>()
                .AddSingleton<IFileMetaService, FileMetaServiceImpl>()
                .BuildServiceProvider();
        }

        /// <summary>
        /// Retrieve an instance of the <code>IFileMetaService</code> implementation.
        /// </summary>
        /// <returns></returns>
        public IFileMetaService GetFileMetaService()
        {
            return serviceProvider.GetService<IFileMetaService>();
        }

        /// <summary>
        /// Get the <code>Dto.File</code> containing File metadata.
        /// </summary>
        /// <returns></returns>
        public Dto.File GetFile()
        {
            if (file == null)
            {
                file = new File
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
        /// Dispose of the fixture.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
