﻿using Xamarin.Forms;

namespace Playground.Features.Gallery.Models
{
    public class GradientTheme
    {
        public string ColorRaw { get; set; }
        public Color Color { get; set; }
        public string Tag { get; set; }

        public override string ToString()
        {
            return ColorRaw;
        }
    }
}
