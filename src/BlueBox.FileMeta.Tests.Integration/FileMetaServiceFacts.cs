namespace BlueBox.FileMeta.Tests.Integration
{
    using BlueBox.FileMeta.Api;
    using System;
    using System.IO;
    using Xunit;

    /// <summary>
    /// Integration test for the <code>IFileMetaService</code>.
    /// </summary>
    public class FileMetaServiceFacts
    {
        /// <summary>
        /// Integration test for the <code>Create</code> method.
        /// </summary>
        public class CreateFileRecord : IClassFixture<FileMetaServiceFixture>
        {
            private readonly IFileMetaService fileMetaService;
            private readonly Dto.File file;

            /// <summary>
            /// Constructor.
            /// </summary>
            public CreateFileRecord(FileMetaServiceFixture fileMetaServiceFixture)
            {
                fileMetaService = fileMetaServiceFixture.GetFileMetaService();

                file = fileMetaServiceFixture.GetFile();

                using (var stream = fileMetaServiceFixture.GetSqlScript("tables.sql"))
                using (var connection = fileMetaServiceFixture
                                            .GetFileMetaDbFactory()
                                            .CreateConnection())
                {
                    connection.Open();

                    TestUtil.ExecuteSql(
                        new StreamReader(stream).ReadToEnd(),
                        connection
                    );
                }
            }

            /// <summary>
            /// Null check test.
            /// </summary>
            [Fact]
            public void ShouldThrowExceptionOnNullFile()
            {
                Assert.Throws<ArgumentException>(() => fileMetaService.CreateFileRecord(null));
            }

            /// <summary>
            /// Record creation test.
            /// </summary>
            [Fact]
            public void ShouldCreateFileRecord()
            {
                fileMetaService.CreateFileRecord(file);

                Assert.All(
                    file.Parts,
                    expectedPart =>
                    {
                        Assert.All(
                            fileMetaService.GetFileRecord(
                                expectedPart.FileId
                            ).Parts,
                            actualPart =>
                            {
                                Assert.Equal(
                                    expectedPart.BlockId,
                                    actualPart.BlockId
                                );
                            }
                        );
                    }
                );
            }
        }
    }
}