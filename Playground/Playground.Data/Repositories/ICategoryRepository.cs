using Playground.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Playground.Data.Repositories
{
    public interface ICategoryRepository : ICanUpdateMyself
    {
        List<Category> GetCategories();
        List<IGrouping<string, Category>> GetGroupedCategories();
        List<Theme> GetThemes();
    }
}
