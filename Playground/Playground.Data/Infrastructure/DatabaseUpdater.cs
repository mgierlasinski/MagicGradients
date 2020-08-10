using Playground.Data.Models;
using Playground.Data.Repositories;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Playground.Data.Infrastructure
{
    public class DatabaseUpdater : IDatabaseUpdater
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly IDocumentRepository _documentRepository;

        public DateTime LastUpdate
        {
            get => Preferences.Get(nameof(LastUpdate), DateTime.MinValue);
            set => Preferences.Set(nameof(LastUpdate), value);
        }

        public DatabaseUpdater()
        {
            _databaseProvider = DependencyService.Get<IDatabaseProvider>();
            _documentRepository = DependencyService.Get<IDocumentRepository>();
        }

        public void RunUpdate(params ICanUpdateMyself[] repositories)
        {
            var metadata = _documentRepository.GetDocument<Metadata>("Playground.Data.Resources.Metadata.json");

            using (var db = _databaseProvider.CreateDatabase())
            {
                if (LastUpdate >= metadata.Date)
                    return;

                foreach (var repository in repositories)
                {
                    repository.UpdateDatabase(db, metadata, _documentRepository);
                }

                LastUpdate = metadata.Date;
            }
        }
    }
}
