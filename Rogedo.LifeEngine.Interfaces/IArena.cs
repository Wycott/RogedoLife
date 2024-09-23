using System.Collections.Generic;

namespace Rogedo.LifeEngine.Interfaces;

public interface IArena
{
    bool Repeating { get; }

    void Pad();
    void Seed(int x, int y);
    void MakeNextGeneration();
    void Initialise(int dimension);
    void InitialiseRandomly(int dimension);
    
    int GetArenaSize();
    int GetPopulation();
    int GetGeneration();
    int CurrentDimension { get; }

    string GetSignature();

    ICell GetCellAt(int x, int y);

    List<ICell> ArenaCells { get; }
}