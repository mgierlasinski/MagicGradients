using MagicCrawler.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace MagicCrawler.Services
{
    public class Storage
    {
        private readonly JsonSerializerSettings _settings;

        public Configuration Configuration { get; private set; }

        public Storage()
        {
            _settings = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public Configuration LoadConfiguration(string path)
        {
            var file = File.ReadAllText(path);
            Configuration = JsonConvert.DeserializeObject<Configuration>(file);

            return Configuration;
        }

        public void CreateOutput()
        {
            if (!Directory.Exists(Configuration.Output))
                Directory.CreateDirectory(Configuration.Output);
        }

        public void WriteObject(string fileName, object content)
        {
            var json = JsonConvert.SerializeObject(content, Formatting.Indented, _settings);
            WriteFile(fileName, json);
        }

        public void WriteFile(string fileName, string content)
        {
            var output = Path.Combine(Configuration.Output, fileName);
            File.WriteAllText(output, content);
        }
    }
}
