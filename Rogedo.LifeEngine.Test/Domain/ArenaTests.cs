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
            const int dim = 5;

            IArena arena = new Arena();
            arena.Initialise(dim);            
            Assert.IsTrue(arena.GetArenaSize() == dim * dim);
        }

        [TestMethod]
        public void WhenArenaInitialised_ThenSignatureShouldBeAsExpected()
        {
            const int dim = 2;
            const string expectedSignature = "0000";
            IArena arena = new Arena();
            arena.Initialise(dim);
            Assert.AreEqual(arena.GetSignature(), expectedSignature);
        }
    }
}
