using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace MagicGradients
{
    [Xamarin.Forms.TypeConverter(typeof(CssGradientSourceTypeConverter))]
    public interface IGradientSource : INotifyPropertyChanged
    {
        IEnumerable<Gradient> GetGradients();
    }
}
