using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GradientParser.Services
{
    public class Dialog
    {
        public async Task ShowAlert(string content, string title = "", string closeButtonText = "Ok")
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = closeButtonText
            };
            await dialog.ShowAsync();
        }
    }
}
