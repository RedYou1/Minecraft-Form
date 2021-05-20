using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Minecraftbilbio;

namespace MinecraftingTest
{
    [TestClass]
    public class Craft_Test
    {
        [TestMethod]
        public void CheckAll()
        {
            Item[,] from = new Item[,] {
                { null,null,null},
                { null,null,null},
                { null,null,null}
            };
            Item to = new Emeraude(1);
            Craft c = new Craft(from, to);
            Assert.AreEqual(c.From,from);
            Assert.AreEqual(c.To, to);
        }
    }
}
