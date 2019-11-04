using System.Collections.Generic;

namespace Playground.Data.Repositories
{
    public interface IDocumentRepository
    {
        IEnumerable<T> GetDocumentCollection<T>(string nameSpace, string[] files);
    }
}
