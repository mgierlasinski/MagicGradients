using System;

namespace GradientsApp.Infrastructure
{
    public class NavigationViewFactory
    {
        public TView CreateInstance<TView>(Type type)
        {
            return (TView)Activator.CreateInstance(type);
        }

        public void CallEvents(object bindingContext)
        {
            if (bindingContext is INavigationAware navAware)
                navAware.Prepare();
        }

        public void CallEvents<TParameter>(object bindingContext, TParameter parameter)
        {
            if (bindingContext is INavigationAware navAware)
                navAware.Prepare();

            if (bindingContext is INavigationAware<TParameter> navAwareParam)
                navAwareParam.Prepare(parameter);
        }
    }
}
