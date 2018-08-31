namespace BlueBox.FileMeta.Dto
{
    using System.Collections.Generic;
    /// <summary>
    /// <para>
    /// Representation of a file.
    /// </para>
    /// </summary>
    public class File
    {
        /// <summary>
        /// <para>
        /// Identifier for the file.
        /// </para>
        /// </summary>
        public int FileId { get; set; }
        /// <summary>
        /// <para>
        /// Part list.
        /// </para>
        /// </summary>
        public List<Part> Parts { get; set; }
    }
}
