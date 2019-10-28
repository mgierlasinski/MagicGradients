using MagicGradients;
using System;

namespace Playground.Models
{
    public class Gradient
    {
        public Guid Id { get; set; }

        public IGradientSource Source { get; set; }
    }
}
