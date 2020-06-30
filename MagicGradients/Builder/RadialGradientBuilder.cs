using System;
using Xamarin.Forms;
using static MagicGradients.FlagsHelper;

namespace MagicGradients
{
    public class RadialGradientBuilder : StopsBuilder<RadialGradientBuilder>, IChildBuilder
    {
        protected override RadialGradientBuilder Instance => this;

        private RadialGradientFlags _flags;
        internal RadialGradientFlags Flags
        {
            get => _flags;
            set => _flags = value;
        }

        internal Point Center { get; set; }
        internal double RadiusX { get; set; }
        internal double RadiusY { get; set; }
        internal RadialGradientShape Shape { get; set; }
        internal RadialGradientSize Size { get; set; }
        internal bool IsRepeating { get; set; }

        public RadialGradientBuilder Circle()
        {
            Shape = RadialGradientShape.Circle;
            return this;
        }

        public RadialGradientBuilder Ellipse()
        {
            Shape = RadialGradientShape.Ellipse;
            return this;
        }

        public RadialGradientBuilder At(Point position, Action<DimenOptions> setup = null)
        {
            var options = new DimenOptions();
            setup?.Invoke(options);

            Center = position;
            SetValue(ref _flags, RadialGradientFlags.PositionProportional, options.IsProportional);

            return this;
        }

        public RadialGradientBuilder At(double x, double y, Action<DimenOptions> setup = null)
        {
            return At(new Point(x, y), setup);
        }

        public RadialGradientBuilder Radius(Size radius, Action<DimenOptions> setup = null)
        {
            return Radius(radius.Width, radius.Height, setup);
        }

        public RadialGradientBuilder Radius(double radiusX, double radiusY, Action<DimenOptions> setup = null)
        {
            var options = new DimenOptions();
            setup?.Invoke(options);

            RadiusX = radiusX;
            RadiusY = radiusY;
            SetValue(ref _flags, RadialGradientFlags.SizeProportional, options.IsProportional);

            return this;
        }

        public RadialGradientBuilder StretchTo(RadialGradientSize size)
        {
            Size = size;
            return this;
        }

        public RadialGradientBuilder Repeat()
        {
            IsRepeating = true;
            return this;
        }

        public Gradient Construct()
        {
            var radialGradient = new RadialGradient
            {
                Center = Center,
                Shape = Shape,
                Size = Size,
                RadiusX = RadiusX,
                RadiusY = RadiusY,
                Flags = _flags,
                IsRepeating = IsRepeating,
                Stops = new GradientElements<GradientStop>(StopsFactory.Stops)
            };

            return radialGradient;
        }
    }
}
