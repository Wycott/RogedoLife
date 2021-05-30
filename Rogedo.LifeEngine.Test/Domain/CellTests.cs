using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rogedo.LifeEngine.Domain;
using Rogedo.LifeEngine.Interfaces;

namespace Rogedo.LifeEngine.Test.Domain
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void WhenCellCreated_ThenNoErrorShouldBeThrown()
        {
            ICell cell = new Cell();
            Assert.IsNotNull(cell);
        }
    }
}
