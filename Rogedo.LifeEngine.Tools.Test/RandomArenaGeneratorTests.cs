using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rogedo.LifeEngine.Tools.Test;

[TestClass]
public class RandomArenaGeneratorTests
{
    [TestMethod]
    public void WhenGeneratorExecutedForSmallDimension_ThenThereShouldBeSomeDataPoints()
    {
        const int Dimensions = 5;
        const int NumberOfCells = 5;

        var generator = new RandomArenaGenerator();
        var data = generator.Execute(Dimensions, NumberOfCells);

        Assert.IsTrue(data.Count > 0);
    }

    [TestMethod]
    public void WhenGeneratorExecutedForLargerDimension_ThenThereShouldBeSomeDataPoints()
    {
        const int Dimensions = 10;
        const int NumberOfCells = 10;

        var generator = new RandomArenaGenerator();
        var data = generator.Execute(Dimensions, NumberOfCells);

        Assert.IsTrue(data.Count > 0);
    }
}