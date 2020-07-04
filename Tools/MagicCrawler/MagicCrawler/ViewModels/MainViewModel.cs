using MagicCrawler.Models;
using MagicCrawler.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MagicCrawler.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly HtmlParser _parser;
        private Configuration _configuration;

        public HtmlLoader HtmlLoader { get; }
        public AsyncCommand GenerateCommand { get; }

        private List<JobItem> _jobs;
        public List<JobItem> Jobs
        {
            get => _jobs;
            set => SetProperty(ref _jobs, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value, onChanged: GenerateCommand.RaiseCanExecuteChanged);
        }

        public MainViewModel()
        {
            _parser = new HtmlParser();

            HtmlLoader = new HtmlLoader();
            Jobs = new List<JobItem>();

            GenerateCommand = new AsyncCommand(Generate, x => !IsBusy);
        }

        public void Initialize()
        {
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            var file = File.ReadAllText("Configuration.json");
            _configuration = JsonConvert.DeserializeObject<Configuration>(file);

            Jobs = new List<JobItem>(_configuration.Endpoints.Select(x => new JobItem(x))).ToList();
        }

        public async Task Generate()
        {
            foreach (var job in Jobs)
                job.Reset();

            IsBusy = true;

            if (!Directory.Exists(_configuration.Output))
                Directory.CreateDirectory(_configuration.Output);

            var categories = new List<Category>();
            var vGradients = new List<Gradient>();

            foreach (var job in Jobs)
            {
                await ExecuteJob(job, categories, vGradients);
            }

            WriteMetadata();
            WriteObject("Categories.json", categories);

            IsBusy = false;
        }

        private async Task ExecuteJob(JobItem job, List<Category> categories, List<Gradient> vGradients)
        {
            job.Status = "Parsing...";

            var result = await WriteEndpoint(job.Data, categories, vGradients);
            job.Status = result == null ? "Done" : $"Parsed {result}";
        }

        private async Task<int?> WriteEndpoint(Endpoint endpoint, List<Category> categories, List<Gradient> vGradients)
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
                return null;
            }

            category.Slug = gradients.FirstOrDefault()?.Slug;
            categories.Add(category);

            AddVirtualTags(gradients, categories, vGradients);
            WriteObject(endpoint.GetFile(), gradients);

            return gradients.Length;
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
