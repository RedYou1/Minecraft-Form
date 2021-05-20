using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minecraftbilbio;
using System;
using System.Collections.Generic;

namespace MinecraftingTest
{
    [TestClass]
    public class Four_Test
    {

        [TestMethod]
        public void Constructeur()
        {
            Four_Block f = new Four_Block();
            Assert.AreEqual(f.Inventaire.Nom, "FOUR");
            Assert.AreEqual(f.Inventaire.Longueur, 1);
            Assert.AreEqual(f.Inventaire.Hauteur, 3);
            Assert.AreEqual(f.Carburant, 0);
        }

        [TestMethod]
        public void Clone()
        {
            Four_Block f = new Four_Block().Clone() as Four_Block;
            Assert.AreEqual(f.Inventaire.Nom, "FOUR");
            Assert.AreEqual(f.Inventaire.Longueur, 1);
            Assert.AreEqual(f.Inventaire.Hauteur, 3);
            Assert.AreEqual(f.Carburant, 0);
        }

        [TestMethod]
        public void Detruire()
        {
            {
                Four_Block tbc = new Four_Block();
                tbc.Inventaire.SetItem(0, new TroncDeChene_Item(2));

                Joueur j = new Joueur(0, 0);

                bool a = tbc.Detruire(j);
                Assert.IsFalse(a);
                Assert.IsNull(j.Barre.GetItem(0));
                Assert.IsNull(j.Barre.GetItem(1));
                Assert.IsNull(j.Barre.GetItem(2));
                Assert.IsNull(j.Inventaire.GetItem(0));
                Assert.IsNull(j.Inventaire.GetItem(1));

                j.AjouterItem(new PiocheBois());
                bool b = tbc.Detruire(j);
                Assert.IsTrue(b);
                Assert.AreEqual(j.Barre.GetItem(0).id(), new PiocheBois().id());
                Assert.AreEqual(j.Barre.GetItem(1).id(), new Four_Item(1).id());
                Assert.AreEqual(j.Barre.GetItem(1).Quantite, 1);
                Assert.AreEqual(j.Barre.GetItem(2).id(), new TroncDeChene_Item(1).id());
                Assert.AreEqual(j.Barre.GetItem(2).Quantite, 2);
                Assert.IsNull(j.Inventaire.GetItem(0));
                Assert.IsNull(j.Inventaire.GetItem(1));
            }
            {
                Four_Block tbc = new Four_Block();
                tbc.Inventaire.SetItem(0, new TroncDeChene_Item(2));
                tbc.Inventaire.SetItem(1, new FeuilleDeChene_Item(2));
                tbc.Inventaire.SetItem(2, new Pierre_Item(2));

                Joueur j = new Joueur(0, 0);

                j.AjouterItem(new PiocheBois());
                bool b = tbc.Detruire(j);
                Assert.IsTrue(b);
                Assert.AreEqual(j.Barre.GetItem(0).id(), new PiocheBois().id());
                Assert.AreEqual(j.Barre.GetItem(1).id(), new Four_Item(1).id());
                Assert.AreEqual(j.Barre.GetItem(1).Quantite, 1);
                Assert.AreEqual(j.Barre.GetItem(2).id(), new TroncDeChene_Item(1).id());
                Assert.AreEqual(j.Barre.GetItem(2).Quantite, 2);
                Assert.AreEqual(j.Inventaire.GetItem(0).id(), new FeuilleDeChene_Item(1).id());
                Assert.AreEqual(j.Inventaire.GetItem(0).Quantite, 2);
                Assert.AreEqual(j.Inventaire.GetItem(1).id(), new Pierre_Item(1).id());
                Assert.AreEqual(j.Inventaire.GetItem(1).Quantite, 2);
            }
        }

        [TestMethod]
        public void Update()
        {
            Four_Block f = new Four_Block();

            f.Inventaire.SetItem(1, new Charbon(1));
            f.Update();
            Assert.IsNull(f.Inventaire.GetItem(0));
            Assert.IsNull(f.Inventaire.GetItem(2));
            Assert.AreEqual(f.Carburant, 0);
            Assert.AreEqual(f.Inventaire.GetItem(1).id(), new Charbon(1).id());
            Assert.AreEqual(f.Inventaire.GetItem(1).Quantite, 1);

            f.Inventaire.SetItem(0, new Minerais_Fer_Item(1));
            f.Update();
            Assert.IsNull(f.Inventaire.GetItem(0));
            Assert.IsNull(f.Inventaire.GetItem(1));
            Assert.AreEqual(f.Carburant, 70);
            Assert.AreEqual(f.Inventaire.GetItem(2).id(), new Fer(1).id());
            Assert.AreEqual(f.Inventaire.GetItem(2).Quantite, 1);

            f.Inventaire.SetItem(0, new Minerais_Fer_Item(1));
            f.Update();
            Assert.IsNull(f.Inventaire.GetItem(0));
            Assert.IsNull(f.Inventaire.GetItem(1));
            Assert.AreEqual(f.Carburant, 60);
            Assert.AreEqual(f.Inventaire.GetItem(2).id(), new Fer(1).id());
            Assert.AreEqual(f.Inventaire.GetItem(2).Quantite, 2);

            f.Inventaire.SetItem(0, new Minerais_Fer_Item(64));
            f.Inventaire.SetItem(1, new Charbon(64));
            f.Update();
            Assert.AreEqual(f.Carburant, 0);
            Assert.AreEqual(f.Inventaire.GetItem(0).id(), new Minerais_Fer_Item(1).id());
            Assert.AreEqual(f.Inventaire.GetItem(0).Quantite, 2);
            Assert.AreEqual(f.Inventaire.GetItem(1).id(), new Charbon(1).id());
            Assert.AreEqual(f.Inventaire.GetItem(1).Quantite, 57);
            Assert.AreEqual(f.Inventaire.GetItem(2).id(), new Fer(1).id());
            Assert.AreEqual(f.Inventaire.GetItem(2).Quantite, 64);

            f.Inventaire.SetItem(0, new Minerais_Fer_Item(10));
            f.Inventaire.SetItem(1, new Charbon(1));
            f.Inventaire.SetItem(2, null);
            f.Update();
            Assert.AreEqual(f.Carburant, 0);
            Assert.AreEqual(f.Inventaire.GetItem(0).id(), new Minerais_Fer_Item(1).id());
            Assert.AreEqual(f.Inventaire.GetItem(0).Quantite, 2);
            Assert.IsNull(f.Inventaire.GetItem(1));
            Assert.AreEqual(f.Inventaire.GetItem(2).id(), new Fer(1).id());
            Assert.AreEqual(f.Inventaire.GetItem(2).Quantite, 8);
        }

        [TestMethod]
        public void CliqueDroit()
        {
            Four_Block f = new Four_Block();
            Joueur j = new Joueur(0, 0);

            Tuple<bool, Tuple<Ecrans, object>> h = f.CliqueDroit(j);
            Tuple<Ecrans, object> g = h.Item2;
            Assert.AreEqual(g.Item1, Ecrans.Four);

            KeyValuePair<Four_Block, Inventaire[]> l = (KeyValuePair<Four_Block, Inventaire[]>)g.Item2;
            Assert.AreEqual(l.Key, f);
            Assert.AreEqual(l.Value.Length, 3);
            Assert.AreEqual(l.Value[0], f.Inventaire);
            Assert.AreEqual(l.Value[1], j.Inventaire);
            Assert.AreEqual(l.Value[2], j.Barre);
        }
    }
}
