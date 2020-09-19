using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlaygroundLite.ViewModels
{
    public class AnimationsViewModel : FreshBasePageModel
    {
        protected List<AnimationItem> Animations { get; } = new List<AnimationItem>();

        protected AnimationItem CreateAnimation(string title)
        {
            var animation = new AnimationItem(title);
            Animations.Add(animation);

            return animation;
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            foreach (var animation in Animations.Where(x => x.IsRunning))
            {
                animation.IsRunning = false;
            }
        }
    }
}
