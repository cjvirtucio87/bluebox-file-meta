namespace BlueBox.FileMeta.Resources
{
    using System.IO;

    /// <summary>
    /// Class for resolving resources embedded in an assembly.
    /// </summary>
    public interface IEmbeddedResourceResolver
    {
        /// <summary>
        /// <para>Retrieve the <code>System.IO.Stream</code> representing the resource being retrieved.</para>
        /// <para>Expects a resource path using the UNIX file path separator.</para>
        /// </summary>
        /// <param name="resourcePath">path to the resource to be retrieved</param>
        /// <returns>the <code>System.IO.Stream</code> representing the resource sought</returns>
        Stream GetStream(string resourcePath);
    }
}
