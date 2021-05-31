using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rogedo.LifeEngine.Tools.Test
{
    [TestClass]
    public class RandomArenaGeneratorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            const int expectedNumberOfCells = 10;
            var generator = new RandomArenaGenerator();
            var data = generator.Execute(10, expectedNumberOfCells);

            Assert.IsTrue(data.Count > 0);
        }
    }
}
