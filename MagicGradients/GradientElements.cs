﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace MagicGradients
{
    public class GradientElements<TElement> : ObservableCollection<TElement> where TElement : GradientElement
    {
        public GradientElement Parent { get; private set; }

        public GradientElements() { }
        public GradientElements(IEnumerable<TElement> collection) : base(collection) { }
        public GradientElements(List<TElement> list) : base(list) { }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (Parent == null)
                return;

            if (e.OldItems != null)
            {
                SetupItems(e.OldItems, null);
            }

            if (e.NewItems != null)
            {
                SetupItems(e.NewItems, Parent);
            }

            Parent.InvalidateCanvas();
        }

        private void SetupItems(IList items, GradientElement parent)
        {
            foreach (var item in items)
            {
                var element = (GradientElement)item;
                BindableObject.SetInheritedBindingContext(element, parent?.BindingContext);
                element.Parent = parent;
            }
        }

        internal void AttachTo(GradientElement parent)
        {
            Parent = parent;

            foreach (var item in Items)
            {
                BindableObject.SetInheritedBindingContext(item, Parent?.BindingContext);
                item.Parent = parent;
            }

            Parent?.InvalidateCanvas();
        }

        internal void Release()
        {
            Parent = null;

            foreach (var item in Items)
            {
                BindableObject.SetInheritedBindingContext(item, null);
                item.Parent = null;
            }
        }

        internal void SetInheritedBindingContext(object bindingContext)
        {
            foreach (var item in Items)
            {
                BindableObject.SetInheritedBindingContext(item, bindingContext);
            }
        }
    }
}
