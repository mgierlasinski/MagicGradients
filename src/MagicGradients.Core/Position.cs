using MagicGradients.Converters;
using System.ComponentModel;
using System.Diagnostics;

namespace MagicGradients
{
    [DebuggerDisplay("X={X}, Y={Y}")]
    [TypeConverter(typeof(PositionTypeConverter))]
    public struct Position
    {
        public static Position Zero { get; } = Abs(0, 0);

        public Offset X { get; set; }
        public Offset Y { get; set; }

        public bool IsZero => X.Value == 0 && Y.Value == 0;
        
        public Position(Offset x, Offset y)
        {
            X = x;
            Y = y;
        }

        public Position(double x, double y, OffsetType type)
        {
            X = new Offset(x, type);
            Y = new Offset(y, type);
        }

        public static Position Prop(double x, double y) => new(Offset.Prop(x), Offset.Prop(y));
        public static Position Abs(double x, double y) => new(Offset.Abs(x), Offset.Abs(y));
        
        public bool Equals(Position other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is Position other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public static bool operator ==(Position p1, Position p2) => p1.X == p2.X && p1.Y == p2.Y;
        public static bool operator !=(Position p1, Position p2) => p1.X != p2.X || p1.Y != p2.Y;
    }
}
