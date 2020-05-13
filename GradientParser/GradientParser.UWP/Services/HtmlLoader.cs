using System;
using Windows.UI.Xaml.Controls;

namespace GradientParser.Services
{
    public class HtmlLoader
    {
        readonly WebView _webView = new WebView();
        string _siteHtML = null;

        public event EventHandler<string> HtmlLoaded;

        public void StartLoading(string url)
        {
            _siteHtML = null;
            _webView.Navigate(new Uri(url));
            _webView.NavigationCompleted += webView_NavigationCompletedAsync;
        }

        private void OnHtmlLoaded()
        {
            HtmlLoaded?.Invoke(this, _siteHtML);
        }

        private async void webView_NavigationCompletedAsync(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(2)).Wait();
            _siteHtML = await _webView.InvokeScriptAsync("eval", new string[] { "document.documentElement.outerHTML;" });
            OnHtmlLoaded();
        }
    }
}
