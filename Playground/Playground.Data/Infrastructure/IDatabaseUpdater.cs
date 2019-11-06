using Playground.Data.Repositories;

namespace Playground.Data.Infrastructure
{
    public interface IDatabaseUpdater
    {
        void RunUpdate(params ICanUpdateMyself[] repositories);
    }
}
