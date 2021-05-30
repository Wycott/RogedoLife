using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rogedo.LifeEngine.Domain;
using Rogedo.LifeEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rogedo.LifeEngine.Test.Domain
{
    [TestClass]
    class CellTests
    {
        [TestMethod]
        public void WhenCellCreated_ThenNoErrorShouldBeThrown()
        {
            ICell cell = new Cell();
            Assert.IsNotNull(cell);
        }
    }
}
