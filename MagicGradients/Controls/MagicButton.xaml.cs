using System.Windows.Input;
using Xamarin.Forms;

namespace MagicGradients.Controls
{
     public partial class MagicButton : ContentView
    {
        public MagicButton()
        {
            InitializeComponent();

            ActionButton.SetBinding(Button.TextProperty, new Binding(nameof(Text), source: this));
            ActionButton.SetBinding(Button.FontSizeProperty, new Binding(nameof(FontSize), source: this));
            ActionButton.SetBinding(Button.TextColorProperty, new Binding(nameof(TextColor), source: this));
            ActionButton.SetBinding(Button.CommandProperty, new Binding(nameof(Command), source: this));
            ActionButton.SetBinding(Button.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
            
            GradientView.SetBinding(GradientView.GradientSourceProperty, new Binding(nameof(GradientSource), source: this));

            TopFrame.SetBinding(Frame.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
        }

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(MagicButton),
            defaultValue: default,
            defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion Text

        #region FontSize

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: nameof(FontSize),
            returnType: typeof(double),
            declaringType: typeof(MagicButton),
            defaultValue: (double)18,
            defaultBindingMode: BindingMode.TwoWay);

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        #endregion FontSize
        
        #region FontFamily

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            propertyName: nameof(FontFamily),
            returnType: typeof(string),
            declaringType: typeof(MagicButton),
            defaultValue: default,
            defaultBindingMode: BindingMode.TwoWay);

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        #endregion FontFamily

        #region TextColor

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: nameof(TextColor),
            returnType: typeof(Color),
            declaringType: typeof(MagicButton),
            defaultValue: Color.White,
            defaultBindingMode: BindingMode.TwoWay);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        #endregion TextColor

        #region GradientSource

        public static readonly BindableProperty GradientSourceProperty = BindableProperty.Create(
            propertyName: nameof(GradientSource),
            returnType: typeof(IGradientSource),
            declaringType: typeof(MagicButton),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

        public IGradientSource GradientSource
        {
            get => (IGradientSource)GetValue(GradientSourceProperty);
            set => SetValue(GradientSourceProperty, value);
        }

        #endregion GradientSource

        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            propertyName: nameof(Command),
            returnType: typeof(ICommand),
            declaringType: typeof(MagicButton),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion Command

        #region CornerRadius

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            propertyName: nameof(CornerRadius),
            returnType: typeof(float),
            declaringType: typeof(MagicButton),
            defaultValue: 15f,
            defaultBindingMode: BindingMode.TwoWay);

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        #endregion CornerRadius
    }
}