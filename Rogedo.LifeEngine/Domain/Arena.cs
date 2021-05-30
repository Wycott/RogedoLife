using Rogedo.LifeEngine.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace Rogedo.LifeEngine.Domain
{
    public class Arena : IArena
    {
        public List<ICell> ArenaCells { get; private set; }
        private int Dimension { get; set; }

        public Arena()
        {
            ArenaCells = new List<ICell>();
        }

        public int GetArenaSize()
        {
            return ArenaCells.Count;
        }

        public void Initialise(int dimension)
        {
            Dimension = dimension;
            ArenaCells = new List<ICell>();

            for (int x = 0; x < dimension; x++)
            {
                for (int y = 0; y < dimension; y++)
                {
                    var cell = new Cell();
                    ArenaCells.Add(cell);
                }
            }
        }

        public string GetSignature()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var cell in ArenaCells)
            {
                var simpleCell = cell.Generation == Interfaces.Types.CellGeneration.Dead ? "0" : "1";
                sb.Append(simpleCell);
            }
            return sb.ToString();
        }

        public void Seed(int x, int y)
        {
            var index = (x % Dimension) + (y * Dimension);
            ArenaCells[index].SetGeneration(Interfaces.Types.CellGeneration.Current);
        }
    }
}
