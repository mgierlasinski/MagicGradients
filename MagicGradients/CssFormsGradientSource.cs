using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MagicGradients;
using MagicGradients.Parser;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;

namespace MagicGradients
{
    [ContentProperty(nameof(Stylesheet))]
    public class CssFormsGradientSource : BindableObject, ILinearGradientSource
    {
        static CssFormsGradientSource()
        {
            var stylePropertyInfo = typeof(Xamarin.Forms.Internals.Registrar).GetProperty("StyleProperties",
                BindingFlags.Static | BindingFlags.NonPublic);
            if (stylePropertyInfo == null)
                return;

            var styleProperties = stylePropertyInfo.GetValue(null);

            var styleAttributeType = typeof(StyleSheet).Assembly.GetType("Xamarin.Forms.StyleSheets.StylePropertyAttribute");
            var styleAttributeInstance = Activator.CreateInstance(styleAttributeType, "gradient",
                typeof(CssFormsGradientSource), nameof(StylesheetProperty));

            var dictionaryAdd = styleProperties.GetType().GetMethod("Add");
            if (dictionaryAdd == null)
                return;

            var styleListType = typeof(List<>).MakeGenericType(styleAttributeType);
            var styleList = (IList)Activator.CreateInstance(styleListType);

            styleList.Add(styleAttributeInstance);
            dictionaryAdd.Invoke(styleProperties, new object[] { "gradient", styleList });
        }

        public static readonly BindableProperty StylesheetProperty = BindableProperty.Create(
            nameof(Stylesheet), typeof(string), typeof(CssFormsGradientSource));

        public string Stylesheet
        {
            get => (string)GetValue(StylesheetProperty);
            set => SetValue(StylesheetProperty, value);
        }

        public IEnumerable<LinearGradient> GetGradients()
        {
            return new CssFormsLinearGradientParser().ParseCss(Stylesheet);
        }
    }
}
