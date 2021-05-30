using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rogedo.LifeEngine.Domain;
using Rogedo.LifeEngine.Interfaces;

namespace Rogedo.LifeEngine.Test.Domain
{
    [TestClass]
    public class ArenaTests
    {
        [TestMethod]
        public void WhenArenaCreated_ThenNoErrorShouldBeThrown()
        {
            IArena arena = new Arena();
            Assert.IsNotNull(arena);
        }

        [TestMethod]
        public void WhenArenaInitialised_ThenItShouldBeTheCorrectSize()
        {
            IArena arena = new Arena();
            arena.Initialise(5);
            Assert.IsTrue(arena.GetArenaSize() == 25);
        }
    }
}
