namespace BlueBox.FileMeta.Grpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BlueBox.FileMeta.Service;
    using global::Grpc.Core;

    /// <inheritdoc/>
    public class FileMetaServer : FileMeta.FileMetaBase 
    {
        private readonly IFileMetaService fileMetaService;

        /// <summary>
        /// Constructor.
        /// </summary>
        public FileMetaServer(IFileMetaService fileMetaService)
        {
            this.fileMetaService = fileMetaService;
        }

        /// <inheritdoc/>
        public override Task<File> GetFileRecord(FileRequest fileRequest, ServerCallContext serverCallContext)
        {
            var fileRecord = fileMetaService.GetFileRecord(fileRequest.Id);

            var parts = new List<Part>();

            foreach (var fileRecordPart in fileRecord.Parts) 
            {
                parts.Add(
                    new Part {
                        Id = fileRecordPart.Id,
                        BlockId = fileRecordPart.BlockId
                    }
                );
            }

            var file = new File {
                Id = fileRecord.Id,
            };

            file.Parts.AddRange(parts);

            return Task.FromResult(file);
        }
    }
}