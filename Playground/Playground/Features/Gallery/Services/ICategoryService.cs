using System.Collections.Generic;
using Playground.Features.Gallery.Models;

namespace Playground.Features.Gallery.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryItem> GetCategories();
        IEnumerable<CategoryGroup> GetGroupedCategories();
        IEnumerable<ThemeItem> GetThemes();
    }
}
