﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rogedo.LifeEngine.Domain;
using Rogedo.LifeEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
