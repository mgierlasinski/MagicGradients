using Playground.Data.Models;
using Playground.Data.Repositories;
using Xamarin.Forms;

namespace Playground.Data.Infrastructure
{
    public class DatabaseUpdater : IDatabaseUpdater
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly IDocumentRepository _documentRepository;

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
                if (db.Engine.UserVersion >= metadata.Version)
                    return;

                foreach (var repository in repositories)
                {
                    repository.UpdateDatabase(db, metadata, _documentRepository);
                }

                db.Engine.UserVersion = metadata.Version;
            }
        }
    }
}
