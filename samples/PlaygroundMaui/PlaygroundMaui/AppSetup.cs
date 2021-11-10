using Playground.Data.Infrastructure;
using Playground.Data.Repositories;

namespace PlaygroundMaui
{
    public static class AppSetup
    {
        public static void ConfigureAndRun()
        {
            InitializeDatabase();
        }

        public static void InitializeDatabase()
        {
            var provider = new DatabaseProvider();
            var repository = new DocumentRepository();
            repository.SetupMapper();
            
            var repositories = new ICanUpdateMyself[]
            {
                new GradientRepository(provider),
                new CategoryRepository(provider)
            };

            new DatabaseUpdater(provider, repository).RunUpdate(repositories);
        }
    }
}
