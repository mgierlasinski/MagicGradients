using LiteDB;
using Playground.Data.Models;

namespace Playground.Data.Repositories
{
    public interface ICanUpdateMyself
    {
        void UpdateDatabase(LiteDatabase db, Metadata metadata, IDocumentRepository documentRepository);
    }
}
