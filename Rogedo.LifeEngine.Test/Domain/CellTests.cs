using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rogedo.LifeEngine.Domain;
using Rogedo.LifeEngine.Interfaces;

namespace Rogedo.LifeEngine.Test.Domain;

[TestClass]
public class CellTests
{
    [TestMethod]
    public void WhenCellCreated_ThenNoErrorShouldBeThrown()
    {
        ICell cell = new Cell();
        Assert.IsNotNull(cell);
    }

    [TestMethod]
    public void WhenCellCreated_ThenItShouldBeDead()
    {
        ICell cell = new Cell();
        Assert.IsTrue(cell.Generation == Types.CellGeneration.Dead);
    }

    [TestMethod]
    public void WhenCellGenerationIsChanged_ThenItShouldBeCorrect()
    {
        ICell cell = new Cell();
        cell.SetGeneration(Types.CellGeneration.Current);

        Assert.IsTrue(cell.Generation == Types.CellGeneration.Current);
    }
}