using Playground.Data.Models;
using System.Collections.Generic;

namespace Playground.Data.Repositories
{
    public interface IGradientRepository
    {
        void Initialize();

        IEnumerable<Gradient> GetAll();
    }
}
