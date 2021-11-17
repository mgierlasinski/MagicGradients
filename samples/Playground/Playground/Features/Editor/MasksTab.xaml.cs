using System;
using Xamarin.Forms;

namespace Playground.Features.Editor
{
    public partial class MasksTab : StackLayout
    {
        public MasksTab()
        {
            InitializeComponent();
        }

        private void FontSize_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue);
            ((Slider)sender).Value = newStep;
        }
    }
}