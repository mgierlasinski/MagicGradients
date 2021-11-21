using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using MagicGradients;

namespace GradientsApp.Android.Views.Adapters
{
    public class CategoryViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; set; }
        public GradientView Preview { get; set; }

        public CategoryViewHolder(View itemView) : base(itemView)
        {
            Name = itemView.FindViewById<TextView>(Resource.Id.category_name);
            Preview = itemView.FindViewById<GradientView>(Resource.Id.category_preview);
        }
    }
}