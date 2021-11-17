using System.Collections.Generic;

namespace Playground.Data.Repositories
{
    public interface IDocumentRepository
    {
        void SetupMapper();
        T GetDocument<T>(string fullPath);
        IEnumerable<T> GetDocumentCollection<T>(string nameSpace, params string[] files);
    }
}
