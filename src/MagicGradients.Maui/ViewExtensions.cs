namespace MagicGradients.Forms;

public static class ViewExtensions
{
    public static bool TryFindParent<TParent>(this Element element, out TParent parent) where TParent : Element
    {
        while (element.Parent != null)
        {
            if (element.Parent is TParent foundParent)
            {
                parent = foundParent;
                return true;
            }

            element = element.Parent;
        }

        parent = null;
        return false;
    }
}
