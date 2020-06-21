using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class ColorDefinition
    {
        protected ColorTypeConverter ColorConverter { get; } = new ColorTypeConverter();
        protected OffsetTypeConverter OffsetConverter { get; } = new OffsetTypeConverter();

        protected List<Offset> GetOffsets(string[] tokens)
        {
            var offsets = new List<Offset>();

            foreach (var token in tokens)
            {
                if (OffsetConverter.TryExtractOffset(token, out var offset))
                    offsets.Add(offset);
            }

            return offsets;
        }
    }
}
