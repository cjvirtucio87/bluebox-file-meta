namespace BlueBox.FileMeta.Tests.Integration
{
    using BlueBox.FileMeta.Dto;
    using BlueBox.FileMeta.Resources;
    using BlueBox.FileMeta.Service;
    using BlueBox.FileMeta.Sql;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public class FileMetaServiceFixture : IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IEmbeddedResourceResolver embeddedResourceResolver;
        private readonly IFileMetaDbFactory fileMetaDbFactory;
        private Dto.File file;

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

            using (var stream = embeddedResourceResolver.GetStream($"sql/mysql_init.sql"))
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

        public IFileMetaService GetFileMetaService()
        {
            return serviceProvider.GetService<IFileMetaService>();
        }

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

        public Stream GetSqlScript(string name)
        {
            return embeddedResourceResolver.GetStream($"sql/{name}");
        }

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
