using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minecraftbilbio;
using System;
using System.IO;

namespace MinecraftingTest
{
    [TestClass]
    public class Joueur_Test
    {
        [TestMethod]
        public void Sauvegarder()
        {
            Directory.CreateDirectory("C:\\tempMinecraftTest\\");
            Joueur j1 = new Joueur(100, -512);
            j1.Casque = new CasqueCuire();
            j1.Plastron = new PlastronFer();
            j1.Jambiere = new JambiereOr();
            j1.Botte = new BotteDiamant();
            j1.Casque.Durabiliter -= 5;
            j1.Plastron.Durabiliter -= 10;
            j1.Jambiere.Durabiliter -= 15;
            j1.Botte.Durabiliter -= 20;
            j1.AjouterItem(new PlancheDeChene(5));
            Sacados it = new Sacados();
            it.Inventaire.AjouterItem(new PlancheDeChene(5));
            j1.AjouterItem(it);
            j1.Sauvegarder("C:\\tempMinecraftTest\\");
            Joueur j2 = Entite.Entites()[new Joueur(0, 0).id()].Charger("C:\\tempMinecraftTest\\") as Joueur;
            Assert.IsTrue(j1.Equals(j2));
            Directory.Delete("C:\\tempMinecraftTest\\", true);
        }

        [TestMethod]
        public void Clone()
        {
            Joueur j1 = new Joueur(0, 0);
            j1.Tp(10, -6);
            j1.Faim -= 2;
            j1.Inventaire.SetItem(2, new PlancheDeChene(12));
            j1.AjouterItem(new Four_Item(1));
            j1.Casque = new CasqueDiamant();
            j1.Botte = new BotteCuire();
            j1.Plastron = new PlastronDiamant();
            j1.Jambiere = new JambiereCuire();
            j1.Vie--;
            j1.Maindroite = 1;
            Joueur j2 = (Joueur)j1.Clone();
            Assert.IsTrue(j1.Equals(j2));
            j1.Faim -= 2;
            j1.Vie--;
            j1.Plastron = new PlastronFer();
            j1.Jambiere = new JambiereOr();
            j1.Plastron = new PlastronFer();
            j1.Jambiere = new JambiereOr();
            j1.Tp(50, -16);
            Assert.IsFalse(j1.Equals(j2));
        }

        [TestMethod]
        public void Bouger()
        {
            Monde m = new Monde(new VoidGenerateur(new Noise()));
            m.SetBlock(0, -1, new Terre_Block());
            m.SetBlock(1, 0, new Terre_Block());
            Joueur joueur = new Joueur(0, 0);
            joueur.Bouger(1, 0, m);
            Assert.AreEqual(joueur.X, 1);
            Assert.AreEqual(joueur.Y, 1);
            joueur.Bouger(-1, -5, m);
            Assert.AreEqual(joueur.X, 0);
            Assert.AreEqual(joueur.Y, 0);

            joueur.Bouger(-1, 0, m);
            Assert.AreEqual(joueur.X, -1);
            Assert.AreEqual(joueur.Y, Sauvegarde.bordureYMin + 1);
            Assert.AreEqual(joueur.Vie, 0);
        }

        [TestMethod]
        public void Tp()
        {
            Joueur joueur = new Joueur(0, 0);
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
            Assert.AreEqual(new Joueur(0, 0).id(), "Joueur");
        }

        [TestMethod]
        public void Equals()
        {
            Joueur j1 = new Joueur(0, 0);

            Assert.IsNull(j1.Crafting.GetItem(0));
            Assert.IsNull(j1.Crafting.GetItem(1));
            Assert.IsNull(j1.Crafting.GetItem(2));
            Assert.IsNull(j1.Crafting.GetItem(3));

            Joueur j2 = new Joueur(0, 0);
            Assert.IsTrue(j1.Equals(j2));
            Assert.IsTrue(j2.Equals(j1));
            j2.Faim--;
            j2.Vie--;
            Assert.IsFalse(j1.Equals(j2));
            Assert.IsFalse(j2.Equals(j1));
        }

