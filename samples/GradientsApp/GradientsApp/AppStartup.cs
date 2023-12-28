using Playground.Data.Infrastructure;
using Playground.Data.Repositories;

namespace GradientsApp;

public class AppStartup
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IGradientRepository _gradientRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IDatabaseUpdater _databaseUpdater;

    public AppStartup(
        IDocumentRepository documentRepository, 
        IGradientRepository gradientRepository,
        ICategoryRepository categoryRepository,
        IDatabaseUpdater databaseUpdater)
    {
        _documentRepository = documentRepository;
        _gradientRepository = gradientRepository;
        _categoryRepository = categoryRepository;
        _databaseUpdater = databaseUpdater;
    }

    public void Run()
    {
        _documentRepository.SetupMapper();
        _databaseUpdater.RunUpdate(_gradientRepository, _categoryRepository);
    }
}