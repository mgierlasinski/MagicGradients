using GradientParser.ViewModels;
using Windows.UI.Xaml.Controls;

using static System.String;

namespace GradientParser
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
        }
    }
}
