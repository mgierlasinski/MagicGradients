using PlaygroundMaui.Infrastructure;
using PlaygroundMaui.Models;

namespace PlaygroundMaui.ViewModels
{
    public class GalleryViewModel : INavigationAware<Category>
    {
        public string Name { get; private set; }

        public void Prepare(Category parameter)
        {
            Name = parameter.Name;
        }
    }
}
