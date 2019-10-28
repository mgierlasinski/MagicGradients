using LiteDB;

namespace Playground.Data.Infrastructure
{
    public interface IDatabaseProvider
    {
        LiteDatabase CreateDatabase();
    }
}
