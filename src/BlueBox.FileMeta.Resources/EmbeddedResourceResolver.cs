namespace BlueBox.FileMeta.Resources
{
    using System.IO;
    using System.Linq;
    using System.Reflection;

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
            return assembly
                    .GetManifestResourceStream(
                        assembly
                            .GetManifestResourceNames()
                            .FirstOrDefault(
                                resourceName => resourceName.Contains(
                                  // inspired by https://stackoverflow.com/a/27993476
                                  resourcePath.Replace(@"/", ".")
                                )
                            )
                    );
        }
    }
}
