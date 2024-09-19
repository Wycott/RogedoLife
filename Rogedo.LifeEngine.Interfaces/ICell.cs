using Rogedo.LifeEngine.Types;

namespace Rogedo.LifeEngine.Interfaces;

public interface ICell
{
    CellGeneration Generation { get; }
    void SetGeneration(CellGeneration generation);
}