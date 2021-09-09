﻿using Microsoft.Maui.Graphics;

namespace MagicGradients.Graphics
{
    public static class GraphicsExtensions
    {
        public static Color ToMauiColor(this Xamarin.Forms.Color formsColor)
        {
            return new Color((float)formsColor.R, (float)formsColor.G, (float)formsColor.B, (float)formsColor.A);
        }
    }
}