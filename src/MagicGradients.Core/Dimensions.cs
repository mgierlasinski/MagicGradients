using MagicGradients.Converters;
using System.ComponentModel;
using System.Diagnostics;

namespace MagicGradients
{
    [DebuggerDisplay("Width={Width}, Height={Height}")]
    [TypeConverter(typeof(DimensionsTypeConverter))]
    public struct Dimensions
    {
        public static Dimensions Zero { get; } = Abs(0, 0);

        public Offset Width { get; set; }
        public Offset Height { get; set; }

        public bool IsZero => Width.Value == 0 && Height.Value == 0;

        public Dimensions(Offset uniformSize) 
            : this(uniformSize, uniformSize)
        {
        }

        public Dimensions(Offset width, Offset height)
        {
            Width = width;
            Height = height;
        }

        public static Dimensions Prop(double width, double height) => new(Offset.Prop(width), Offset.Prop(height));
        public static Dimensions Abs(double width, double height) => new(Offset.Abs(width), Offset.Abs(height));

        public override bool Equals(object obj)
        {
            if (obj is Dimensions other)
            {
                return Width.Equals(other.Width) && Height.Equals(other.Height);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Width.GetHashCode() ^ (Height.GetHashCode() * 397);
        }

        public static bool operator ==(Dimensions d1, Dimensions d2) => d1.Width == d2.Width && d1.Height == d2.Height;
        public static bool operator !=(Dimensions d1, Dimensions d2) => d1.Width != d2.Width || d1.Height != d2.Height;
    }
}
