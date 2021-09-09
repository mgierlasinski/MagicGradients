using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace PlaygroundMaui.Resources
{
    public class DocumentReader
    {
        public T GetDocument<T>(string fullPath)
        {
            var assembly = typeof(DocumentReader).GetTypeInfo().Assembly;

            using var stream = assembly.GetManifestResourceStream(fullPath);
            using var streamReader = new StreamReader(stream);
            
            var serializer = new JsonSerializer();
            return (T)serializer.Deserialize(streamReader, typeof(T));
        }
    }
}
