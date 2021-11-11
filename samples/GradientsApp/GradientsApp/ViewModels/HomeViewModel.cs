using GradientsApp.Infrastructure;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using System.Threading.Tasks;

namespace GradientsApp.ViewModels
{
    public class HomeViewModel
    {
        private readonly INavigationService _navigationService;

        public IAsyncCommand LinearCommand { get; }

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            LinearCommand = new AsyncCommand(LinearAction);
        }

        private Task LinearAction()
        {
            return _navigationService.NavigateTo("Linear", "test string");
        }
    }
}
