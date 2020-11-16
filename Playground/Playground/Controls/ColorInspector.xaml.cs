using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using MagicGradients;
using Playground.Converters;
using Xamarin.Forms;
using GradientStop = MagicGradients.GradientStop;

namespace Playground.Controls
{
    public partial class ColorInspector : ContentView
    {
        private readonly PanGestureRecognizer _panRecognizer;

        public ColorInspectorViewModel ViewModel { get; }

        public static readonly BindableProperty GradientProperty = BindableProperty.Create(nameof(Gradient),
            typeof(Gradient), typeof(ColorInspector), propertyChanged: OnGradientChanged);

        public Gradient Gradient
        {
            get => (Gradient)GetValue(GradientProperty);
            set => SetValue(GradientProperty, value);
        }

        public ColorInspector()
        {
            InitializeComponent();

            _panRecognizer = new PanGestureRecognizer();
            _panRecognizer.PanUpdated += PanUpdated;

            //AbsoluteLayout.GestureRecognizers.Add(_panRecognizer);
            AbsoluteLayout.SizeChanged += AbsoluteLayoutOnSizeChanged;
            ViewModel = (ColorInspectorViewModel)LayoutRoot.BindingContext;
            ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void AbsoluteLayoutOnSizeChanged(object sender, EventArgs e)
        {
            _width = AbsoluteLayout.Width;
        }

        static void OnGradientChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var inspector = (ColorInspector)bindable;
            var gradient = (Gradient)newValue;

            inspector.ViewModel.OnGradientChanged(gradient);
            inspector.ColorSpectrum.GradientSource = new LinearGradient { Angle = 270, Stops = gradient.Stops };
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Offset")
            {
                UpdateChildrenPositions();
            }
        }

        private void UpdateChildrenPositions()
        {
            Gradient.Measure(0, 0);

            for (var i = 0; i < AbsoluteLayout.Children.Count; i++)
            {
                SetPosition(AbsoluteLayout.Children[i], Gradient.Stops[i].RenderOffset);
            }

            Gradient.InvalidateCanvas();
        }

        private void Slider_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateChildrenPositions();
        }

        private void AbsoluteLayout_OnChildAdded(object sender, ElementEventArgs e)
        {
            var view = (View) e.Element;
            var stop = (GradientStop)view.BindingContext;

            AbsoluteLayout.SetLayoutFlags(view, AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.HeightProportional);
            SetPosition(view, stop.RenderOffset);
            
            view.GestureRecognizers.Add(_panRecognizer);
        }

        private void AbsoluteLayout_OnChildRemoved(object sender, ElementEventArgs e)
        {
            ((View)e.Element).GestureRecognizers.Remove(_panRecognizer);
        }

        double _width;
        double _prevTotalX;
        double _prevTotalY;
        private int _touchId;

        private void PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    _touchId = e.GestureId;
                    _prevTotalX = 0;
                    _prevTotalY = 0;
                    break;
                case GestureStatus.Running:
                    if (e.GestureId == _touchId)
                    {
                        //if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.macOS)
                        //{
                        var deltaX = e.TotalX - _prevTotalX;
                        var deltaY = e.TotalY - _prevTotalY;

                        UpdateLayout((BindableObject) sender, deltaX, deltaY);

                        _prevTotalX = e.TotalX;
                        _prevTotalY = e.TotalY;
                        //}
                        //else
                        //    UpdateLayout((BindableObject)sender, e.TotalX, e.TotalY);
                    }

                    break;
                case GestureStatus.Completed:
                case GestureStatus.Canceled:
                    break;
            }
        }

        private void UpdateLayout(BindableObject sender, double offsetX = 0, double offsetY = 0)
        {
            var deltaX = offsetX / _width;
            var newX = AbsoluteLayout.GetLayoutBounds(sender).X + deltaX;

            SetPosition(sender, newX);

            ((GradientStop)sender.BindingContext).Offset = Offset.Prop(newX);

            Gradient.InvalidateCanvas();
        }

        private void SetPosition(BindableObject target, double position)
        {
            var value = new Rectangle(position, 0, 60, 1);
            AbsoluteLayout.SetLayoutBounds(target, value);
        }
    }
}