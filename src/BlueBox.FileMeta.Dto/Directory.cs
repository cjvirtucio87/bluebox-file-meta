namespace BlueBox.FileMeta.Dto
{
    using System.Collections.Generic;
    /// <summary>
    /// <para>
    /// Representation of a directory.
    /// </para>
    /// </summary>
    public class Directory
    {
        /// <summary>
        /// <para>
        /// Identifier for a particular directory.
        /// </para>
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// <para>
        /// The namespaces that are mounted on this directory.
        /// </para>
        /// <para>
        /// A directory can have multiple instances of the same child namespace, as long they're given different names when mounted on it.
        /// </para>
        /// <para>
        /// Each child namespace can also be given the same name as other items of different types, such as <code>Dto.File</code> or <code>Dto.Directory</code>.
        /// </para>
        /// </summary>
        public Dictionary<string, Namespace> ChildNamespaces { get; }

        /// <summary>
        /// <para>
        /// The files that are mounted on this directory.
        /// </para>
        /// <para>
        /// A directory can have multiple instances of the same child file, as long they're given different names when mounted on it.
        /// </para>
        /// <para>
        /// Each child file can also be given the same name as other items of different types, such as <code>Dto.Namespace</code> or <code>Dto.Directory</code>.
        /// </para>
        /// </summary>
        public Dictionary<string, File> ChildFiles { get; }

        /// <summary>
        /// <para>
        /// The directories that are mounted on this directory.
        /// </para>
        /// <para>
        /// A namespace can have multiple instances of the same child directory, as long they're given different names when mounted on the root.
        /// </para>
        /// <para>
        /// Each child directory can also be given the same name as other items of different types, such as <code>Dto.File</code> or <code>Dto.Namespace</code>.
        /// </para>
        /// </summary>
        public Dictionary<string, Namespace> ChildDirectories { get; }
    }
}
