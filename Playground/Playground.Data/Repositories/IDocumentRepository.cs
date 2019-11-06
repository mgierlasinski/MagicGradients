using System.Collections.Generic;

namespace Playground.Data.Repositories
{
    public interface IDocumentRepository
    {
        T GetDocument<T>(string fullPath);

        IEnumerable<T> GetDocumentCollection<T>(string nameSpace, string[] files);
    }
}
