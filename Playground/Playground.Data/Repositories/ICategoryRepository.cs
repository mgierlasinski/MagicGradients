using Playground.Data.Models;
using System.Collections.Generic;

namespace Playground.Data.Repositories
{
    public interface ICategoryRepository : ICanUpdateMyself
    {
        IEnumerable<Category> GetCategories();
    }
}
