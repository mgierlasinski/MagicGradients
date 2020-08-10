using MagicCrawler.ViewModels;
using System.Windows;

namespace MagicCrawler.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ((MainViewModel)DataContext).Initialize();
            ((MainViewModel)DataContext).HtmlLoader.Initialize(WebView);
        }
    }
}
