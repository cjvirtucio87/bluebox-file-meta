namespace BlueBox.FileMeta.Tests.Integration
{
    using BlueBox.FileMeta.Service;
    using System;
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

                for (var i = 0; i < file.Parts.Count; i++) {
                    var actualParts = fileMetaService.GetFileRecord(
                        file.Id
                    ).Parts;

                    Assert.Equal(
                        file.Parts[i].BlockId,
                        actualParts[i].BlockId
                    );
                }
            }
        }
    }
}
