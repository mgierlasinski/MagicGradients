using System.Collections.Generic;
using Playground.Features.Gallery.Models;

namespace Playground.Features.Gallery.Services
{
    public interface ICategoryService
    {
        IEnumerable<GradientCategory> GetCategories();
        IEnumerable<GradientTheme> GetThemes();
    }
}
