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

        public async Task ExecuteJobs(List<JobItem> jobs)
        {
            _storage.CreateOutput();

            var categories = new List<Category>();
            var gradients = new List<Gradient>();

            foreach (var job in jobs)
            {
                await ExecuteJob(job, categories, gradients);
            }

            WriteMetadata();
            WriteCategories(categories);
            WriteCollections(jobs.Select(x => x.Data));
        }

        private async Task ExecuteJob(JobItem job, List<Category> categories, List<Gradient> gradients)
        {
            try
            {
                job.Status = "Parsing...";
                var result = await ParseCollection(job.Data, categories, gradients);
                job.Status = result == null ? "Done" : $"Parsed {result}";
            }
            catch (Exception e)
            {
                job.Status = $"Error: {e.Message}";
            }
        }

        private async Task<int?> ParseCollection(Collection collection, List<Category> categories, List<Gradient> gradients)
        {
            categories.Add(collection.ToCategory());

            var html = await _loader.LoadAsync(collection.GetFullUrl());
            var collectionGradients = _parser.Parse(html, collection.GetTag());

            RemoveDuplicates(collectionGradients, gradients);
            
            gradients.AddRange(collectionGradients);
            collection.Gradients = collectionGradients;

            return collectionGradients.Count;
        }

        private void RemoveDuplicates(List<Gradient> collectionGradients, List<Gradient> allGradients)
        {
            var gradients = collectionGradients.ToArray();

            foreach (var gradient in gradients)
            {
                var existing = allGradients.Find(x => x.Stylesheet == gradient.Stylesheet);
                if (existing != null)
                {
                    existing.Tags.AddRange(gradient.Tags);
                    collectionGradients.Remove(gradient);
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
                Themes = "Themes.json"
            };

            _storage.WriteObject("Metadata.json", metadata);
        }

        private void WriteCategories(List<Category> categories)
        {
            _storage.WriteObject("Categories.json", categories.OrderBy(x => x.DisplayOrder));
        }

        private void WriteCollections(IEnumerable<Collection> collections)
        {
            foreach (var collection in collections)
            {
                _storage.WriteObject(collection.GetFile(), collection.Gradients);
            }
        }
    }
}
