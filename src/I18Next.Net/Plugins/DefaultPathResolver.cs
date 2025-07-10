using I18Next.Net.Backends;
using System.IO;

namespace I18Next.Net.Plugins
{
    public class DefaultPathResolver : IPathResolver
    {
        public string GetPath(string basePath, string language, string @namespace, string fileExension)
            => Path.Combine(basePath, language, @namespace + fileExension);

        public string GetPathForBaseLanguage(string basePath, string language, string @namespace, string fileExension)
            => GetPath(basePath, BackendUtilities.GetLanguagePart(language), @namespace, fileExension);
    }
}
