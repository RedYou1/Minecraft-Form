using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minecraftbilbio;
using System;

namespace MinecraftingTest
{
    [TestClass]
    public class Vache_Test
    {
        [TestMethod]
        public void Bouger()
        {
            Monde m = new Monde(new FlatGenerateur(new Noise()));
            m.GenerateChunk(0, 0);
            Vache joueur = new Vache(0, 0);
            joueur.Bouger(2, 2, m);
            Assert.AreEqual(joueur.X, 1);
            Assert.AreEqual(joueur.Y, 0);
            joueur.Bouger(-1, -5, m);
            Assert.AreEqual(joueur.X, 0);
            Assert.AreEqual(joueur.Y, 0);
        }

        [TestMethod]
        public void Tp()
        {
            Vache joueur = new Vache(0, 0);
            joueur.Tp(2, 2);
            Assert.AreEqual(joueur.X, 2);
            Assert.AreEqual(joueur.Y, 2);
            joueur.Tp(-1, -5);
            Assert.AreEqual(joueur.X, -1);
            Assert.AreEqual(joueur.Y, -5);
        }

        [TestMethod]
        public void id()
        {
            Assert.AreEqual(new Vache(0, 0).id(), "Vache");
        }

        [TestMethod]
        public void Equals()
        {
            Monde m = new Monde(new FlatGenerateur(new Noise()));
            Vache j1 = new Vache(0, 0);

            Vache j2 = new Vache(0, 0);
            Assert.IsTrue(j1.Equals(j2));
            Assert.IsTrue(j2.Equals(j1));
            j2.Bouger(1, 0, m);
            Assert.IsFalse(j1.Equals(j2));
            Assert.IsFalse(j2.Equals(j1));
        }

        [TestMethod]
        public void CliqueGauche()
        {
            Monde monde = new Monde(new FlatGenerateur(new Noise()));
            Vache j1 = new Vache(0, 0);
            monde.Entites.Add(j1);
            Assert.AreEqual(j1.Vie, 20);

            Joueur joueur = new Joueur(0, 0);
            monde.Entites.Add(joueur);
            Assert.AreEqual(joueur.Vie, 20);

            for (int i = 19; i > 0; i--)
            {
                j1.CliqueGauche(joueur, 1, monde);
                Assert.AreEqual(j1.Vie, i);
                Assert.AreEqual(joueur.Vie, 20);
                Assert.IsTrue(monde.Entites.Contains(j1));
                Assert.IsTrue(monde.Entites.Contains(joueur));
            }

            joueur.AjouterItem(new EpeeBois());
            Assert.IsTrue(j1.CliqueGauche(joueur, 1, monde));
            Assert.IsFalse(monde.Entites.Contains(j1));
            Assert.IsTrue(monde.Entites.Contains(joueur));
        }

        [TestMethod]
        public void CliqueDroite()
        {
            Assert.IsNull(new Vache(0, 0).CliqueDroite(null));
        }
    }
}
