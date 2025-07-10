namespace I18Next.Net.Plugins
{
    public interface IPathResolver
    {
        string GetPath(string basePath, string language, string @namespace, string fileExension);
        string GetPathForBaseLanguage(string basePath, string language, string @namespace, string fileExension);
    }
}
