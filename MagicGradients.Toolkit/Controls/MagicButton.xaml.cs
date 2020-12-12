using SkiaSharp.Views.Forms;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MagicGradients.Toolkit.Controls
{
    [ContentProperty("Content")]
    public partial class MagicButton : TemplatedView
    {
        private const string PressedState = "Pressed";
        private const string HoveredState = "Hovered";
        private const string TemplateRootName = "TemplateRoot";
        private const string GradientViewName = "GradientView";
        private const string OverlayName = "Overlay";

        private Frame _templateRoot;
        protected Frame TemplateRoot => _templateRoot ??= (Frame)GetTemplateChild(TemplateRootName);

        public static readonly BindableProperty ContentProperty = BindableProperty.Create(
            nameof(Content), typeof(object), typeof(MagicButton), null,
            propertyChanged: OnContentChanged, coerceValue: CoerceContent);

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            nameof(Color), typeof(Color), typeof(MagicButton), Color.Default);

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            nameof(FontSize), typeof(double), typeof(MagicButton), (double)18, 
            propertyChanged: (b, x, y) => ((MagicButton)b).UpdateFontSize());

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            nameof(FontFamily), typeof(string), typeof(MagicButton),
            propertyChanged: (b, x, y) => ((MagicButton)b).UpdateFontFamily());

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor), typeof(Color), typeof(MagicButton), Color.Black,
            propertyChanged: (b, x, y) => ((MagicButton)b).UpdateTextColor());

        public static readonly BindableProperty GradientSourceProperty = BindableProperty.Create(
            nameof(GradientSource), typeof(IGradientSource), typeof(MagicButton));

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command), typeof(ICommand), typeof(MagicButton));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter), typeof(object), typeof(MagicButton));

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius), typeof(float), typeof(MagicButton), 15f);

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(
            nameof(HasShadow), typeof(bool), typeof(MagicButton));

        public static readonly BindableProperty GradientSizeProperty = BindableProperty.Create(nameof(GradientSize),
            typeof(Dimensions), typeof(GradientView));

        public static readonly BindableProperty GradientRepeatProperty = BindableProperty.Create(nameof(GradientRepeat),
            typeof(BackgroundRepeat), typeof(GradientView));
        
        [TypeConverter(typeof(TextContentTypeConverter))]
        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
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

        [TypeConverter(typeof(ColorTypeConverter))]
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
        
        public Dimensions GradientSize
        {
            get => (Dimensions)GetValue(GradientSizeProperty);
            set => SetValue(GradientSizeProperty, value);
        }

        public BackgroundRepeat GradientRepeat
        {
            get => (BackgroundRepeat)GetValue(GradientRepeatProperty);
            set => SetValue(GradientRepeatProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
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

        static MagicButton()
        {
            StyleSheets.RegisterStyle("color", typeof(MagicButton), nameof(TextColorProperty));
            StyleSheets.RegisterStyle("border-radius", typeof(MagicButton), nameof(CornerRadiusProperty));
            StyleSheets.RegisterStyle("background", typeof(MagicButton), nameof(GradientSourceProperty));
            StyleSheets.RegisterStyle("background-size", typeof(MagicButton), nameof(GradientSizeProperty));
            StyleSheets.RegisterStyle("background-repeat", typeof(MagicButton), nameof(GradientRepeatProperty));
        }

        public MagicButton()
        {
            InitializeComponent();
        }

        private void ExtendNameScope()
        {
            var nameScope = NameScope.GetNameScope(this);
            nameScope.RegisterName(TemplateRootName, TemplateRoot);
            nameScope.RegisterName(GradientViewName, (GradientView)GetTemplateChild(GradientViewName));
            nameScope.RegisterName(OverlayName, (BoxView)GetTemplateChild(OverlayName));
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsEnabled))
            {
                GoToDefaultState();
            }
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

            ExtendNameScope();
            GoToDefaultState();
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

        private void GoToDefaultState()
        {
            GoToState(IsEnabled ?
                VisualStateManager.CommonStates.Normal :
                VisualStateManager.CommonStates.Disabled);
        }

        private void ExecuteCommand()
        {
            if(Command != null && Command.CanExecute(CommandParameter))
                Command.Execute(CommandParameter);
        }

        private void GradientView_Touch(object sender, SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    if (IsEnabled)
                    {
                        GoToState(PressedState);
                    }
                    break;
                case SKTouchAction.Released:
                    GoToState(VisualStateManager.CommonStates.Normal);
                    ExecuteCommand();
                    break;
                case SKTouchAction.Cancelled:
                    GoToState(VisualStateManager.CommonStates.Normal);
                    break;
                case SKTouchAction.Entered:
                    GoToState(HoveredState);
                    break;
                case SKTouchAction.Exited:
                    GoToDefaultState();
                    break;
            }
            
            e.Handled = true;
        }

        private void GoToState(string stateName)
        {
            if (TemplateRoot != null)
            {
                VisualStateManager.GoToState(TemplateRoot, stateName);
            }
            VisualStateManager.GoToState(this, stateName);
        }
    }
}