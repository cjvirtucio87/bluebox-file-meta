namespace BlueBox.FileMeta.Dto
{
    using System.Collections.Generic;
    /// <summary>
    /// <para>
    /// Representation of a shared/shareable directory.
    /// </para>
    /// </summary>
    public class Namespace
    {
        /// <summary>
        /// <para>
        /// Identifier for the file.
        /// </para>
        /// </summary>
        public int NamespaceId { get; }

        /// <summary>
        /// <para>
        /// A string representing the actual value of the namespace.
        /// </para>
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// <para>
        /// The namespaces that are mounted on this namespace.
        /// </para>
        /// <para>
        /// A namespace can have multiple instances of the same child namespace, as long they're given different names when mounted on the root.
        /// </para>
        /// <para>
        /// Each child namespace can also be given the same name as other items of different types, such as <code>Dto.File</code> or <code>Dto.Directory</code>.
        /// </para>
        /// </summary>
        public Dictionary<string, Namespace> ChildNamespaces { get; }

        /// <summary>
        /// <para>
        /// The files that are mounted on this namespace.
        /// </para>
        /// <para>
        /// A namespace can have multiple instances of the same child file, as long they're given different names when mounted on the root.
        /// </para>
        /// <para>
        /// Each child file can also be given the same name as other items of different types, such as <code>Dto.Namespace</code> or <code>Dto.Directory</code>.
        /// </para>
        /// </summary>
        public Dictionary<string, Dto.File> ChildFiles { get; }

        /// <summary>
        /// <para>
        /// The directories that are mounted on this namespace.
        /// </para>
        /// <para>
        /// A namespace can have multiple instances of the same child directory, as long they're given different names when mounted on it.
        /// </para>
        /// <para>
        /// Each child namespace can also be given the same name as other items of different types, such as <code>Dto.File</code> or <code>Dto.Namespace</code>.
        /// </para>
        /// </summary>
        public Dictionary<string, Namespace> ChildDirectories { get; }
    }
}
