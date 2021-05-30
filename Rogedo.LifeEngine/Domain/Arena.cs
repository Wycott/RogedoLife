using Rogedo.LifeEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rogedo.LifeEngine.Domain
{
    public class Arena : IArena
    {
        private List<Cell> ArenaCells { get; set; }

        public Arena()
        {
            ArenaCells = new List<Cell>();
        }

        public int GetArenaSize()
        {
            return ArenaCells.Count;
        }

        public void Initialise(int dimension)
        {
            ArenaCells = new List<Cell>();

            for (int x = 0; x < dimension; x++)
            {
                for (int y = 0; y < dimension; y++)
                {
                    var cell = new Cell();
                    ArenaCells.Add(cell);
                }
            }
        }
    }
}
