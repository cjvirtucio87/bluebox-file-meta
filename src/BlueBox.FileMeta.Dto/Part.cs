﻿namespace BlueBox.FileMeta.Dto
{
    /// <summary>
    /// <para>
    /// A partition of the <code>Dto.File</code>.
    /// </para>
    /// </summary>
    public class Part
    {
        /// <summary>
        /// <para>
        /// Identifier for a part.
        /// </para>
        /// </summary>
        public int PartId { get; }
        /// <summary>
        /// <para>
        /// Identifier for the parent file.
        /// </para>
        /// </summary>
        public int FileId { get; }

        /// <summary>
        /// <para>
        /// Identifier for the file part in the block servers.
        /// </para>
        /// </summary>
        public string BlockId { get; }
    }
}