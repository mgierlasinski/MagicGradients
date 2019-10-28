using LiteDB;
using System.Collections.Generic;

namespace Playground.Data.Repositories
{
    public interface IDocumentRepository
    {
        IEnumerable<BsonValue> GetInitialValues();
    }
}
