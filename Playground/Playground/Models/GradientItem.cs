using MagicGradients;
using System;

namespace Playground.Models
{
    public class GradientItem
    {
        public Guid Id { get; set; }

        public IGradientSource Source { get; set; }
    }
}
