using GradientsApp.Infrastructure;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;

namespace GradientsApp.ViewModels
{
    public class HomeViewModel
    {
        public IAsyncCommand<string> NavigateCommand { get; }

        public HomeViewModel(INavigationService navigationService)
        {
            NavigateCommand = new AsyncCommand<string>(navigationService.NavigateTo);
        }
    }
}
