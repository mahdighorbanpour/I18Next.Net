using I18Next.Net.Plugins;
using I18Next.Net.TranslationTrees;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace I18Next.Net.Backends;

/// <summary>
/// Provides an extended backend for managing translation data stored in JSON files.
/// </summary>
/// <remarks>This class extends the functionality of <see cref="JsonFileBackend"/> by adding support for loading
/// translation namespaces from multiple JSON files within a specified directory ordered by file names. It allows for dynamic discovery of
/// namespaces based on file names.</remarks>
public class JsonFileExtendedBackend : JsonFileBackend
{
    public JsonFileExtendedBackend(string basePath)
        : base(basePath, new GenericTranslationTreeBuilderFactory<HierarchicalTranslationTreeBuilder>(), new DefaultPathResolver())
    {
    }
    public JsonFileExtendedBackend(string basePath, ITranslationTreeBuilderFactory treeBuilderFactory, IPathResolver pathResolver)
        : base(basePath, treeBuilderFactory, pathResolver)
    {
    }
    public JsonFileExtendedBackend(ITranslationTreeBuilderFactory treeBuilderFactory, IPathResolver pathResolver)
        : base(treeBuilderFactory, pathResolver)
    {
    }
    public JsonFileExtendedBackend()
        : base()
    {
    }

    public override async Task<ITranslationTree> LoadNamespaceAsync(string language, string @namespace)
    {
        List<string> @namespaces = [@namespace];
        foreach (var file in Directory.GetFiles(_basePath, "*.json", SearchOption.AllDirectories)
            .OrderBy(f => f))
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            if (fileName != null && !@namespaces.Contains(fileName))
            {
                @namespaces.Add(fileName);
            }
        }

        var builder = _treeBuilderFactory.Create();
        foreach (var ns in @namespaces)
        {
            var path = FindFile(language, ns);

            if (path == null)
                continue;

            JObject parsedJson;

            using (var streamReader = new StreamReader(path, Encoding))
            using (var reader = new JsonTextReader(streamReader))
            {
                parsedJson = (JObject)await JToken.ReadFromAsync(reader);
            }

            PopulateTreeBuilder("", parsedJson, builder);
        }

        return builder.Build();
    }
}
