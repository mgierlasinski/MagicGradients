using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [TypeConverter(typeof(CssGradientSourceTypeConverter))]
    public interface ILinearGradientSource
    {
        IEnumerable<LinearGradient> GetGradients();
    }
}
