using System.Collections.Generic;
using Playground.Features.Gallery.Models;

namespace Playground.Features.Gallery.Services
{
    public interface IGalleryService
    {
        IEnumerable<GradientItem> GetGradients(string tag);
        IEnumerable<GradientItem> FilterGradients(string category, params string[] tags);
        GradientItem GetGradientById(int id);
    }
}
