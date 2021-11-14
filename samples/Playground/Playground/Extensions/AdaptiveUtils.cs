using System;
using Xamarin.Forms;

namespace Playground.Extensions
{
    public static class AdaptiveUtils
    {
        private const int ColSize = 200;
        private const int MinCols = 2;

        public static void SetGridColumns(CollectionView view)
        {
            if (view.Width > ColSize * MinCols)
            {
                var cols = (int)Math.Floor(view.Width / ColSize);
                ((GridItemsLayout)view.ItemsLayout).Span = cols;
            }
            else
            {
                ((GridItemsLayout)view.ItemsLayout).Span = MinCols;
            }
        }
    }
}
