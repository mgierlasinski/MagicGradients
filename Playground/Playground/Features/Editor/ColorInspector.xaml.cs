using System;
using System.Linq;
using MagicGradients;
using Xamarin.Forms;
using GradientStop = MagicGradients.GradientStop;

namespace Playground.Features.Editor
{
    public partial class ColorInspector : ContentView
    {
        private readonly PanGestureRecognizer _panRecognizer;
        private readonly TapGestureRecognizer _tapRecognizer;

        private ColorSpectrumGradient _spectrum;

        private double _width;
        private double _height;
        private double _prevTotalX;
        private int _touchId;

        public static readonly BindableProperty GradientProperty = BindableProperty.Create(nameof(Gradient),
            typeof(Gradient), typeof(ColorInspector), propertyChanged: OnGradientChanged);

        public Gradient Gradient
        {
            get => (Gradient)GetValue(GradientProperty);
            set => SetValue(GradientProperty, value);
        }

        public static readonly BindableProperty SelectedStopProperty = BindableProperty.Create(nameof(SelectedStop),
            typeof(GradientStop), typeof(ColorInspector));

        public GradientStop SelectedStop
        {
            get => (GradientStop)GetValue(SelectedStopProperty);
            set => SetValue(SelectedStopProperty, value);
        }

        public ColorInspector()
        {
            InitializeComponent();

            _panRecognizer = new PanGestureRecognizer();
            _panRecognizer.PanUpdated += PanUpdated;

            _tapRecognizer = new TapGestureRecognizer();
            _tapRecognizer.Tapped += OnTapped;
        }

        static void OnGradientChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ColorInspector)bindable).CreateSpectrum((Gradient)newValue);
        }

        public void CreateSpectrum(Gradient gradient)
        {
            _spectrum = new ColorSpectrumGradient(gradient);
            ColorSpectrum.GradientSource = _spectrum;

            BindableLayout.SetItemsSource(AbsoluteLayout, _spectrum.Stops);
            SelectStop((GradientStopClone)_spectrum.Stops.FirstOrDefault());
        }

        private void AbsoluteLayout_OnSizeChanged(object sender, EventArgs e)
        {
            _width = AbsoluteLayout.Width;
            _height = AbsoluteLayout.Height;
        }
        
        private void AbsoluteLayout_OnChildAdded(object sender, ElementEventArgs e)
        {
            var view = (View)e.Element;
            
            view.GestureRecognizers.Add(_panRecognizer);
            view.GestureRecognizers.Add(_tapRecognizer);
        }

        private void AbsoluteLayout_OnChildRemoved(object sender, ElementEventArgs e)
        {
            ((View)e.Element).GestureRecognizers.Clear();
        }

        private void OnTapped(object sender, EventArgs e)
        {
            var bindable = (VisualElement)sender;
            var stop = (GradientStopClone)bindable.BindingContext;

            SelectStop(stop);
        }

        private void SelectStop(GradientStopClone stop)
        {
            var current = _spectrum.Stops.FirstOrDefault(x => ((GradientStopClone)x).IsSelected);
            if (current != null)
            {
                ((GradientStopClone) current).IsSelected = false;
            }

            if (stop == null)
            {
                SelectedStop = null;
                return;
            }

            stop.IsSelected = true;
            SelectedStop = stop;
        }

        private void PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    _touchId = e.GestureId;
                    _prevTotalX = 0;
                    break;
                case GestureStatus.Running:
                    if (e.GestureId == _touchId)
                    {
                        var deltaX = e.TotalX - _prevTotalX;

                        MoveStopBy((BindableObject)sender, deltaX);

                        _prevTotalX = e.TotalX;
                    }
                    break;
                case GestureStatus.Completed:
                case GestureStatus.Canceled:
                    break;
            }
        }

        private void MoveStopBy(BindableObject stop, double offsetX)
        {
            var deltaX = offsetX / _width;
            var newX = AbsoluteLayout.GetLayoutBounds(stop).X + deltaX;

            var stopItem = (GradientStopClone)stop.BindingContext;
            stopItem.Offset = Offset.Prop(newX);

            SelectStop(stopItem);
        }

        private void AddColor_Clicked(object sender, EventArgs e)
        {
            _spectrum.AddStop();
        }

        private void RemoveColor_Clicked(object sender, EventArgs e)
        {
            if (SelectedStop == null || _spectrum.Stops.Count == 1)
                return;

            var index = _spectrum.Stops.IndexOf(SelectedStop);
            if (index >= 0)
            {
                _spectrum.RemoveStopAt(index);
                SelectStop((GradientStopClone)_spectrum.Stops.FirstOrDefault());
            }
        }
    }
}