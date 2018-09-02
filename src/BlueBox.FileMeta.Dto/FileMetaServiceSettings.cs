namespace BlueBox.FileMeta.Dto
{
    /// <summary>
    /// DTO for service settings.
    /// </summary>
    public class FileMetaServiceSettings
    {
        /// <summary>
        /// Name of the <code>DbProviderFactory</code> instance to be used.
        /// </summary>
        public string DbProviderName { get; set; }
        /// <summary>
        /// Connection string for the <code>DbProviderFactory</code>.
        /// </summary>
        public string DbConnectionString { get; set; }
    }
}
