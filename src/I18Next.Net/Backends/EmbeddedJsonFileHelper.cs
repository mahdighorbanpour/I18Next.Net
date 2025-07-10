using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace I18Next.Net.Backends;

public static class EmbeddedJsonFileHelper
{
    public static string FindFile(Assembly assembly, string resourceFilename)
    {
        var resourceStream = (from resouceName in assembly.GetManifestResourceNames()
                              where resouceName.EndsWith(resourceFilename, ignoreCase: true, null)
                              select resouceName).FirstOrDefault();
        if (resourceStream is not null)
        {
            using var stream = assembly.GetManifestResourceStream(resourceStream)
                ?? throw new Exception($"Could not load the resource file:{resourceFilename}!");
            var jsonString = ReadStringFromStream(stream);
            return jsonString;
        }

        return string.Empty;
    }

    private static string ReadStringFromStream(Stream stream)
    {
        var bytes = GetAllBytes(stream);
        var skipCount = HasBom(bytes) ? 3 : 0;
        return Encoding.UTF8.GetString(bytes, skipCount, bytes.Length - skipCount);
    }

    private static byte[] GetAllBytes(Stream stream)
    {
        _ = stream ?? throw new ArgumentNullException(nameof(stream));
        using var memoryStream = new MemoryStream();
        stream.Position = 0;
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    private static bool HasBom(byte[] bytes)
    {
        if (bytes.Length < 3)
        {
            return false;
        }

        if (!(bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF))
        {
            return false;
        }

        return true;
    }
}
