using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;

namespace MagicGradients.Builder
{
    public class RadialGradientBuilder : StopsBuilder<RadialGradientBuilder>, IChildBuilder
    {
        protected override RadialGradientBuilder Instance => this;
        
        public Position Center { get; internal set; }
        public Dimensions Radius { get; internal set; }
        public RadialGradientShape Shape { get; internal set; }
        public RadialGradientSize Size { get; internal set; }
        public bool IsRepeating { get; internal set; }

        public RadialGradientBuilder()
        {
            Center = Position.Prop(0.5, 0.5);
            Radius = Dimensions.Zero;
            Shape = RadialGradientShape.Ellipse;
            Size = RadialGradientSize.FarthestCorner;
            IsRepeating = false;
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

        public RadialGradientBuilder At(Position position)
        {
            Center = position;
            return this;
        }

        public RadialGradientBuilder At(Point position, Action<OffsetOptions> setup = null)
        {
            return At(position.X, position.Y, setup);
        }

        public RadialGradientBuilder At(double x, double y, Action<OffsetOptions> setup = null)
        {
            var options = new OffsetOptions().Proportional();
            setup?.Invoke(options);

            Center = options.IsProportional
                ? Position.Prop(x, y)
                : Position.Abs(x, y);

            return this;
        }

        public RadialGradientBuilder Resize(Dimensions radius)
        {
            Radius = radius;
            return this;
        }

        public RadialGradientBuilder Resize(Size radius, Action<OffsetOptions> setup = null)
        {
            return Resize(radius.Width, radius.Height, setup);
        }

        public RadialGradientBuilder Resize(double radiusX, double radiusY, Action<OffsetOptions> setup = null)
        {
            var options = new OffsetOptions();
            setup?.Invoke(options);

            Radius = options.IsProportional
                ? Dimensions.Prop(radiusX, radiusY)
                : Dimensions.Abs(radiusX, radiusY);
            
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

        public void AddConstructed(List<IGradient> gradients)
        {
            gradients.Add(Factory.Construct(this));
        }
    }
}
