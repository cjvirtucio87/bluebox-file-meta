namespace BlueBox.FileMeta.Tests.Integration
{
    using BlueBox.FileMeta.Service;
    using System;
    using Xunit;

    public class FileMetaServiceFacts
    {
        public class CreateFileRecord : IClassFixture<FileMetaServiceFixture>
        {
            private readonly IFileMetaService fileMetaService;
            private readonly Dto.File file;

            public CreateFileRecord(FileMetaServiceFixture fileMetaServiceFixture)
            {
                fileMetaService = fileMetaServiceFixture.GetFileMetaService();

                file = fileMetaServiceFixture.GetFile();
            }

            [Fact]
            public void ShouldThrowExceptionOnNullFile()
            {
                Assert.Throws<ArgumentException>(() => fileMetaService.CreateFileRecord(null));
            }

            [Fact]
            public void ShouldCreateFileRecord()
            {
                var actualParts = fileMetaService.GetFileRecord(
                    fileMetaService.CreateFileRecord(file)
                ).Parts;

                for (var i = 0; i < file.Parts.Count; i++) {
                    Assert.Equal(
                        file.Parts[i].BlockId,
                        actualParts[i].BlockId
                    );
                }
            }
        }
    }
}
