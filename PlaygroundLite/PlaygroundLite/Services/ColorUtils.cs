using System;
using Xamarin.Forms;

namespace PlaygroundLite.Services
{
    public static class ColorUtils
    {
        public static Color GetRandom()
        {
            var random = new Random();
            return new Color(
                random.NextDouble(),
                random.NextDouble(),
                random.NextDouble());
        }
    }
}
