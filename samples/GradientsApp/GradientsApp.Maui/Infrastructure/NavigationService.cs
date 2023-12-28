using GradientsApp.Infrastructure;

namespace GradientsApp.Maui.Infrastructure;

public class NavigationService : INavigationService
{
    public Task NavigateTo(string route)
    {
        return Shell.Current.GoToAsync(route);
    }

    public Task NavigateTo<TParameter>(string route, TParameter parameter)
    {
        return Shell.Current.GoToAsync(route, new Dictionary<string, object>
        {
            ["parameter"] = parameter
        });
    }

    public void RegisterRoute(string route, Type type)
    {
        Routing.RegisterRoute(route, type);
    }
}