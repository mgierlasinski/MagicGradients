using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MagicGradients.Maui;

public class GradientElements<TElement> : ObservableCollection<TElement> where TElement : GradientElement
{
    public GradientElement Parent { get; private set; }

    public GradientElements() { }
    public GradientElements(IEnumerable<TElement> collection) : base(collection) { }
    public GradientElements(List<TElement> list) : base(list) { }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        base.OnCollectionChanged(e);

        if (Parent == null)
            return;

        var isChanged = false;

        if (e.OldItems != null)
        {
            SetupItems(e.OldItems, null);
            isChanged = true;
        }

        if (e.NewItems != null)
        {
            SetupItems(e.NewItems, Parent);
            isChanged = true;
        }

        if (isChanged)
        {
            Parent.InvalidateCanvas();
        }
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
