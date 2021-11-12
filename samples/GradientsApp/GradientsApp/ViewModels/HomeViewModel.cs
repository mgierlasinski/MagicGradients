using GradientsApp.Infrastructure;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;

namespace GradientsApp.ViewModels
{
    public class HomeViewModel
    {
        public IAsyncCommand LinearCommand { get; }
        public IAsyncCommand GalleryCommand { get; }

        public HomeViewModel(INavigationService navigationService)
        {
            LinearCommand = new AsyncCommand(() => navigationService.NavigateTo(AppRoutes.Linear));
            GalleryCommand = new AsyncCommand(() => navigationService.NavigateTo(AppRoutes.Categories));
        }
    }
}
