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
        public class CreateFileRecord
        {
            private readonly IFileMetaService fileMetaService;

            /// <summary>
            /// Constructor.
            /// </summary>
            public CreateFileRecord()
            {
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