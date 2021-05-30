using Rogedo.LifeEngine.Interfaces;
using Rogedo.LifeEngine.Interfaces.Types;

namespace Rogedo.LifeEngine.Domain
{
    public class Cell : ICell
    {
        public CellGeneration Generation { get; private set; }

        public Cell()
        {
            Generation = CellGeneration.Dead;
        }

        public void SetGeneration(CellGeneration generation)
        {
            Generation = generation;
        }
    }
}
