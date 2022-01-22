using Android.Views;
using Android.Widget;

namespace GradientsApp.Android.Markup
{
    public static class ViewExtensions
    {
        public static T Height<T>(this T view, int height) where T : View
        {
            if (view.LayoutParameters != null)
                view.LayoutParameters.Height = height;
            else
                view.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, height);

            return view;
        }

        public static T Children<T>(this T view, params View[] children) where T : ViewGroup
        {
            foreach (var child in children)
            {
                view.AddView(child);
            }
            return view;
        }

        public static LinearLayout Horizontal(this LinearLayout view)
        {
            view.Orientation = Orientation.Horizontal;
            return view;
        }

        public static LinearLayout Vertical(this LinearLayout view)
        {
            view.Orientation = Orientation.Vertical;
            return view;
        }
    }
}