        [TestMethod]
        public void FaimVie()
        {
            Joueur j = new Joueur(0, 0);
            Assert.AreEqual(j.Faim, 20);
            Assert.AreEqual(j.Vie, 20);
            j.Faim = -5;
            j.Vie = -5;
            Assert.AreEqual(j.Faim, 0);
            Assert.AreEqual(j.Vie, 0);
            j.Faim = 30;
            j.Vie = 30;
            Assert.AreEqual(j.Faim, 20);
            Assert.AreEqual(j.Vie, 20);
        }

        [TestMethod]
        public void Armure()
        {
            Joueur j1 = new Joueur(0, 0);
            j1.Casque = new CasqueCuire();
            j1.Plastron = new PlastronCuire();
            j1.Jambiere = new JambiereCuire();
            j1.Botte = new BotteCuire();

            Assert.AreEqual(j1.Vie, 20);

            for (int i = j1.Casque.MaxDurabiliter; i > 0; i--)
            {
                Assert.AreEqual(j1.Casque.Durabiliter, i);
                j1.CliqueGauche(j1, 1, null);
            }
            Assert.IsNull(j1.Casque);

            Assert.AreEqual(j1.Vie, 20);

            for (int i = j1.Plastron.MaxDurabiliter; i > 0; i--)
            {
                Assert.AreEqual(j1.Plastron.Durabiliter, i);
                j1.CliqueGauche(j1, 1, null);
            }
            Assert.IsNull(j1.Plastron);

            Assert.AreEqual(j1.Vie, 20);

            for (int i = j1.Jambiere.MaxDurabiliter; i > 0; i--)
            {
                Assert.AreEqual(j1.Jambiere.Durabiliter, i);
                j1.CliqueGauche(j1, 1, null);
            }
            Assert.IsNull(j1.Jambiere);

            Assert.AreEqual(j1.Vie, 20);

            for (int i = j1.Botte.MaxDurabiliter; i > 0; i--)
            {
                Assert.AreEqual(j1.Botte.Durabiliter, i);
                j1.CliqueGauche(j1, 1, null);
            }
            Assert.IsNull(j1.Botte);

            Assert.AreEqual(j1.Vie, 20);
            j1.CliqueGauche(j1, 1, null);
            Assert.AreEqual(j1.Vie, 19);
        }


        [TestMethod]
        public void CliqueGauche()
        {
            Monde monde = new Monde(new FlatGenerateur(new Noise()));

            Joueur j1 = new Joueur(0, 0);
            Joueur j2 = new Joueur(0, 0);

            monde.Entites.Add(j1);
            monde.Entites.Add(j2);

            Assert.AreEqual(j1.Vie, j2.Vie);

            j1.CliqueGauche(j2, 1, monde);

            Assert.AreEqual(j1.Vie + 1, j2.Vie);

            j1.AjouterItem(new EpeeBois());

            j2.CliqueGauche(j1, 1, monde);

            Assert.AreEqual(j1.Vie, j2.Vie + 1);

            Assert.IsNull(j1.Casque);
            Assert.IsNull(j1.Plastron);
            Assert.IsNull(j1.Jambiere);
            Assert.IsNull(j1.Botte);

            j1.Casque = new CasqueCuire();
            j1.Plastron = new PlastronCuire();
            j1.Jambiere = new JambiereCuire();
            j1.Botte = new BotteCuire();

            Assert.AreEqual(j1.Casque.id(), new CasqueCuire().id());
            Assert.AreEqual(j1.Plastron.id(), new PlastronCuire().id());
            Assert.AreEqual(j1.Jambiere.id(), new JambiereCuire().id());
            Assert.AreEqual(j1.Botte.id(), new BotteCuire().id());

            j2.AjouterItem(new EpeeDiamant());
            j2.Maindroite = 1;

            j1.CliqueGauche(j2, 1, monde);

            int degat = new EpeeDiamant().Degat - new CasqueCuire().Resistance
                - new PlastronCuire().Resistance
                - new JambiereCuire().Resistance
                - new BotteCuire().Resistance;

            Assert.AreEqual(j1.Vie - degat, j2.Vie + 1);

            j1.Barre.SetItem(0, new EpeeDiamant());

            Assert.AreEqual(j1.Barre.GetItem(j1.Maindroite).id(), "EpeeDiamant");

            Assert.IsTrue(monde.Entites.Contains(j2));

            j2.CliqueGauche(j1, 1, monde);
            j2.CliqueGauche(j1, 1, monde);
            j2.CliqueGauche(j1, 1, monde);
            j2.CliqueGauche(j1, 1, monde);

            Assert.IsFalse(monde.Entites.Contains(j2));
        }

