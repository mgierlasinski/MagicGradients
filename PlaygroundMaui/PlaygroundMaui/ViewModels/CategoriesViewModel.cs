using PlaygroundMaui.Models;
using PlaygroundMaui.Resources;
using System.Collections.Generic;

namespace PlaygroundMaui.ViewModels
{
    public class CategoriesViewModel
    {
        public List<Category> Categories { get; }

        public CategoriesViewModel()
        {
            var reader = new DocumentReader();
            Categories = reader.GetDocument<List<Category>>("PlaygroundMaui.Resources.Categories.json");
        }
    }
}
