using System;
using System.Globalization;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public abstract class ColorDefinition
    {
        protected ColorTypeConverter ColorConverter { get; } = new ColorTypeConverter();
    }
}
