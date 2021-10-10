using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicGradients.Xaml
{
    [TypeConversion(typeof(Offset))]
    public class OffsetTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            return Offset.Parse(value, OffsetType.Proportional);
        }
    }
}
