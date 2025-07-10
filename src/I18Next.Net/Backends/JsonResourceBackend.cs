// Platform: P3.4.0 AppZero

using I18Next.Net.Plugins;
using I18Next.Net.TranslationTrees;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace I18Next.Net.Backends;

public class JsonResourceBackend : JsonFileBackend, ITranslationBackend
{
    private readonly Assembly _assembly;

    public JsonResourceBackend(string basePath, Assembly assembly)
        : this(basePath, assembly, new GenericTranslationTreeBuilderFactory<HierarchicalTranslationTreeBuilder>(), new ResourcePathResolver())
    {
    }

    public JsonResourceBackend(string basePath, Assembly assembly, ITranslationTreeBuilderFactory treeBuilderFactory, IPathResolver pathResolver)
        : base(basePath, treeBuilderFactory, pathResolver)
    {
        _assembly = assembly;
    }

    public JsonResourceBackend(ITranslationTreeBuilderFactory treeBuilderFactory, IPathResolver pathResolver)
        : this("locales", typeof(JsonResourceBackend).Assembly, treeBuilderFactory, pathResolver)
    {
    }

    public JsonResourceBackend()
        : this("locales", typeof(JsonResourceBackend).Assembly)
    {
    }

    public override Task<ITranslationTree> LoadNamespaceAsync(string language, string @namespace)
    {
        var jsonString = FindFile(language, @namespace);
        if (string.IsNullOrEmpty(jsonString))
        {
            return Task.FromResult((ITranslationTree)null);
        }

        var parsedJson = JObject.Parse(jsonString);

        var builder = _treeBuilderFactory.Create();
        PopulateTreeBuilder("", parsedJson, builder);
        return Task.FromResult(builder.Build());
    }

    protected override string FindFile(string language, string @namespace)
    {
        // "I18Next.Net.Tests.TestFiles.en_US.testResource.json"
        var resourceFilename = _pathResolver.GetPath(_basePath, language, @namespace, ".json");
        var jsonString = EmbeddedJsonFileHelper.FindFile(_assembly, resourceFilename);

        if (string.IsNullOrWhiteSpace(jsonString))
        {
            resourceFilename = _pathResolver.GetPathForBaseLanguage(_basePath, language, @namespace, ".json");
            jsonString = EmbeddedJsonFileHelper.FindFile(_assembly, resourceFilename);
        }
        return jsonString;
    }
}
