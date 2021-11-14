using System;
using Xamarin.Forms;

namespace Playground.Controls
{
    public partial class ColorEditor
    {
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value),
            typeof(Color), typeof(ColorEditor), Color.Default, BindingMode.TwoWay,
            propertyChanged: OnValueChanged);

        public Color Value
        {
            get => (Color)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public ColorEditor()
        {
            InitializeComponent();

            Type.SelectedItem = ColorSpace.Hex;
        }

        private static void OnValueChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((ColorEditor)bindable).UpdateEditor();
        }

        private void Type_OnSelectedItemChanged(object sender, EventArgs e)
        {
            UpdateState();
            UpdateEditor();
        }

        private bool _isUpdating;

        private void ColorHex_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateValue();
        }

        private void ColorRgba_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateValue();
        }

        private void UpdateState()
        {
            VisualStateManager.GoToState(this,
                (ColorSpace)Type.SelectedItem == ColorSpace.Hex ? "Hex" : "Rgba");
        }

        private void UpdateValue()
        {
            if (_isUpdating)
                return;

            _isUpdating = true;

            if ((ColorSpace)Type.SelectedItem == ColorSpace.Rgba)
            {
                Value = Color.FromRgba(
                    StringToChannel(ColorR.Text), 
                    StringToChannel(ColorG.Text), 
                    StringToChannel(ColorB.Text), 
                    StringToChannel(ColorA.Text));
            }
            else
            {
                Value = Color.FromHex(ColorHex.Text);
            }

            _isUpdating = false;
        }

        private void UpdateEditor()
        {
            if (_isUpdating)
                return;

            _isUpdating = true;
            ColorHex.Text = Value.ToHex();
            ColorR.Text = ChannelToString(Value.R);
            ColorG.Text = ChannelToString(Value.G);
            ColorB.Text = ChannelToString(Value.B);
            ColorA.Text = ChannelToString(Value.A);
            _isUpdating = false;
        }

        private string ChannelToString(double channel)
        {
            return $"{Math.Floor(channel * 255)}";
        }

        private int StringToChannel(string channelStr)
        {
            if (int.TryParse(channelStr, out var ch))
            {
                return ch;
            }

            return 0;
        }
    }

    public enum ColorSpace
    {
        Hex, Rgba
    }
}