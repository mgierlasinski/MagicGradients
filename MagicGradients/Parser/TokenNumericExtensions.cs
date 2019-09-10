using System.Globalization;

namespace MagicGradients.Parser
{
    public static class TokenNumericExtensions
    {
        public static byte ToByte(this string token)
        {
            if (byte.TryParse(token, out var result))
            {
                return result;
            }

            return 0;
        }

        public static int ToInt(this string token)
        {
            if (int.TryParse(token, out var result))
            {
                return result;
            }

            return 0;
        }

        public static double ToDouble(this string token)
        {
            if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            return 0;
        }

        public static float ToFloat(this string token)
        {
            if (float.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            return 0;
        }
    }
}
