using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Playground.StaticData
{
    public static class StaticDataReader
    {
        public static IEnumerable<string> ReadLines(string fileName)
        {
            var lines = new List<string>();
            var assembly = typeof(StaticDataReader).GetTypeInfo().Assembly;

            using (var stream = assembly.GetManifestResourceStream(fileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            return lines;
        }
    }
}
