using Microsoft.Maui.Graphics;
using System;

namespace MagicGradients.Builder
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

        public RadialGradientBuilder()
        {
            Center = new Point(0.5, 0.5);
            RadiusX = -1d;
            RadiusY = -1d;
            Shape = RadialGradientShape.Ellipse;
            Size = RadialGradientSize.FarthestCorner;
            IsRepeating = false;
            Flags = RadialGradientFlags.PositionProportional;
        }

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
            var options = new DimenOptions().Proportional();
            setup?.Invoke(options);

            Center = position;
            FlagsHelper.SetValue(ref _flags, RadialGradientFlags.PositionProportional, options.IsProportional);

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
            FlagsHelper.SetValue(ref _flags, RadialGradientFlags.SizeProportional, options.IsProportional);

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

        public IGradient Construct(IGradientFactory factory)
        {
            return factory.Construct(this);
        }
    }
}
