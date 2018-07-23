using System.Collections.Generic;

namespace Bluebox.FileMeta.Dto
{
    /// <summary>
    /// <para>
    /// Identifier for the file.
    /// </para>
    /// </summary>
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
        /// List of namespaces that the file belongs to. A file could exist in multiple namespaces, because the user defines the namespace and the file could be shared, so each user can specify for himself which namespace each file will belong to.
        /// </para>
        /// </summary>
        public List<int> Namespaces { get; }
        /// <summary>
        /// <para>
        /// Part list.
        /// </para>
        /// </summary>
        public List<Part> Parts { get; }
    }
}
