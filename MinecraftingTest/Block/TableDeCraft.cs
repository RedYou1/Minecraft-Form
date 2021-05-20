using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minecraftbilbio;
using System;
using System.Collections.Generic;

namespace MinecraftingTest
{
    [TestClass]
    public class TableDeCraft_Test
    {

        [TestMethod]
        public void Constructeur()
        {
            TableDeCraft tbc = new TableDeCraft();
            Assert.AreEqual(tbc.Inventaire.Nom, "CRAFT");
            Assert.AreEqual(tbc.Inventaire.Longueur, 3);
            Assert.AreEqual(tbc.Inventaire.Hauteur, 3);
        }

        [TestMethod]
        public void Clone()
        {
            TableDeCraft tbc = new TableDeCraft().Clone() as TableDeCraft;
            Assert.AreEqual(tbc.Inventaire.Nom, "CRAFT");
            Assert.AreEqual(tbc.Inventaire.Longueur, 3);
            Assert.AreEqual(tbc.Inventaire.Hauteur, 3);
        }

        [TestMethod]
        public void CheckAll()
        {
            Inventaire inv = new Inventaire("", 3, 3);
            Assert.IsNull(TableDeCraft.CheckAll(inv));

            inv = new Inventaire("", 4, 4);
            Assert.IsNull(TableDeCraft.CheckAll(inv));

            inv.SetItem(3, new TroncDeChene_Item(1));
            Assert.IsNull(TableDeCraft.CheckAll(inv));

            inv.SetItem(0, new TroncDeChene_Item(32));
            Craft c = TableDeCraft.CheckAll(inv);
            Assert.AreEqual(c.To.id(), new PlancheDeChene(1).id());
            Assert.AreEqual(c.To.Quantite, 4);
        }

        [TestMethod]
        public void Detruire()
        {
            TableDeCraft tbc = new TableDeCraft();
            tbc.Inventaire.SetItem(0, new TroncDeChene_Item(2));

            Joueur j = new Joueur(0, 0);
            bool a = tbc.Detruire(j);
            Assert.IsTrue(a);
            Assert.AreEqual(j.Barre.GetItem(0).id(), new TableDeCraft_Item(1).id());
            Assert.AreEqual(j.Barre.GetItem(0).Quantite, 1);
            Assert.AreEqual(j.Barre.GetItem(1).id(), new TroncDeChene_Item(1).id());
            Assert.AreEqual(j.Barre.GetItem(1).Quantite, 2);
        }

        [TestMethod]
        public void CliqueDroit()
        {
            Joueur j = new Joueur(0, 0);
            TableDeCraft t = new TableDeCraft();
            Tuple<bool, Tuple<Ecrans, object>> v = t.CliqueDroit(j);
            Assert.AreEqual(v.Item2.Item1, Ecrans.TableCraft);
            Assert.AreEqual(((Inventaire[])v.Item2.Item2)[0], t.Inventaire);
        }

        [TestMethod]
        public void CraftIt()
        {
            TableDeCraft t = new TableDeCraft();
            Joueur j = new Joueur(0, 0);

            Assert.IsNull(j.Barre.GetItem(0));
            TableDeCraft.CraftIt(t.Inventaire, j);
            Assert.IsNull(j.Barre.GetItem(0));

            t.Inventaire.SetItem(0, new TroncDeChene_Item(1));
            TableDeCraft.CraftIt(t.Inventaire, j);
            Assert.AreEqual(j.Barre.GetItem(0).id(), new PlancheDeChene(1).id());
            Assert.AreEqual(j.Barre.GetItem(0).Quantite, 4);

            j.Barre.SetItem(0, new Fer(1));
            j.Barre.SetItem(1, new Fer(1));
            j.Barre.SetItem(2, new Fer(1));

            j.Inventaire.SetItem(0, new Fer(1));
            j.Inventaire.SetItem(1, new Fer(1));
            j.Inventaire.SetItem(2, new Fer(1));
            j.Inventaire.SetItem(3, new Fer(1));
            j.Inventaire.SetItem(4, new Fer(1));
            j.Inventaire.SetItem(5, new Fer(1));

            t.Inventaire.SetItem(0, new TroncDeChene_Item(1));
            TableDeCraft.CraftIt(t.Inventaire, j);
            Assert.AreEqual(j.Barre.GetItem(0).id(), new Fer(1).id());
            Assert.AreEqual(j.Barre.GetItem(0).Quantite, 1);
            Assert.AreEqual(j.Barre.GetItem(1).id(), new Fer(1).id());
            Assert.AreEqual(j.Barre.GetItem(1).Quantite, 1);
            Assert.AreEqual(j.Barre.GetItem(2).id(), new Fer(1).id());
            Assert.AreEqual(j.Barre.GetItem(2).Quantite, 1);

            Assert.AreEqual(j.Inventaire.GetItem(0).id(), new Fer(1).id());
            Assert.AreEqual(j.Inventaire.GetItem(0).Quantite, 1);
            Assert.AreEqual(j.Inventaire.GetItem(1).id(), new Fer(1).id());
            Assert.AreEqual(j.Inventaire.GetItem(1).Quantite, 1);
            Assert.AreEqual(j.Inventaire.GetItem(2).id(), new Fer(1).id());
            Assert.AreEqual(j.Inventaire.GetItem(2).Quantite, 1);
            Assert.AreEqual(j.Inventaire.GetItem(3).id(), new Fer(1).id());
            Assert.AreEqual(j.Inventaire.GetItem(3).Quantite, 1);
            Assert.AreEqual(j.Inventaire.GetItem(4).id(), new Fer(1).id());
            Assert.AreEqual(j.Inventaire.GetItem(4).Quantite, 1);
            Assert.AreEqual(j.Inventaire.GetItem(5).id(), new Fer(1).id());
            Assert.AreEqual(j.Inventaire.GetItem(5).Quantite, 1);
        }
    }
}
