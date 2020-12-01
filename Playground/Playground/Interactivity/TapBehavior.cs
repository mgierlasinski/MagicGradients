using System;
using System.Windows.Input;
using Playground.Extensions;
using Xamarin.Forms;

namespace Playground.Interactivity
{
    public class TapBehavior : BindableBehavior<View>
    {
        private IGestureRecognizer _tapGestureRecognizer;

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command), typeof(ICommand), typeof(TapBehavior));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter), typeof(object), typeof(TapBehavior));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public event EventHandler Clicked;

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);

            _tapGestureRecognizer = new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    bindable.Click(TriggerCommand);
                })
            };

            bindable.GestureRecognizers.Add(_tapGestureRecognizer);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.GestureRecognizers.Remove(_tapGestureRecognizer);
        }

        private void TriggerCommand()
        {
            Command?.Execute(CommandParameter);
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
