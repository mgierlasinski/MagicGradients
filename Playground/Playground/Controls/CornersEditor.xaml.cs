using MagicGradients.Masks;
using Xamarin.Forms;

namespace Playground.Controls
{
    public partial class CornersEditor
    {
        private bool _isUpdating;

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value),
            typeof(Corners), typeof(CornersEditor), default, BindingMode.TwoWay,
            propertyChanged: OnValueChanged);

        public Corners Value
        {
            get => (Corners)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public CornersEditor()
        {
            InitializeComponent();
        }

        private static void OnValueChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((CornersEditor)bindable).UpdateEditor();
        }

        private void Corner_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateValue();
        }

        private void UpdateValue()
        {
            if (_isUpdating)
                return;

            _isUpdating = true;

            Value = new Corners(LeftTop.Value, RightTop.Value, LeftBottom.Value, RightBottom.Value);

            _isUpdating = false;
        }

        private void UpdateEditor()
        {
            if (_isUpdating)
                return;

            _isUpdating = true;

            LeftTop.Value = Value.LeftTop;
            RightTop.Value = Value.RightTop;
            LeftBottom.Value = Value.LeftBottom;
            RightBottom.Value = Value.RightBottom;

            _isUpdating = false;
        }
    }
}