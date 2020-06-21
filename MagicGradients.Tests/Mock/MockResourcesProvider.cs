using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MagicGradients.Tests.Mock
{
    internal class MockResourcesProvider : ISystemResourcesProvider
    {
        public IResourceDictionary GetSystemResources()
        {
            var dictionary = new ResourceDictionary();
            Style style;
            style = new Style(typeof(Label));
            dictionary[Device.Styles.BodyStyleKey] = style;

            style = new Style(typeof(Label));
            style.Setters.Add(Label.FontSizeProperty, 50);
            dictionary[Device.Styles.TitleStyleKey] = style;

            style = new Style(typeof(Label));
            style.Setters.Add(Label.FontSizeProperty, 40);
            dictionary[Device.Styles.SubtitleStyleKey] = style;

            style = new Style(typeof(Label));
            style.Setters.Add(Label.FontSizeProperty, 30);
            dictionary[Device.Styles.CaptionStyleKey] = style;

            style = new Style(typeof(Label));
            style.Setters.Add(Label.FontSizeProperty, 20);
            dictionary[Device.Styles.ListItemTextStyleKey] = style;

            style = new Style(typeof(Label));
            style.Setters.Add(Label.FontSizeProperty, 10);
            dictionary[Device.Styles.ListItemDetailTextStyleKey] = style;

            return dictionary;
        }
    }
}
