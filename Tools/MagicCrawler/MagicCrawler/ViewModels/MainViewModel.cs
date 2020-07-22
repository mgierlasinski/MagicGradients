using MagicCrawler.Models;
using MagicCrawler.Services;
using Microsoft.Win32;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MagicCrawler.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly Storage _storage;
        private readonly Crawler _crawler;

        public HtmlLoader HtmlLoader { get; }
        public ICommand BrowseCommand { get; }
        public AsyncCommand GenerateCommand { get; }

        private List<JobItem> _jobs;
        public List<JobItem> Jobs
        {
            get => _jobs;
            set => SetProperty(ref _jobs, value);
        }

        private string _configurationPath;
        public string ConfigurationPath
        {
            get => _configurationPath;
            set => SetProperty(ref _configurationPath, value, onChanged: GenerateCommand.RaiseCanExecuteChanged);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value, onChanged: GenerateCommand.RaiseCanExecuteChanged);
        }

        public MainViewModel()
        {
            HtmlLoader = new HtmlLoader();
            Jobs = new List<JobItem>();

            _storage = new Storage();
            _crawler = new Crawler(_storage, HtmlLoader);

            BrowseCommand = new Command(Browse);
            GenerateCommand = new AsyncCommand(Generate, x => !IsBusy && _storage.Configuration != null);
        }

        public void Initialize()
        {
            LoadConfiguration("Configuration.json");
        }

        private void LoadConfiguration(string path)
        {
            try
            {
                var config = _storage.LoadConfiguration(path);
                
                Jobs = new List<JobItem>(config.Collections.Select(x =>
                {
                    x.BaseUrl = config.Input;
                    return new JobItem(x);
                }
                )).ToList();

                ConfigurationPath = Path.GetFullPath(path);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error loading configuration: {e.Message}", "Error");
            }
        }

        private void Browse()
        {
            var dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();

            if (result == true)
            {
                LoadConfiguration(dialog.FileName);
            }
        }

        private async Task Generate()
        {
            foreach (var job in Jobs)
                job.Reset();

            IsBusy = true;
            await _crawler.ExecuteJobs(Jobs);
            IsBusy = false;
        }
    }
}
