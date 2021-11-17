using System.Collections.Generic;

namespace Playground.Features.Gallery.Models
{
    public class CategoryGroup : List<CategoryItem>
    {
        public string Name { get; }

        public CategoryGroup(string name, IEnumerable<CategoryItem> categories) 
            : base(categories)
        {
            Name = name;
        }
    }
}
