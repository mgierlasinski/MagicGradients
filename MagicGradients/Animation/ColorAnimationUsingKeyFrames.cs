using Xamarin.Forms;

namespace MagicGradients.Animation
{
    public class ColorAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<Color>
    {
        protected override Color GetProgressValue(Color @from, Color to, double progress)
        {
            return AnimationHelper.GetColorValue(@from, to, progress);
        }
    }
}
