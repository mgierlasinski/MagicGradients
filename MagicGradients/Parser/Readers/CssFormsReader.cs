using System;

namespace MagicGradients.Parser.Readers
{
    public class CssFormsReader : CssNativeReader
    {
        public CssFormsReader(string css) : base()
        {
            Tokens = css
                .Replace(" ", "")
                .Replace("\r\n", "")
                .Split(new[] { CssToken.LinearGradient }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
