using Playground.Models;
using System.Collections.Generic;

namespace Playground.Services
{
    public interface ICategoryService
    {
        IEnumerable<GradientCategory> GetCategories();

        IEnumerable<GradientTheme> GetThemes();
    }
}
