using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minecraftbilbio;
using System;

namespace MinecraftingTest
{
    [TestClass]
    public class Echange_Test
    {
        [TestMethod]
        public void Constructeur()
        {
            Echange ec = new Echange(new EpeeBois(), new BotteFer());

            Assert.AreEqual(ec.ItemVoulu.id(), new EpeeBois().id());
            Assert.IsNull(ec.ItemVoulu2);
            Assert.AreEqual(ec.ItemDonne.id(), new BotteFer().id());

            ec = new Echange(new EpeeBois(), new PlastronCuire(), new BotteFer());

            Assert.AreEqual(ec.ItemVoulu.id(), new EpeeBois().id());
            Assert.AreEqual(ec.ItemVoulu2.id(), new PlastronCuire().id());
            Assert.AreEqual(ec.ItemDonne.id(), new BotteFer().id());

            ec = new Echange(null, new EpeeBois(), new BotteFer());

            Assert.AreEqual(ec.ItemVoulu.id(), new EpeeBois().id());
            Assert.IsNull(ec.ItemVoulu2);
            Assert.AreEqual(ec.ItemDonne.id(), new BotteFer().id());
        }

        [TestMethod]
        public void EqualsEtClone()
        {
            Echange e1 = new Echange(new EpeeBois(), new PlastronCuire(), new BotteFer());
            Echange e2 = e1.Clone();
            Assert.IsTrue(e1.Equals(e2));
            e1 = new Echange(null, null);
            Assert.IsFalse(e1.Equals(e2));
            Assert.IsFalse(e1.Equals(null));
        }
    }
}
