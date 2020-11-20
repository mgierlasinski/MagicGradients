using MagicGradients;
using Playground.Extensions;
using System.Linq;

namespace Playground.Controls
{
    public class ColorSpectrumGradient : LinearGradient
    {
        private readonly Gradient _source;

        public ColorSpectrumGradient(Gradient source)
        {
            _source = source;
            Angle = 270;

            foreach (var stop in _source.Stops)
            {
                Stops.Add(new GradientStopClone(stop));
            }
        }

        public void AddStop()
        {
            var stop = new GradientStop
            {
                Color = ColorUtils.GetRandom(),
                Offset = Offset.Prop(1)
            };

            var lastStop = Stops.LastOrDefault();
            if (lastStop != null && lastStop.RenderOffset > 0.9)
            {
                foreach (var x in Stops)
                {
                    x.Offset = Offset.Prop(x.RenderOffset * 0.9);
                }
            }

            _source.Stops.Add(stop);
            Stops.Add(new GradientStopClone(stop));
        }

        public void RemoveStopAt(int index)
        {
            _source.Stops.RemoveAt(index);
            Stops.RemoveAt(index);
        }
    }

    public class GradientStopClone : GradientStop
    {
        public GradientStop Source { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public GradientStopClone(GradientStop source)
        {
            Source = source;
            Color = source.Color;
            RenderOffset = source.RenderOffset;
            Offset = source.Offset.IsEmpty ? Offset.Prop(source.RenderOffset) : source.Offset;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Color))
                Source.Color = Color;

            if (propertyName == nameof(Offset))
                Source.Offset = Offset;
        }
    }
}
