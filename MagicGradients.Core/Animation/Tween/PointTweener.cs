﻿using Xamarin.Forms;

namespace MagicGradients.Animation.Tween
{
    public class PointTweener : ITweener<Point>
    {
        public Point Tween(Point @from, Point to, double progress)
        {
            return new Point(
                from.X + (to.X - from.X) * progress,
                from.Y + (to.Y - from.Y) * progress);
        }
    }
}
