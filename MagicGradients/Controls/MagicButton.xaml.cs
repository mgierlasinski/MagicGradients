using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MagicGradients.Controls
{
    [ContentProperty("Content")]
    public partial class MagicButton : TemplatedView
    {
        public static readonly BindableProperty ContentProperty = BindableProperty.Create(
            nameof(Content), typeof(object), typeof(MagicButton), null,
            propertyChanged: OnContentChanged, coerceValue: CoerceContent);

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            nameof(FontSize), typeof(double), typeof(MagicButton), (double)18, 
            propertyChanged: (b, x, y) => ((MagicButton)b).UpdateFontSize());

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            nameof(FontFamily), typeof(string), typeof(MagicButton),
            propertyChanged: (b, x, y) => ((MagicButton)b).UpdateFontFamily());

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor), typeof(Color), typeof(MagicButton), Color.White,
            propertyChanged: (b, x, y) => ((MagicButton)b).UpdateTextColor());

        public static readonly BindableProperty GradientSourceProperty = BindableProperty.Create(
            nameof(GradientSource), typeof(IGradientSource), typeof(MagicButton));

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command), typeof(ICommand), typeof(MagicButton));

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius), typeof(float), typeof(MagicButton), 15f);

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(
            nameof(HasShadow), typeof(bool), typeof(MagicButton));

        [TypeConverter(typeof(TextContentTypeConverter))]
        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double) GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public string FontFamily
        {
            get => (string) GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public Color TextColor
        {
            get => (Color) GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public IGradientSource GradientSource
        {
            get => (IGradientSource) GetValue(GradientSourceProperty);
            set => SetValue(GradientSourceProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public float CornerRadius
        {
            get => (float) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public MagicButton()
        {
            InitializeComponent();

            var gradientView = (GradientView)GetTemplateChild("GradientView");
            gradientView.SetBinding(GradientView.GradientSourceProperty, new Binding(nameof(GradientSource), source: this));

            var border = (Frame)GetTemplateChild("BorderFrame");
            border.SetBinding(Frame.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
            border.SetBinding(Frame.HasShadowProperty, new Binding(nameof(HasShadow), source: this));

            var coverButton = (Button)GetTemplateChild("CoverButton");
            coverButton.SetBinding(Button.CommandProperty, new Binding(nameof(Command), source: this));
            //todo coverButton.SetBinding(Button.CommandParameterProperty);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            View content = (View)Content;
            ControlTemplate controlTemplate = ControlTemplate;

            if (content != null && controlTemplate != null)
            {
                SetInheritedBindingContext(content, BindingContext);
            }
        }

        protected override void OnApplyTemplate()
        {
            View content = (View)Content;
            ControlTemplate controlTemplate = ControlTemplate;

            if (content != null && controlTemplate != null)
            {
                SetInheritedBindingContext(content, BindingContext);
            }
        }

        private static void OnContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newElement = (Element)newValue;
            if (newElement != null)
            {
                SetInheritedBindingContext(newElement, bindable.BindingContext);

                var button = (MagicButton)bindable;
                button.UpdateFontSize();
                button.UpdateFontFamily();
                button.UpdateTextColor();
            }
        }

        private static object CoerceContent(BindableObject bindable, object value)
        {
            if (value is View view)
                return view;

            if (value != null)
                return new TextContent { Text = value.ToString() };

            return null;
        }

        private void UpdateFontSize()
        {
            if (Content is Label label)
            {
                label.FontSize = FontSize;
            }
        }

        private void UpdateFontFamily()
        {
            if (Content is Label label)
            {
                label.FontFamily = FontFamily;
            }
        }

        private void UpdateTextColor()
        {
            if (Content is Label label)
            {
                label.TextColor = TextColor;
            }
        }
    }
}