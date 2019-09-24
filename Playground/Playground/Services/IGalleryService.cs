using Playground.Models;
using System.Collections.Generic;

namespace Playground.Services
{
    public interface IGalleryService
    {
        List<Gradient> GetGradients(string category);

        Gradient GetGradientById(string category, int id);
    }
}
