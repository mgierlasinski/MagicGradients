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

        public static Position Prop(double x, double y) => new(Offset.Prop(x), Offset.Prop(y));
        public static Position Abs(double x, double y) => new(Offset.Abs(x), Offset.Abs(y));

        public override bool Equals(object obj)
        {
            if (obj is Position other)
            {
                return X.Equals(other.X) && Y.Equals(other.Y);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ (Y.GetHashCode() * 397);
        }

        public static bool operator ==(Position p1, Position p2) => p1.X == p2.X && p1.Y == p2.Y;
        public static bool operator !=(Position p1, Position p2) => p1.X != p2.X || p1.Y != p2.Y;
    }
}
