using MagicCrawler.Models;
using MagicCrawler.Services;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json.Serialization;

namespace MagicCrawler.ViewModels
{
    public class MainViewModel
    {
        private readonly HtmlParser _parser;
        private Configuration _configuration;

        public HtmlLoader HtmlLoader { get; }
        public ICommand GenerateCommand { get; }

        public MainViewModel()
        {
            _parser = new HtmlParser();
            HtmlLoader = new HtmlLoader();
            GenerateCommand = new AsyncCommand(Generate);
        }

        public async Task Generate()
        {
            LoadConfiguration();

            if (!Directory.Exists(_configuration.Output))
                Directory.CreateDirectory(_configuration.Output);

            var categories = new List<Category>();
            var vGradients = new List<Gradient>();

            foreach (var endpoint in _configuration.Endpoints)
            {
                await WriteEndpoint(endpoint, categories, vGradients);
            }

            WriteMetadata();
            WriteObject("Categories.json", categories);
        }

        private void LoadConfiguration()
        {
            var file = File.ReadAllText("Configuration.json");
            _configuration = JsonConvert.DeserializeObject<Configuration>(file);
        }

        private async Task WriteEndpoint(Endpoint endpoint, List<Category> categories, List<Gradient> vGradients)
        {
            var input = $"{_configuration.Input}{endpoint.GetUrl()}";
            var html = await HtmlLoader.LoadAsync(input);
            var gradients = _parser.Parse(html, endpoint.GetTag());

            var category = new Category
            {
                Name = endpoint.Title,
                Tag = endpoint.GetTag(),
            };

            if (endpoint.IsVirtual)
            {
                categories.Add(category);
                vGradients.AddRange(gradients);
                return;
            }

            category.Slug = gradients.FirstOrDefault()?.Slug;
            categories.Add(category);

            AddVirtualTags(gradients, categories, vGradients);
            WriteObject(endpoint.GetFile(), gradients);
        }

        private void AddVirtualTags(Gradient[] gradients, List<Category> categories, List<Gradient> vGradients)
        {
            foreach (var gradient in gradients)
            {
                // Match gradient with virtual list and assign additional tags like "popular"
                var vTags = vGradients
                    .Where(x => x.Stylesheet == gradient.Stylesheet)
                    .SelectMany(x => x.Tags)
                    .Distinct()
                    .ToArray();

                gradient.Tags.AddRange(vTags);

                // Match this gradient to virtual category and update it's preview
                foreach (var vTag in vTags)
                {
                    var vCategory = categories.FirstOrDefault(x => x.Tag == vTag);
                    if (vCategory != null)
                    {
                        vCategory.Slug = gradient.Slug;
                    }
                }
            }
        }

        private void WriteMetadata()
        {
            var metadata = new Metadata
            {
                Date = DateTime.Now,
                NameSpace = "Playground.Data.Resources",
                Categories = "Categories.json",
                Themes = "Themes.json",
                Gradients = _configuration.Endpoints.Where(x => !x.IsVirtual).Select(x => x.GetFile()).ToArray()
            };

            WriteObject("Metadata.json", metadata);
        }

        private void WriteObject(string fileName, object content)
        {
            var json = JsonConvert.SerializeObject(content, Formatting.Indented, new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver =  new CamelCasePropertyNamesContractResolver()
            });
            WriteFile(fileName, json);
        }

        private void WriteFile(string fileName, string content)
        {
            var output = Path.Combine(_configuration.Output, fileName);
            File.WriteAllText(output, content);
        }
    }
}
