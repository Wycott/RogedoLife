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
            const int Dim = 5;

            IArena arena = new Arena();
            arena.Initialise(Dim);

            Assert.IsTrue(arena.GetArenaSize() == Dim * Dim);
        }

        [TestMethod]
        public void WhenArenaInitialised_ThenSignatureShouldBeAsExpected()
        {
            const int Dim = 2;
            const string ExpectedSignature = "0000";
            const int ExpectedGeneration = 1;

            IArena arena = new Arena();
            arena.Initialise(Dim);

            Assert.AreEqual(ExpectedSignature, arena.GetSignature());
            Assert.AreEqual(ExpectedGeneration, arena.GetGeneration());

        }

        [TestMethod]
        public void WhenArenaInitialisedAndSeeded_ThenSignatureShouldBeAsExpected()
        {
            const int Dim = 2;
            const string ExpectedSignature = "1001";
            const int ExpectedGeneration = 1;

            IArena arena = new Arena();
            arena.Initialise(Dim);
            arena.Seed(0, 0);
            arena.Seed(1, 1);

            Assert.AreEqual(ExpectedSignature, arena.GetSignature());
            Assert.AreEqual(ExpectedGeneration, arena.GetGeneration());
        }

        [TestMethod]
        public void WhenArenaInitialisedAndSeededWithOscillator_ThenSignatureAndCountShouldBeAsExpectedAtEachStage()
        {
            const int Dim = 5;
            const string ExpectedSignature1 = "0000000100001000010000000";
            const string ExpectedSignature2 = "0000000000011100000000000";
            const int ExpectedPopulation = 3;
            const int ExpectedGeneration = 2;

            IArena arena = new Arena();
            arena.Initialise(Dim);

            arena.Seed(2, 1);
            arena.Seed(2, 2);
            arena.Seed(2, 3);
            var firstSignature = arena.GetSignature();
            var firstCount = arena.GetPopulation();
            arena.MakeNextGeneration();
            var secondSignature = arena.GetSignature();
            var secondCount = arena.GetPopulation();

            Assert.AreEqual(ExpectedSignature1, firstSignature);
            Assert.AreEqual(ExpectedSignature2, secondSignature);
            Assert.AreEqual(ExpectedPopulation, firstCount);
            Assert.AreEqual(ExpectedPopulation, secondCount);
            Assert.AreEqual(ExpectedGeneration, arena.GetGeneration());
        }

        [TestMethod]
        public void WhenArenaInitialisedAndSeededWithStillLife_ThenSignatureAndCountShouldBeAsExpectedAtEachStage()
        {
            const int Dim = 4;
            const string ExpectedSignature = "0000011001100000";
            const int ExpectedPopulation = 4;
            const int ExpectedGeneration = 2;

            IArena arena = new Arena();
            arena.Initialise(Dim);

            arena.Seed(1, 1);
            arena.Seed(1, 2);
            arena.Seed(2, 1);
            arena.Seed(2, 2);

            var firstSignature = arena.GetSignature();
            var firstCount = arena.GetPopulation();
            arena.MakeNextGeneration();
            var secondSignature = arena.GetSignature();
            var secondCount = arena.GetPopulation();

            Assert.AreEqual(ExpectedSignature, firstSignature);
            Assert.AreEqual(ExpectedSignature, secondSignature);
            Assert.AreEqual(ExpectedPopulation, firstCount);
            Assert.AreEqual(ExpectedPopulation, secondCount);
            Assert.AreEqual(ExpectedGeneration, arena.GetGeneration());
        }
    }
}
