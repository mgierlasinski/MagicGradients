using System.Collections.Generic;

namespace MagicGradients.Parser.TokenDefinitions
{
    public class ColorDefinition
    {
        protected List<Offset> GetOffsets(string[] tokens)
        {
            var offsets = new List<Offset>();

            foreach (var token in tokens)
            {
                if (Offset.TryParseWithUnit(token, out var offset))
                    offsets.Add(offset);
            }

            return offsets;
        }
    }
}
