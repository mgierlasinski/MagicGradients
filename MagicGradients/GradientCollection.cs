using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Gradients))]
    public class GradientCollection : GradientElement, IGradientSource
    {
        public ObservableCollection<Gradient> Gradients { get; set; }

        public GradientCollection()
        {
            Gradients = new ObservableCollection<Gradient>();
            Gradients.CollectionChanged += OnCollectionChanged;
        }

        public IEnumerable<Gradient> GetGradients() => Gradients;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            foreach (var gradient in Gradients)
            {
                SetInheritedBindingContext(gradient, BindingContext);
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                SetParent(e.OldItems, null);
            }

            if (e.NewItems != null)
            {
                SetParent(e.NewItems, this);
            }
        }
    }
}
