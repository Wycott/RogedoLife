using System.Collections.Generic;

namespace Rogedo.LifeEngine.Interfaces
{
    public interface IArena
    {
        List<ICell> ArenaCells { get; }
        void Initialise(int dimension);
        int GetArenaSize();
        string GetSignature();
        void Seed(int x, int y);
        ICell GetCellAt(int x, int y);
        void MakeNextGeneration();
        int GetPopulation();
        int GetGeneration();
        bool Repeating { get; }
        void InitialiseRandomly(int dimension);
        void Pad();
        int CurrentDimension { get; }
    }
}
