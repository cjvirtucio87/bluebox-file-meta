namespace Bluebox.FileMeta.Dto
{
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
        /// The encrypted hash representing the file part's fully-qualified namespace.
        /// </para>
        /// </summary>
        public string Hash { get; }
    }
}
