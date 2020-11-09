using Playground.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Playground.Features.Animation
{
    public class AnimationsViewModel : ObservableObject
    {
        private readonly List<AnimationItem> _animations = new List<AnimationItem>();

        public AnimationItem RotateAnimation { get; }
        public AnimationItem ColorAnimation { get; }
        public AnimationItem PointAnimation { get; }
        public AnimationItem DimensionsAnimation { get; }
        public AnimationItem StoryboardAnimation { get; }
        public AnimationItem PointFrameAnimation { get; }
        public AnimationItem ScannerAnimation { get; }
        public AnimationItem PulseAnimation { get; }

        public AnimationsViewModel()
        {
            RotateAnimation = CreateAnimation("Rotate Animation");
            ColorAnimation = CreateAnimation("Color Animation");
            PointAnimation = CreateAnimation("Point Animation");
            DimensionsAnimation = CreateAnimation("Dimensions Animation");
            StoryboardAnimation = CreateAnimation("Storyboard Animation");
            PointFrameAnimation = CreateAnimation("Point Frame Animation");
            ScannerAnimation = CreateAnimation("Scanner Animation");
            PulseAnimation = CreateAnimation("Pulse Animation");
        }

        protected AnimationItem CreateAnimation(string title)
        {
            var animation = new AnimationItem(title);
            _animations.Add(animation);

            return animation;
        }

        public void StopAnimations()
        {
            foreach (var animation in _animations.Where(x => x.IsRunning))
            {
                animation.IsRunning = false;
            }
        }
    }
}
