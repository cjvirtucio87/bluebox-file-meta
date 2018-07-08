using System.Collections.Generic;

namespace Bluebox.FileMeta.Dto
{
    public class File
    {
        /// <summary>
        /// <para>
        /// Identifier for the file.
        /// </para>
        /// </summary>
        public int FileId { get; }
        /// <summary>
        /// <para>
        /// Part list.
        /// </para>
        /// </summary>
        public List<Part> Parts { get; }
    }
}
