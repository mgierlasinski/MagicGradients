using Xamarin.Forms;

namespace MagicGradients
{
    [TypeConverter(typeof(OffsetTypeConverter))]
    public struct Offset
    {
        public static Offset Empty { get; } = new Offset(-1, OffsetType.Proportional);
        public static Offset Zero { get; } = new Offset(0, OffsetType.Proportional);

        public double Value { get; set; }
        public OffsetType Type { get; set; }

        public bool IsEmpty => Value < 0;

        public Offset(double value, OffsetType type)
        {
            Value = value;
            Type = type;
        }

        public static Offset Prop(double value) => new Offset(value, OffsetType.Proportional);
        public static Offset Abs(double value) => new Offset(value, OffsetType.Absolute);
    }

    public enum OffsetType
    {
        Proportional,
        Absolute
    }
}
