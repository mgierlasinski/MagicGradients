using Playground.Models;
using System.Collections.Generic;

namespace Playground.Services
{
    public interface IGalleryService
    {
        IEnumerable<GradientItem> GetGradients(string tag);

        IEnumerable<GradientItem> FilterGradients(string category, params string[] tags);

        GradientItem GetGradientById(int id);
    }
}
