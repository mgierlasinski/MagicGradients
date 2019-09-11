using Playground.Models;
using System.Collections.Generic;

namespace Playground.Services
{
    public interface IGalleryService
    {
        List<Gradient> GetGradients();

        Gradient GetGradientById(int id);
    }
}
