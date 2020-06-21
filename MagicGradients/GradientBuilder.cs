using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MagicGradients
{
    public interface IChildBuilder
    {
        StopsBuilder StopsBuilder { get; }
        Gradient Construct();
    }

    public class StopsBuilder
    {
        public List<GradientStop> Stops { get; } = new List<GradientStop>();

        internal void AddSingleStop(Color color, Offset? offset = null)
        {
            var stop = new GradientStop
            {
                Color = color,
                Offset = offset ?? Offset.Empty
            };

            Stops.Add(stop);
        }

        internal void AddMultipleStops(Color color, IEnumerable<Offset> offsets)
        {
            foreach (var offset in offsets)
            {
                AddSingleStop(color, offset);
            }
        }

        internal void AddMultipleStops(params Color[] colors)
        {
            foreach (var color in colors)
            {
                AddSingleStop(color);
            }
        }
    }

    public abstract class BuilderBase<T>
    {
        public virtual StopsBuilder StopsBuilder { get; } = new StopsBuilder();
        protected abstract T Instance { get; }

        public T AddStop(Color color, Offset? offset = null)
        {
            StopsBuilder.AddSingleStop(color, offset);
            return Instance;
        }

        public T AddStops(Color color, IEnumerable<Offset> offsets)
        {
            StopsBuilder.AddMultipleStops(color, offsets);
            return Instance;
        }

        public T AddStops(params Color[] colors)
        {
            StopsBuilder.AddMultipleStops(colors);
            return Instance;
        }
    }

    public class LinearGradientBuilder : BuilderBase<LinearGradientBuilder>, IChildBuilder
    {
        protected override LinearGradientBuilder Instance => this;

        internal double Angle { get; set; }
        internal bool IsRepeating { get; set; }

        public LinearGradientBuilder Rotate(double angle)
        {
            Angle = angle;
            return this;
        }

        public LinearGradientBuilder Repeat()
        {
            IsRepeating = true;
            return this;
        }

        public Gradient Construct()
        {
            var linearGradient = new LinearGradient
            {
                Angle = Angle,
                IsRepeating = IsRepeating,
                Stops = new GradientElements<GradientStop>(StopsBuilder.Stops)
            };

            return linearGradient;
        }
    }

    public class RadialGradientBuilder : BuilderBase<RadialGradientBuilder>, IChildBuilder
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
                Stops = new GradientElements<GradientStop>(StopsBuilder.Stops)
            };

            return radialGradient;
        }
    }

    public class GradientBuilder : BuilderBase<GradientBuilder>
    {
        private IChildBuilder _currentBuilder;
        private readonly List<IChildBuilder> _children = new List<IChildBuilder>();

        protected override GradientBuilder Instance => this;
        public override StopsBuilder StopsBuilder => GetCurrentBuilder().StopsBuilder;

        public GradientBuilder AddLinearGradient(Action<LinearGradientBuilder> setup)
        {
            var builder = new LinearGradientBuilder();
            setup(builder);

            UseBuilder(builder);
            return this;
        }

        public LinearGradientBuilder AddLinearGradient(double angle, bool isRepeating = false)
        {
            var builder = new LinearGradientBuilder
            {
                Angle = angle,
                IsRepeating = isRepeating
            };
            UseBuilder(builder);
            return builder;
        }

        public GradientBuilder AddRadialGradient(Action<RadialGradientBuilder> setup)
        {
            var builder = new RadialGradientBuilder();
            setup(builder);

            UseBuilder(builder);
            return this;
        }

        public RadialGradientBuilder AddRadialGradient(
            Point center, 
            RadialGradientShape shape, 
            RadialGradientSize size, 
            RadialGradientFlags flags = RadialGradientFlags.PositionProportional, 
            bool isRepeating = false)
        {
            var builder = new RadialGradientBuilder
            {
                Center = center,
                Shape = shape, 
                Size = size, 
                Flags = flags,
                IsRepeating = isRepeating
            };

            UseBuilder(builder);
            return builder;
        }

        public Gradient[] Build()
        {
            return _children.Select(x => x.Construct()).ToArray();
        }

        private IChildBuilder GetCurrentBuilder()
        {
            if (_currentBuilder == null)
            {
                UseBuilder(new LinearGradientBuilder());
            }

            return _currentBuilder;
        }

        private void UseBuilder(IChildBuilder builder)
        {
            _currentBuilder = builder;
            _children.Add(builder);
        }
    }

    public static class GradientBuilderExtensions
    {
        public static IGradientSource ToSource(this GradientBuilder builder)
        {
            return new GradientCollection
            {
                Gradients = new GradientElements<Gradient>(builder.Build())
            };
        }
    }
}
