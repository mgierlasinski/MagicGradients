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

            foreach (var endpoint in _configuration.Endpoints)
            {
                await WriteEndpoint(endpoint, categories);
            }

            WriteMetadata();
            WriteObject("Categories.json", categories);
        }

        private void LoadConfiguration()
        {
            var file = File.ReadAllText("Configuration.json");
            _configuration = JsonConvert.DeserializeObject<Configuration>(file);
        }

        private async Task WriteEndpoint(Endpoint endpoint, List<Category> categories)
        {
            var input = $"{_configuration.Input}{endpoint.Url}";
            var html = await HtmlLoader.LoadAsync(input);
            var gradients = _parser.Parse(html, endpoint.GetTag());

            var category = new Category
            {
                Name = endpoint.Title,
                Tag = endpoint.GetTag(),
                Slug = gradients.FirstOrDefault()?.Slug
            };

            categories.Add(category);

            WriteObject(endpoint.File, gradients);
        }

        private void WriteMetadata()
        {
            var metadata = new Metadata
            {
                Version = 1,
                Date = DateTime.Now,
                NameSpace = "Playground.Data.Resources",
                Categories = "Categories.json",
                Themes = "Themes.json",
                Gradients = _configuration.Endpoints.Select(x => x.File).ToArray()
            };

            WriteObject("Metadata.json", metadata);
        }

        private void WriteObject(string fileName, object content)
        {
            var json = JsonConvert.SerializeObject(content, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
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
