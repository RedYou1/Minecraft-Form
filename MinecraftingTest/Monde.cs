using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minecraftbilbio;
using System;
using System.Collections.Generic;

namespace MinecraftingTest
{
    [TestClass]
    public class Monde_Test
    {
        [TestMethod]
        public void Constructeur()
        {
            Generateur g = new GenerateurParDefault(new Noise());
            Monde m = new Monde(g);

            Assert.AreEqual(m.Chunks.Count, 0);
            Assert.AreEqual(m.Entites.Count, 0);
            Assert.AreEqual(m.Generateur, g);
        }

        [TestMethod]
        public void GenerateChunk()
        {
            {
                Generateur g = new FlatGenerateur(new Noise());
                Monde m = new Monde(g);
                Chunk c = g.Generer(0, 0, new Chunk(new Block[16, 16])).Item1;
                m.GenerateChunk(0, 0);
                for (int x = 0; x < 16; x++)
                    for (int y = 0; y < 16; y++)
                    {
                        Block b1 = m.Chunks["0/0"].Blocks[x, y];
                        Block b2 = c.Blocks[x, y];
                        Assert.IsTrue((b1 == null && b2 == null) || (b1 != null && b2 != null && b1.Name == b2.Name));
                    }
            }
            {
                Generateur g = new GenerateurParDefault(new Noise(100));
                Monde m = new Monde(g);
                Tuple<Chunk, Tuple<int, int, Schematique>[], Entite[]> s = g.Generer(0, 0, new Chunk(new Block[16, 16]));
                Chunk c = s.Item1;

                foreach (Tuple<int, int, Schematique> shematic in s.Item2)
                    foreach (KeyValuePair<string, Block> b in shematic.Item3.Blocks)
                    {
                        string[] a = b.Key.Split('/');
                        int x = int.Parse(a[0]) + shematic.Item1;
                        int y = int.Parse(a[1]) + shematic.Item1;
                        if (x >= 0 && x < 16 && y >= 0 && y < 16)
                        {
                            c.SetBlock(x % 16, y % 16, b.Value);
                        }
                    }

                m.GenerateChunk(0, 0);
                for (int x = 0; x < 16; x++)
                    for (int y = 0; y < 16; y++)
                    {
                        Block b1 = m.Chunks["0/0"].Blocks[x, y];
                        Block b2 = c.Blocks[x, y];
                        Assert.IsTrue((b1 == null && b2 == null) || (b1 != null && b2 != null && b1.Name == b2.Name));
                    }
            }
        }

        [TestMethod]
        public void GetMaxHeight()
        {
            Generateur g = new FlatGenerateur(new Noise());
            Monde m = new Monde(g);
            m.GenerateChunk(0, -1);
            m.GenerateChunk(0, 0);
            Assert.AreEqual(m.GetMaxHeight(0, false, true), -1);
            Assert.AreEqual(m.GetMaxHeight(0, false, false), -1);
            m.SetBlock(2, 0, new Echelle_Block());
            Assert.AreEqual(m.GetMaxHeight(2, false, true), 0);
            Assert.AreEqual(m.GetMaxHeight(2, false, false), -1);
            Assert.AreEqual(m.GetMaxHeight(2, true, false), 0);
            Assert.AreEqual(m.GetMaxHeight(2, true, true), 0);

            g = new FlatGenerateur(new Noise());
            m = new Monde(g);
            m.GenerateChunk(0, 0);
            Assert.AreEqual(m.GetMaxHeight(0, false, true), int.MinValue);
            Assert.AreEqual(m.GetMaxHeight(0, false, false), int.MinValue);
            m.SetBlock(2, 0, new Echelle_Block());
            Assert.AreEqual(m.GetMaxHeight(2, false, false), int.MinValue);
            Assert.AreEqual(m.GetMaxHeight(2, false, true), 0);
            Assert.AreEqual(m.GetMaxHeight(2, true, false), 0);
            Assert.AreEqual(m.GetMaxHeight(2, true, true), 0);
        }

        [TestMethod]
        public void Chunks()
        {
            Generateur g = new FlatGenerateur(new Noise());
            Monde m = new Monde(g);

            Chunk c = g.Generer(0, 0, new Chunk(new Block[16, 16])).Item1;
            m.SetBlockChunk(0, 0, c);
            Assert.AreEqual(m.GetBlockChunk(0, 0), c);

            m.RemoveBlockChunk(0, 0);
            Assert.IsNull(m.GetBlockChunk(0, 0));

            m.AddBlockChunk(0, 0, c);
            Assert.AreEqual(m.GetBlockChunk(0, 0), c);

            m.RemoveBlockChunk(0, 0);
            Assert.IsNull(m.GetBlockChunk(0, 0));
        }

        [TestMethod]
        public void GetBlock()
        {
            Generateur g = new FlatGenerateur(new Noise());
            Monde m = new Monde(g);

            Assert.IsNull(m.GetBlock(0, 0, false));
            Assert.IsNull(m.GetBlock(0, 0, true));
            Assert.IsNull(m.GetBlock(0, -1, false));
            Assert.AreEqual(m.GetBlock(0, -1, true).Name, "Terre");
        }

        [TestMethod]
        public void GetEntity()
        {
            Generateur g = new FlatGenerateur(new Noise());
            Monde m = new Monde(g);

            Assert.IsNull(m.GetEntite(0, 0));

            Entite ent = new Joueur(0, 0);
            m.Entites.Add(ent);
            Assert.AreEqual(m.GetEntite(0, 0), ent);
        }
    }
}
