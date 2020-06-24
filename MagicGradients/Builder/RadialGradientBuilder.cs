using Xamarin.Forms;

namespace MagicGradients
{
    public class RadialGradientBuilder : StopsBuilder<RadialGradientBuilder>, IChildBuilder
    {
        protected override RadialGradientBuilder Instance => this;

        internal Point Center { get; set; }
        internal double RadiusX { get; set; }
        internal double RadiusY { get; set; }
        internal RadialGradientShape Shape { get; set; }
        internal RadialGradientSize Size { get; set; }
        internal RadialGradientFlags Flags { get; set; } = RadialGradientFlags.PositionProportional;
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

        public RadialGradientBuilder At(Point position)
        {
            Center = position;
            return this;
        }

        public RadialGradientBuilder At(double x, double y)
        {
            Center = new Point(x, y);
            return this;
        }

        public RadialGradientBuilder Radius(double radiusX, double radiusY)
        {
            RadiusX = radiusX;
            RadiusY = radiusY;
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
                Flags = Flags,
                IsRepeating = IsRepeating,
                Stops = new GradientElements<GradientStop>(StopsFactory.Stops)
            };

            return radialGradient;
        }
    }
}
