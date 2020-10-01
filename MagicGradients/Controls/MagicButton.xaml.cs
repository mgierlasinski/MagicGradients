using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MagicGradients.Controls
{
    [ContentProperty("Content")]
    public partial class MagicButton : TemplatedView
    {
        public static readonly BindableProperty ContentProperty = BindableProperty.Create(
            nameof(Content), typeof(View), typeof(MagicButton), null,
            propertyChanged: OnContentChanged);

        [TypeConverter(typeof(TextContentTypeConverter))]
        public View Content
        {
            get { return (View)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            View content = Content;
            ControlTemplate controlTemplate = ControlTemplate;

            if (content != null && controlTemplate != null)
            {
                SetInheritedBindingContext(content, BindingContext);
            }
        }

        protected override void OnApplyTemplate()
        {
            View content = Content;
            ControlTemplate controlTemplate = ControlTemplate;

            if (content != null && controlTemplate != null)
            {
                SetInheritedBindingContext(content, BindingContext);
            }
        }

        public static void OnContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newElement = (Element)newValue;
            if (newElement != null)
            {
                BindableObject.SetInheritedBindingContext(newElement, bindable.BindingContext);
            }
        }

        //public static readonly BindableProperty TextProperty = BindableProperty.Create(
        //    propertyName: nameof(Text),
        //    returnType: typeof(View),
        //    declaringType: typeof(MagicButton),
        //    propertyChanged: PropertyChanged);

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: nameof(FontSize),
            returnType: typeof(double),
            declaringType: typeof(MagicButton),
            defaultValue: (double) 18);

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            propertyName: nameof(FontFamily),
            returnType: typeof(string),
            declaringType: typeof(MagicButton));

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: nameof(TextColor),
            returnType: typeof(Color),
            declaringType: typeof(MagicButton),
            defaultValue: Color.White);

        public static readonly BindableProperty GradientSourceProperty = BindableProperty.Create(
            propertyName: nameof(GradientSource),
            returnType: typeof(IGradientSource),
            declaringType: typeof(MagicButton));

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            propertyName: nameof(Command),
            returnType: typeof(ICommand),
            declaringType: typeof(MagicButton));

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            propertyName: nameof(CornerRadius),
            returnType: typeof(float),
            declaringType: typeof(MagicButton),
            defaultValue: 15f);

        //[TypeConverter(typeof(TextContentTypeConverter))]
        //public View Text
        //{
        //    get => (View)GetValue(TextProperty);
        //    set => SetValue(TextProperty, value);
        //}

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

        public MagicButton()
        {
            InitializeComponent();

            var actionButton = (ContentPresenter)GetTemplateChild("ContentPresenter");
            ((TapGestureRecognizer)actionButton.GestureRecognizers.First()).SetBinding(TapGestureRecognizer.CommandProperty, new Binding(nameof(Command), source: this));

            var gradientView = (GradientView)GetTemplateChild("GradientView");
            gradientView.SetBinding(GradientView.GradientSourceProperty, new Binding(nameof(GradientSource), source: this));

            var topFrame = (Frame)GetTemplateChild("TopFrame");
            topFrame.SetBinding(Frame.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
        }
    }
}