using Playground.Models;
using System;
using System.Collections.Generic;

namespace Playground.Services
{
    public interface IGalleryService
    {
        IEnumerable<Gradient> GetGradients(string tag);

        Gradient GetGradientById(Guid id);

        IEnumerable<GradientCategory> GetCategories();
    }
}
