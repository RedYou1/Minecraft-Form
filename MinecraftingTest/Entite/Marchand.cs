using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minecraftbilbio;
using System;

namespace MinecraftingTest
{
    [TestClass]
    public class Marchand_Test
    {
        [TestMethod]
        public void Bouger()
        {
            Monde m = new Monde(new FlatGenerateur(new Noise()));
            m.GenerateChunk(0, 0);
            Marchand joueur = new Marchand(0, 0, null);
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
            Marchand joueur = new Marchand(0, 0, null);
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
            Assert.AreEqual(new Marchand(0, 0, new Minecraftbilbio.Echange[0]).id(), "Marchand");
        }

        [TestMethod]
        public void Equals()
        {
            Monde m = new Monde(new FlatGenerateur(new Noise()));
            Marchand j1 = new Marchand(0, 0, null);

            Marchand j2 = new Marchand(0, 0, null);
            Assert.IsTrue(j1.Equals(j2));
            Assert.IsTrue(j2.Equals(j1));
            j2.Bouger(1, 0, m);
            Assert.IsFalse(j1.Equals(j2));
            Assert.IsFalse(j2.Equals(j1));
        }

        [TestMethod]
        public void Echanger()
        {
            Joueur p = new Joueur(0, 0);

            Echange[] e = new Echange[]
            {
                new Echange(new Emeraude(1),new Boeuf_Cuit(1)),
                new Echange(new Boeuf_Cuit(1),new Emeraude(1))
            };

            Marchand m = new Marchand(0, 0, e);

            Assert.AreEqual(m.Echanges, e);

            m.Inventaire.AjouterItem(new Emeraude(1));
            m.Inventaire.AjouterItem(new Boeuf_Cuit(1));

            Assert.IsNull(p.Barre.GetItem(0));
            p.AjouterItem(new Emeraude(1));
            Assert.IsTrue(p.Barre.GetItem(0).Equals(new Emeraude(1)));
            Assert.IsFalse(p.Barre.GetItem(0).Equals(new Boeuf_Cuit(1)));

            m.Echanger(p, 0);
            Assert.IsFalse(p.Barre.GetItem(0).Equals(new Emeraude(1)));
            Assert.IsTrue(p.Barre.GetItem(0).Equals(new Boeuf_Cuit(1)));

            m.Echanger(p, 1);
            Assert.IsTrue(p.Barre.GetItem(0).Equals(new Emeraude(1)));
            Assert.IsFalse(p.Barre.GetItem(0).Equals(new Boeuf_Cuit(1)));

            m.Echanger(p, 1);
            Assert.IsTrue(p.Barre.GetItem(0).Equals(new Emeraude(1)));
            Assert.IsFalse(p.Barre.GetItem(0).Equals(new Boeuf_Cuit(1)));
        }

        [TestMethod]
        public void CliqueDroite()
        {
            Marchand marchand = new Marchand(0, 0, null);
            Tuple<Ecrans, object> t = new Tuple<Ecrans, object>(Ecrans.Marchand, marchand);
            Tuple<Ecrans, object> m = marchand.CliqueDroite(null);
            Assert.AreEqual(m.Item1, t.Item1);
            Assert.AreEqual(m.Item2, t.Item2);
        }
    }
}
