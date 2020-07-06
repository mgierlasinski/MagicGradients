using MagicCrawler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicCrawler.Services
{
    public class Crawler
    {
        private readonly Storage _storage;
        private readonly HtmlLoader _loader;
        private readonly HtmlParser _parser;

        public Crawler(Storage storage, HtmlLoader loader)
        {
            _storage = storage;
            _loader = loader;
            _parser = new HtmlParser();
        }

        public async Task ExecuteJobs(IEnumerable<JobItem> jobs)
        {
            _storage.CreateOutput();

            var categories = new List<Category>();
            var vGradients = new List<Gradient>();

            foreach (var job in jobs)
            {
                await ExecuteJob(job, categories, vGradients);
            }

            WriteMetadata();
            _storage.WriteObject("Categories.json", categories);
        }

        private async Task ExecuteJob(JobItem job, List<Category> categories, List<Gradient> vGradients)
        {
            try
            {
                job.Status = "Parsing...";
                var result = await WriteEndpoint(job.Data, categories, vGradients);
                job.Status = result == null ? "Done" : $"Parsed {result}";
            }
            catch (Exception e)
            {
                job.Status = $"Error: {e.Message}";
            }
        }

        private async Task<int?> WriteEndpoint(Endpoint endpoint, List<Category> categories, List<Gradient> vGradients)
        {
            var input = $"{_storage.Configuration.Input}{endpoint.GetUrl()}";
            var html = await _loader.LoadAsync(input);
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
            _storage.WriteObject(endpoint.GetFile(), gradients);

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
                Gradients = _storage.Configuration.Endpoints.Where(x => !x.IsVirtual).Select(x => x.GetFile()).ToArray()
            };

            _storage.WriteObject("Metadata.json", metadata);
        }
    }
}
