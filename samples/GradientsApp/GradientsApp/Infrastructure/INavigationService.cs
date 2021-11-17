using System;
using System.Threading.Tasks;

namespace GradientsApp.Infrastructure
{
    public interface INavigationService
    {
        Task NavigateTo(string route);
        Task NavigateTo<TParameter>(string route, TParameter parameter);
        void RegisterRoute(string route, Type type);
    }
}
