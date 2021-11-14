using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms.StyleSheets;
using Registrar = Xamarin.Forms.Internals.Registrar;

namespace MagicGradients
{
    public static class StyleSheets
    {
        public static void RegisterStyle(string name, Type targetType, string bindablePropertyName)
        {
            var stylePropertyInfo = typeof(Registrar).GetProperty("StyleProperties", BindingFlags.Static | BindingFlags.NonPublic);
            var styleProperties = stylePropertyInfo?.GetValue(null);

            if (styleProperties == null)
                return;

            var stylePropertiesType = styleProperties.GetType();
            var styleAttributeType = typeof(StyleSheet).Assembly.GetType("Xamarin.Forms.StyleSheets.StylePropertyAttribute");
            var styleAttributeInstance = Activator.CreateInstance(styleAttributeType, name, targetType, bindablePropertyName);

            var containsKeyMethod = stylePropertiesType.GetMethod("ContainsKey");
            if (containsKeyMethod == null)
                return;

            var containsKey = (bool)containsKeyMethod.Invoke(styleProperties, new object[] { name });
            if (containsKey)
            {
                var itemProperty = stylePropertiesType.GetProperty("Item");
                var attributes = itemProperty?.GetValue(styleProperties, new object[] { name });

                attributes?.GetType().GetMethod("Insert")?.Invoke(attributes, new[] { 0, styleAttributeInstance });
            }
            else
            {
                var styleListType = typeof(List<>).MakeGenericType(styleAttributeType);
                var styleList = (IList)Activator.CreateInstance(styleListType);
                styleList.Add(styleAttributeInstance);

                stylePropertiesType.GetMethod("Add")?.Invoke(styleProperties, new object[] { name, styleList });
            }
        }
    }
}
