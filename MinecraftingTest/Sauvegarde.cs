using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minecraftbilbio;
using System;
using System.IO;

namespace MinecraftingTest
{
    [TestClass]
    public class Sauvegarde_Test
    {
        [TestMethod]
        public void Monde()
        {
            Monde m = new Monde(new GenerateurParDefault(new Noise()));

            Sauvegarde.joueur = new Joueur(0, 0);
            m.Entites.Add(Sauvegarde.joueur);
            m.Entites.Add(new Zombie(0, 0));
            for (int x = -2; x <= 2; x++)
                for (int y = -2; y <= 2; y++)
                {
                    m.GenerateChunk(x, y);
                }

            Sauvegarde.monde = m.Clone();
            Sauvegarde.SauvegarderMonde(Sauvegarde.monde, "C:\\testMinecraftMonde");
            Sauvegarde.ChargerMonde("C:\\testMinecraftMonde");
            Assert.IsTrue(Sauvegarde.monde.Equals(m));
            Directory.Delete("C:\\testMinecraftMonde", true);
        }
    }
}
