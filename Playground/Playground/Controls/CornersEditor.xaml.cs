using MagicGradients;
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

            TopLeft.Value = Dimensions.Prop(0.25d, 0.25d);
            TopRight.Value = Dimensions.Prop(0.25d, 0.25d);
            BottomRight.Value = Dimensions.Prop(0.25d, 0.25d);
            BottomLeft.Value = Dimensions.Prop(0.25d, 0.25d);
        }

        private static void OnValueChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((CornersEditor)bindable).UpdateEditor();
        }

        private void OnEditorChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateValue();
        }

        private void UpdateValue()
        {
            if (_isUpdating)
                return;

            _isUpdating = true;

            Value = new Corners(TopLeft.Value, TopRight.Value, BottomLeft.Value, BottomRight.Value);

            _isUpdating = false;
        }

        private void UpdateEditor()
        {
            if (_isUpdating)
                return;

            _isUpdating = true;

            TopLeft.Value = Value.TopLeft;
            TopRight.Value = Value.TopRight;
            BottomLeft.Value = Value.BottomLeft;
            BottomRight.Value = Value.BottomRight;

            _isUpdating = false;
        }
    }
}