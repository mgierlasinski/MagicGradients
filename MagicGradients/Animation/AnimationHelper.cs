using Xamarin.Forms;

namespace MagicGradients.Animation
{
    public static class AnimationHelper
    {
        public static int GetIntValue(int from, int to, double animationProgress)
        {
            return (int)(from + (to - from) * animationProgress);
        }

        public static double GetDoubleValue(double from, double to, double animationProgress)
        {
            return from + (to - from) * animationProgress;
        }

        public static Color GetColorValue(Color from, Color to, double animationProgress)
        {
            return Color.FromRgb(
                from.R + (to.R - from.R) * animationProgress, 
                from.G + (to.G - from.G) * animationProgress, 
                from.B + (to.B - from.B) * animationProgress);
        }

        public static Point GetPointValue(Point from, Point to, double animationProgress)
        {
            return new Point(
                from.X + (to.X - from.X) * animationProgress,
                from.Y + (to.Y - from.Y) * animationProgress);
        }

        public static CornerRadius GetCornerRadiusValue(CornerRadius from, CornerRadius to, double animationProgress)
        {
            return new CornerRadius(
                from.TopLeft + (to.TopLeft - from.TopLeft) * animationProgress,
                from.TopRight + (to.TopRight - from.TopRight) * animationProgress,
                from.BottomLeft + (to.BottomLeft - from.BottomLeft) * animationProgress,
                from.BottomRight + (to.BottomRight - from.BottomRight) * animationProgress);
        }

        public static Thickness GetThicknessValue(Thickness from, Thickness to, double animationProgress)
        {
            return new Thickness(
                from.Left + (to.Left - from.Left) * animationProgress,
                from.Top + (to.Top - from.Top) * animationProgress,
                from.Right + (to.Right - from.Right) * animationProgress,
                from.Bottom + (to.Bottom - from.Bottom) * animationProgress);
        }
    }

    public static class AnimationExtensions
    {
        public static int Tween(this int from, int to, double progress)
        {
            return (int)(from + (to - from) * progress);
        }

        public static double Tween(this double from, double to, double progress)
        {
            return from + (to - from) * progress;
        }

        public static Color Tween(this Color from, Color to, double progress)
        {
            return Color.FromRgb(
                from.R + (to.R - from.R) * progress,
                from.G + (to.G - from.G) * progress,
                from.B + (to.B - from.B) * progress);
        }

        public static Point Tween(this Point from, Point to, double progress)
        {
            return new Point(
                from.X + (to.X - from.X) * progress,
                from.Y + (to.Y - from.Y) * progress);
        }

        public static CornerRadius Tween(this CornerRadius from, CornerRadius to, double progress)
        {
            return new CornerRadius(
                from.TopLeft + (to.TopLeft - from.TopLeft) * progress,
                from.TopRight + (to.TopRight - from.TopRight) * progress,
                from.BottomLeft + (to.BottomLeft - from.BottomLeft) * progress,
                from.BottomRight + (to.BottomRight - from.BottomRight) * progress);
        }

        public static Thickness Tween(this Thickness from, Thickness to, double progress)
        {
            return new Thickness(
                from.Left + (to.Left - from.Left) * progress,
                from.Top + (to.Top - from.Top) * progress,
                from.Right + (to.Right - from.Right) * progress,
                from.Bottom + (to.Bottom - from.Bottom) * progress);
        }
    }
}
