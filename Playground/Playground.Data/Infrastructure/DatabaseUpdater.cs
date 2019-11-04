using Playground.Data.Models;
using Playground.Data.Repositories;
using Xamarin.Forms;

namespace Playground.Data.Infrastructure
{
    public class DatabaseUpdater : IDatabaseUpdater
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly IGradientRepository _gradientRepository;

        public DatabaseUpdater()
        {
            _databaseProvider = DependencyService.Get<IDatabaseProvider>();
            _gradientRepository = DependencyService.Get<IGradientRepository>();
        }

        public void RunUpdate()
        {
            // TODO: get from json
            var metadata = new Metadata
            {
                Version = 1,
                NameSpace = "Playground.Data.Resources",
                GradientFiles = new []  
                {
                    "Angular.json",
                    "Burst.json",
                    "Checkered.json",
                    "Retro.json",
                    "Standard.json",
                    "Stripes.json"
                }
            }; 

            using (var db = _databaseProvider.CreateDatabase())
            {
                if (db.Engine.UserVersion >= metadata.Version)
                    return;

                _gradientRepository.UpdateDatabase(db, metadata);

                db.Engine.UserVersion = metadata.Version;
            }
        }
    }
}
