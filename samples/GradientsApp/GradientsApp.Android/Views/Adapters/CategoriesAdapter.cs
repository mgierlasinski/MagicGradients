using Android.Views;
using AndroidX.RecyclerView.Widget;
using GradientsApp.Models;
using System.Collections.Generic;

namespace GradientsApp.Android.Views.Adapters
{
    public class CategoriesAdapter : RecyclerView.Adapter
    {
        private readonly IList<CategoryItem> _categories;

        public override int ItemCount => _categories.Count;

        public CategoriesAdapter(IList<CategoryItem> categories)
        {
            _categories = categories;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.category_item, parent, false);
            return new CategoryViewHolder(itemView);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as CategoryViewHolder;
            var category = _categories[position];

            vh.Name.Text = category.Name;
            vh.Preview.GradientSource = category.Source;
        }
    }
}