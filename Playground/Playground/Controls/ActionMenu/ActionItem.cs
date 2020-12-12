using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Playground.Controls
{
    public class ActionItem : BindableObject
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command), typeof(ICommand), typeof(ActionItem));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public ImageSource IconSource { get; set; }
        public string Text { get; set; }
        public bool IsVisible { get; set; } = true;
    }

    public class ActionItemCollection : List<ActionItem>
    {

    }
}
