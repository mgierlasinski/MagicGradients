using System.Collections.Generic;

namespace MagicGradients.Builder
{
    public class RadialGradientBuilder : StopsBuilder<RadialGradientBuilder>, IChildBuilder
    {
        protected override RadialGradientBuilder Instance => this;
        
        public Position Center { get; internal set; }
        public Dimensions Radius { get; internal set; }
        public RadialGradientShape Shape { get; internal set; }
        public RadialGradientStretch Stretch { get; internal set; }
        public bool IsRepeating { get; internal set; }

        public RadialGradientBuilder()
        {
            Center = Position.Prop(0.5, 0.5);
            Radius = Dimensions.Zero;
            Shape = RadialGradientShape.Ellipse;
            Stretch = RadialGradientStretch.FarthestCorner;
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
        
        public RadialGradientBuilder At(double x, double y, OffsetType type = OffsetType.Proportional)
        {
            return At(new Position(x, y, type));
        }

        public RadialGradientBuilder Size(Dimensions size)
        {
            Radius = size;
            return this;
        }
        
        public RadialGradientBuilder Size(double width, double height, OffsetType type = OffsetType.Absolute)
        {
            return Size(new Dimensions(width, height, type));
        }

        public RadialGradientBuilder StretchTo(RadialGradientStretch stretch)
        {
            Stretch = stretch;
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
