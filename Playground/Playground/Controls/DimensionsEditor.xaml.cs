using System;
using MagicGradients;
using Xamarin.Forms;

namespace Playground.Controls
{
    public partial class DimensionsEditor : Grid
    {
        private bool _isUpdating;

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value),
            typeof(Dimensions), typeof(DimensionsEditor), Dimensions.Prop(1,1), BindingMode.TwoWay,
            propertyChanged: OnValueChanged);

        public Dimensions Value
        {
            get => (Dimensions)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public DimensionsEditor()
        {
            InitializeComponent();

            Type.SelectedItem = OffsetType.Proportional;
            SizeScale.Value = 1;
            SizeWidth.Text = "100";
            SizeHeight.Text = "100";
        }

        private static void OnValueChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((DimensionsEditor)bindable).UpdateEditor();
        }

        private void SizeScale_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateValue();
        }

        private void SizePixels_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateValue();
        }

        private void Type_OnSelectedItemChanged(object sender, EventArgs e)
        {
            UpdateState();
            UpdateValue();
        }

        private void UpdateState()
        {
            VisualStateManager.GoToState(this,
                (OffsetType)Type.SelectedItem == OffsetType.Absolute ? "Pixel" : "Proportion");
        }

        private void UpdateValue()
        {
            if (_isUpdating)
                return;

            _isUpdating = true;

            if ((OffsetType)Type.SelectedItem == OffsetType.Absolute)
            {
                if (!double.TryParse(SizeWidth.Text, out var width))
                    width = 0;

                if (!double.TryParse(SizeHeight.Text, out var height))
                    height = 0;

                Value = Dimensions.Abs(width, height);
            }
            else
            {
                Value = Dimensions.Prop(SizeScale.Value, SizeScale.Value);
            }

            _isUpdating = false;
        }

        private void UpdateEditor()
        {
            if (_isUpdating)
                return;

            if (Value.IsZero)
                return;

            _isUpdating = true;
            Type.SelectedItem = Value.Width.Type;

            if (Value.Width.Type == OffsetType.Absolute)
            {
                SizeWidth.Text = $"{Value.Width.Value}";
                SizeHeight.Text = $"{Value.Height.Value}";
            }
            else
            {
                SizeScale.Value = Value.Width.Value;
            }

            _isUpdating = false;
        }
    }
}