using Rogedo.LifeEngine.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;

namespace Rogedo.LifeEngine.Domain
{
    public class Arena : IArena
    {
        public List<ICell> ArenaCells { get; private set; }
        private int Dimension { get; set; }
        public int CellGeneration { get; private set; }

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
            var index = GetIndex(x, y);
            ArenaCells[index].SetGeneration(Interfaces.Types.CellGeneration.Current);
        }

        public ICell GetCellAt(int x, int y)
        {
            if ((x < 0 || x >= Dimension) || (y < 0 || y >= Dimension))
                return new Cell();
            return ArenaCells[GetIndex(x, y)];
        }

        public void MakeNextGeneration()
        {
            List<Point> dying = new List<Point>();

            bool cellProcessed;
            for (int x = 0; x < Dimension; x++)
            {
                for (int y = 0; y < Dimension; y++)
                {
                    cellProcessed = false;
                    var cell = GetCellAt(x, y);

                    if (cell.Generation == Interfaces.Types.CellGeneration.Current) // Live cell
                    {
                        var neighbourCount = GetLiveNeighbourCount(x, y);
                        if (neighbourCount == 2 || neighbourCount == 3)
                        {
                            cellProcessed = true; // Survives
                        }
                    }

                    if (cell.Generation == Interfaces.Types.CellGeneration.Dead) // Dead cell
                    {
                        var neighbourCount = GetLiveNeighbourCount(x, y);
                        if (neighbourCount == 3)
                        {
                            cell.SetGeneration(Interfaces.Types.CellGeneration.Next);
                            cellProcessed = true; // Born
                        }
                    }

                    if (!cellProcessed)
                    {
                        var point = new Point(x, y);
                        dying.Add(point);
                    }
                }
            }

            foreach (var cell in ArenaCells)
            {
                if (cell.Generation == Interfaces.Types.CellGeneration.Next)
                    cell.SetGeneration(Interfaces.Types.CellGeneration.Current);
            }

            foreach (var p in dying)
            {
                GetCellAt(p.X, p.Y).SetGeneration(Interfaces.Types.CellGeneration.Dead);
            }

            CellGeneration++;
        }

        public int GetPopulation()
        {
            return ArenaCells.Where(x => x.Generation == Interfaces.Types.CellGeneration.Current).Count();
        }

        public int GetGeneration()
        {
            return CellGeneration;
        }

        private int GetIndex(int x, int y)
        {
            return (x % Dimension) + (y * Dimension);
        }

        private int GetLiveNeighbourCount(int cx, int cy)
        {
            int liveTally = 0;
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    var currentX = cx + x;
                    var currentY = cy + y;

                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (GetCellAt(currentX, currentY).Generation == Interfaces.Types.CellGeneration.Current)
                            liveTally++;
                    }
                }
            }
            return liveTally;
        }
    }
}
