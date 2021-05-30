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
            const int expectedGeneration = 0;

            IArena arena = new Arena();
            arena.Initialise(dim);

            Assert.AreEqual(expectedSignature, arena.GetSignature());
            Assert.AreEqual(expectedGeneration, arena.GetGeneration());

        }

        [TestMethod]
        public void WhenArenaInitialisedAndSeeded_ThenSignatureShouldBeAsExpected()
        {
            const int dim = 2;
            const string expectedSignature = "1001";
            const int expectedGeneration = 0;

            IArena arena = new Arena();
            arena.Initialise(dim);
            arena.Seed(0, 0);
            arena.Seed(1, 1);

            Assert.AreEqual(expectedSignature, arena.GetSignature());
            Assert.AreEqual(expectedGeneration, arena.GetGeneration());
        }

        [TestMethod]
        public void WhenArenaInitialisedAndSeededWithOscillator_ThenSignatureAndCountShouldBeAsExpectedAtEachStage()
        {
            const int dim = 5;
            const string expectedSignature1 = "0000000100001000010000000";
            const string expectedSignature2 = "0000000000011100000000000";
            const int expectedPopulation = 3;
            const int expectedGeneration = 1;

            IArena arena = new Arena();
            arena.Initialise(dim);
            
            arena.Seed(2, 1);
            arena.Seed(2, 2);
            arena.Seed(2, 3);
            var firstSignature = arena.GetSignature();
            var firstCount = arena.GetPopulation();
            arena.MakeNextGeneration();
            var secondSignature = arena.GetSignature();
            var secondCount = arena.GetPopulation();

            Assert.AreEqual(expectedSignature1, firstSignature);
            Assert.AreEqual(expectedSignature2, secondSignature);
            Assert.AreEqual(expectedPopulation, firstCount);
            Assert.AreEqual(expectedPopulation, secondCount);
            Assert.AreEqual(expectedGeneration, arena.GetGeneration());
        }

        [TestMethod]
        public void WhenArenaInitialisedAndSeededWithStillLife_ThenSignatureAndCountShouldBeAsExpectedAtEachStage()
        {
            const int dim = 4;
            const string expectedSignature = "0000011001100000";            
            const int expectedPopulation = 4;
            const int expectedGeneration = 1;

            IArena arena = new Arena();
            arena.Initialise(dim);

            arena.Seed(1, 1);
            arena.Seed(1, 2);
            arena.Seed(2, 1);
            arena.Seed(2, 2);

            var firstSignature = arena.GetSignature();
            var firstCount = arena.GetPopulation();
            arena.MakeNextGeneration();
            var secondSignature = arena.GetSignature();
            var secondCount = arena.GetPopulation();

            Assert.AreEqual(expectedSignature, firstSignature);
            Assert.AreEqual(expectedSignature, secondSignature);
            Assert.AreEqual(expectedPopulation, firstCount);
            Assert.AreEqual(expectedPopulation, secondCount);
            Assert.AreEqual(expectedGeneration, arena.GetGeneration());
        }
    }
}
