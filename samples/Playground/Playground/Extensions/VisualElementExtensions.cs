using System;
using Xamarin.Forms;

namespace Playground.Extensions
{
    public static class VisualElementExtensions
    {
        public static void Click(this VisualElement view, Action action)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await view.ScaleTo(0.9, 50, Easing.CubicOut);
                action();
                await view.ScaleTo(1, 50, Easing.CubicIn);
            });
        }
    }
}
