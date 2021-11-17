using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using Microsoft.Toolkit.Wpf.UI.Controls;
using System;
using System.Threading.Tasks;

namespace MagicCrawler.Services
{
    public class HtmlLoader
    {
        private WebView _webView;
        private TaskCompletionSource<string> _loadCompletion;

        public void Initialize(WebView webView)
        {
            _webView = webView;
            _webView.NavigationCompleted += WebViewOnNavigationCompleted;
        }

        public Task<string> LoadAsync(string url)
        {
            _loadCompletion = new TaskCompletionSource<string>();
            _webView.Navigate(new Uri(url));

            return _loadCompletion.Task;
        }

        private async void WebViewOnNavigationCompleted(object sender, WebViewControlNavigationCompletedEventArgs e)
        {
            if (_loadCompletion == null)
                return;

            var html = await _webView.InvokeScriptAsync("eval", "document.documentElement.outerHTML;");
            _loadCompletion.SetResult(html);
        }
    }
}
