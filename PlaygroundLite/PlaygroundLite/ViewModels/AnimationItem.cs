using PlaygroundLite.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace PlaygroundLite.ViewModels
{
    public class AnimationItem : ObservableObject
    {
        public ICommand PlayCommand { get; }

        public string Title { get; }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value, () =>
            {
                RaisePropertyChanged(nameof(Text));
            });
        }

        public string Text => IsRunning ? "Stop" : "Play";
        
        public AnimationItem(string title)
        {
            Title = title;
            PlayCommand = new Command(() => IsRunning = !IsRunning);
        }
    }
}
