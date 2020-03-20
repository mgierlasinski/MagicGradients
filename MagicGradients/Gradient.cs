using MagicGradients.Renderers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Stops))]
    public abstract class Gradient : GradientElement, IGradientSource
    {
        public ObservableCollection<GradientStop> Stops { get; set; }

        public static readonly BindableProperty IsRepeatingProperty = BindableProperty.Create(
            nameof(IsRepeating), typeof(bool), typeof(LinearGradient), false);

        public bool IsRepeating
        {
            get => (bool)GetValue(IsRepeatingProperty);
            set => SetValue(IsRepeatingProperty, value);
        }

        protected Gradient()
        {
            Stops = new ObservableCollection<GradientStop>();
            Stops.CollectionChanged += OnCollectionChanged;
        }

        public IEnumerable<Gradient> GetGradients() => new[] { this };

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            foreach (var gradientStop in Stops)
            {
                SetInheritedBindingContext(gradientStop, BindingContext);
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                SetParent(e.OldItems, null);
            }

            if (e.NewItems != null)
            {
                SetParent(e.NewItems, this);
            }
        }

        public abstract void Render(RenderContext context);

        public virtual void Measure()
        {
            var fromIndex = 0;

            for (var i = 0; i < Stops.Count; i++)
            {
                if (Stops[i].Offset >= 0 || i == Stops.Count - 1)
                {
                    SetupUndefinedOffsets(fromIndex, i);
                    fromIndex = i;
                }
            }
        }

        private void SetupUndefinedOffsets(int fromIndex, int toIndex)
        {
            var currentOffset = Math.Max(Stops[fromIndex].Offset, 0);
            var endOffset = Math.Abs(Stops[toIndex].Offset);

            var step = (endOffset - currentOffset) / (toIndex - fromIndex);

            for (var i = fromIndex; i <= toIndex; i++)
            {
                var stop = Stops[i];

                if (stop.Offset < 0)
                {
                    stop.Offset = currentOffset;
                }
                currentOffset += step;
            }
        }
    }
}
