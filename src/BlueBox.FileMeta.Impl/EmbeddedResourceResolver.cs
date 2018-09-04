namespace BlueBox.FileMeta.Impl
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using BlueBox.FileMeta.Api;

    /// <inheritxmldoc/>
    public class EmbeddedResourceResolver : IEmbeddedResourceResolver
    {
        private readonly Assembly assembly;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="assembly">the assembly to retrieve resources from</param>
        public EmbeddedResourceResolver(Assembly assembly)
        {
            this.assembly = assembly;
        }

        /// <inheritxmldoc/>
        public Stream GetStream(string resourcePath)
        {
            // inspired by https://stackoverflow.com/a/27993476
            var parsedResourcePath = resourcePath.Replace(@"/", ".");

            return assembly
                    .GetManifestResourceStream(
                        assembly
                            .GetManifestResourceNames()
                            .FirstOrDefault(
                                resourceName => resourceName.Contains(parsedResourcePath)
                            )
                    );
        }
    }
}
