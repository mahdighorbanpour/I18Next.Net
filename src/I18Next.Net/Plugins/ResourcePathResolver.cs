using I18Next.Net.Backends;

namespace I18Next.Net.Plugins
{
    public class ResourcePathResolver : IPathResolver
    {
        public string GetPath(string basePath, string language, string @namespace, string fileExension)
            => $"{basePath}.{language.Replace('-', '_')}.{@namespace}{fileExension}";

        public string GetPathForBaseLanguage(string basePath, string language, string @namespace, string fileExension)
            => GetPath(basePath, BackendUtilities.GetLanguagePart(language), @namespace, fileExension);
    }
}
