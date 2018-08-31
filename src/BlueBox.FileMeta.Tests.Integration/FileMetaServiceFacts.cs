using BlueBox.FileMeta.Api;
using System;
using Xunit;

namespace BlueBox.FileMeta.Tests.Integration
{
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

            /// <summary>
            /// Constructor.
            /// </summary>
            public CreateFileRecord(FileMetaServiceFixture fileMetaServiceFixture)
            {
                fileMetaService = fileMetaServiceFixture.GetFileMetaService();
            }

            /// <summary>
            /// Null check test.
            /// </summary>
            [Fact]
            public void ShouldThrowExceptionOnNullFile()
            {
                Assert.Throws<Exception>(() => fileMetaService.CreateFileRecord(null));
            }
        }
    }
}