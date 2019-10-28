using Playground.Models;
using System;
using System.Collections.Generic;

namespace Playground.Services
{
    public interface IGalleryService
    {
        List<Gradient> GetGradients(string category);

        Gradient GetGradientById(Guid id);
    }
}
