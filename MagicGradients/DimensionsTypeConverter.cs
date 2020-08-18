using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicGradients
{
    [TypeConversion(typeof(Dimensions))]
    public class DimensionsTypeConverter : SizeTypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            var size = (Size)base.ConvertFromInvariantString(value);
            return (Dimensions)size;
        }
    }
}