        [TestMethod]
        public void AjouterItemEtEnleverItem()
        {
            Joueur j1 = new Joueur(0, 0);

            Assert.IsNull(j1.Barre.GetItem(0));
            Assert.IsNull(j1.Barre.GetItem(1));
            Assert.IsNull(j1.Barre.GetItem(2));
            Assert.IsNull(j1.Inventaire.GetItem(0));

            Item i1 = new EpeeDiamant();
            j1.AjouterItem(i1);
            Assert.AreEqual(j1.Barre.GetItem(0).id(), i1.id());
            Assert.IsNull(j1.Barre.GetItem(1));
            Assert.IsNull(j1.Barre.GetItem(2));
            Assert.IsNull(j1.Inventaire.GetItem(0));

            Item i2 = new EpeeDiamant();
            j1.AjouterItem(i2);
            Assert.AreEqual(j1.Barre.GetItem(0).id(), i1.id());
            Assert.AreEqual(j1.Barre.GetItem(1).id(), i2.id());
            Assert.IsNull(j1.Barre.GetItem(2));
            Assert.IsNull(j1.Inventaire.GetItem(0));

            Item i3 = new EpeeDiamant();
            j1.AjouterItem(i3);
            Assert.AreEqual(j1.Barre.GetItem(0).id(), i1.id());
            Assert.AreEqual(j1.Barre.GetItem(1).id(), i2.id());
            Assert.AreEqual(j1.Barre.GetItem(2).id(), i3.id());
            Assert.IsNull(j1.Inventaire.GetItem(0));

            Item i4 = new EpeeDiamant();
            j1.AjouterItem(i4);
            Assert.AreEqual(j1.Barre.GetItem(0).id(), i1.id());
            Assert.AreEqual(j1.Barre.GetItem(1).id(), i2.id());
            Assert.AreEqual(j1.Barre.GetItem(2).id(), i3.id());
            Assert.AreEqual(j1.Inventaire.GetItem(0).id(), i4.id());

            i1 = new EpeeDiamant();
            j1.EnleverItem(i1);
            Assert.IsNull(j1.Barre.GetItem(0));
            Assert.AreEqual(j1.Barre.GetItem(1).id(), i2.id());
            Assert.AreEqual(j1.Barre.GetItem(2).id(), i3.id());
            Assert.AreEqual(j1.Inventaire.GetItem(0).id(), i4.id());

            i2 = new EpeeDiamant();
            j1.EnleverItem(i2);
            Assert.IsNull(j1.Barre.GetItem(0));
            Assert.IsNull(j1.Barre.GetItem(1));
            Assert.AreEqual(j1.Barre.GetItem(2).id(), i3.id());
            Assert.AreEqual(j1.Inventaire.GetItem(0).id(), i4.id());

            i3 = new EpeeDiamant();
            j1.EnleverItem(i3);
            Assert.IsNull(j1.Barre.GetItem(0));
            Assert.IsNull(j1.Barre.GetItem(1));
            Assert.IsNull(j1.Barre.GetItem(2));
            Assert.AreEqual(j1.Inventaire.GetItem(0).id(), i4.id());

            i4 = new EpeeDiamant();
            j1.EnleverItem(i4);
            Assert.IsNull(j1.Barre.GetItem(0));
            Assert.IsNull(j1.Barre.GetItem(1));
            Assert.IsNull(j1.Barre.GetItem(2));
            Assert.IsNull(j1.Inventaire.GetItem(0));
        }

        [TestMethod]
        public void ContientItem()
        {
            Joueur j1 = new Joueur(0, 0);
            Item i = new EpeeDiamant();
            Assert.IsFalse(j1.ContientItem(i));
            j1.AjouterItem(i);
            Assert.IsTrue(j1.ContientItem(i));
            i = new EpeeDiamant();
            j1.EnleverItem(i);
            Assert.IsFalse(j1.ContientItem(i));
        }
    }
}
