using MagicGradients.Xaml;
using Xamarin.Forms;

namespace MagicGradients
{
    [TypeConverter(typeof(DimensionsTypeConverter))]
    public struct Dimensions
    {
        public static Dimensions Zero { get; } = Abs(0, 0);

        public Offset Width { get; set; }
        public Offset Height { get; set; }

        public bool IsZero => Width.Value == 0 && Height.Value == 0;

        public Dimensions(Offset width, Offset height)
        {
            Width = width;
            Height = height;
        }

        public static Dimensions Prop(double width, double height) => new Dimensions(Offset.Prop(width), Offset.Prop(height));
        public static Dimensions Abs(double width, double height) => new Dimensions(Offset.Abs(width), Offset.Abs(height));
    }
}
