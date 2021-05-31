using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rogedo.LifeEngine.Tools.Test
{
    [TestClass]
    public class RandomArenaGeneratorTests
    {
        [TestMethod]
        public void WhenGeneratorExecuted_ThenThereShouldBeSomeDataPoints()
        {
            const int dimensions = 10;
            const int numberOfCells = 10;

            var generator = new RandomArenaGenerator();
            var data = generator.Execute(dimensions, numberOfCells);

            Assert.IsTrue(data.Count > 0);
        }
    }
}
