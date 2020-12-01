using System.Windows.Input;
using Playground.Extensions;
using Xamarin.Forms;

namespace Playground.Controls
{
    public partial class ActionButton : Grid
	{
        private readonly TapGestureRecognizer _tapGestureRecognizer;
        
        public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(
            nameof(IconSource), typeof(ImageSource), typeof(ActionButton),
            propertyChanged: (bindable, oldValue, newValue) => (bindable as ActionButton)?.UpdateIcon());

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command), typeof(ICommand), typeof(ActionButton),
            propertyChanged: (bindable, oldValue, newValue) => (bindable as ActionButton)?.UpdateCommand());

        public static readonly BindableProperty IsAnimatedProperty = BindableProperty.Create(
            nameof(IsAnimated), typeof(bool), typeof(ActionButton), true,
            propertyChanged: (bindable, oldValue, newValue) => (bindable as ActionButton)?.UpdateAnimated());

        public ImageSource IconSource
        {
            get => (ImageSource)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public bool IsAnimated
        {
            get => (bool)GetValue(IsAnimatedProperty);
            set => SetValue(IsAnimatedProperty, value);
        }

        public ActionButton()
		{
			InitializeComponent();

            _tapGestureRecognizer = new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (IsAnimated)
                    {
                        this.Click(TriggerCommand);
                    }
                    else
                    {
                        TriggerCommand();
                    }
                })
            };
        }

        private void UpdateIcon()
        {
            Icon.Source = IconSource;
        }
        
        private void UpdateCommand()
        {
            if (Command != null)
            {
                EnableTapGestureRecognizer();
            }
            else if(!IsAnimated)
            {
                DisableTapGestureRecognizer();
            }
        }
        
        private void UpdateAnimated()
        {
            if (IsAnimated)
            {
                EnableTapGestureRecognizer();
            }
            else if(Command == null)
            {
                DisableTapGestureRecognizer();
            }
        }

        private void EnableTapGestureRecognizer()
        {
            if (!GestureRecognizers.Contains(_tapGestureRecognizer))
            {
                GestureRecognizers.Add(_tapGestureRecognizer);
            }
        }

        private void DisableTapGestureRecognizer()
        {
            if (GestureRecognizers.Contains(_tapGestureRecognizer))
            {
                GestureRecognizers.Remove(_tapGestureRecognizer);
            }
        }

        private void TriggerCommand()
        {
            Command?.Execute(null);
        }
    }
}