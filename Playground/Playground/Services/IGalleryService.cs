using Playground.Models;
using System;
using System.Collections.Generic;

namespace Playground.Services
{
    public interface IGalleryService
    {
        IEnumerable<Gradient> GetGradients(string tag);

        IEnumerable<Gradient> FilterGradients(string category, params string[] tags);

        Gradient GetGradientById(Guid id);
    }
}
