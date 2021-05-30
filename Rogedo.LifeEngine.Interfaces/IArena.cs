using System;
using System.Collections.Generic;
using System.Text;

namespace Rogedo.LifeEngine.Interfaces
{
    public interface IArena
    {
        void Initialise(int dimension);
        int GetArenaSize();
    }
}
