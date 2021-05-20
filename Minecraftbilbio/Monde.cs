using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class Monde
    {
        private Dictionary<string, Chunk> chunks;
        private Dictionary<string, Chunk> backChunks;
        private List<Entite> entites;
        private Generateur generateur;

        public Monde(Generateur generateur)
        {
            chunks = new Dictionary<string, Chunk>();
            backChunks = new Dictionary<string, Chunk>();
            entites = new List<Entite>();
            this.generateur = generateur;
        }

        public Monde(Generateur generateur, List<Entite> ents)
        {
            chunks = new Dictionary<string, Chunk>();
            backChunks = new Dictionary<string, Chunk>();
            entites = ents;
            this.generateur = generateur;
        }

        public bool Equals(Monde monde)
        {
            if (monde == null)
            {
                return false;
            }

            if (chunks.Count != monde.chunks.Count)
            {
                return false;
            }

            if (entites.Count != monde.entites.Count)
            {
                return false;
            }

            if (generateur.Nom != monde.generateur.Nom || generateur.Noise.Seed != monde.generateur.Noise.Seed)
            {
                return false;
            }

            foreach (string i in chunks.Keys)
            {
                if (!monde.chunks.ContainsKey(i))
                {
                    return false;
                }
                if (!chunks[i].Equals(monde.chunks[i]))
                {
                    return false;
                }
            }

            for (int i = 0; i < entites.Count; i++)
            {
                if ((entites[i] == null) != (monde.entites[i] == null))
                {
                    return false;
                }
                if (entites[i] == null && monde.entites[i] == null)
                {
                    continue;
                }
                if (!entites[i].Equals(monde.entites[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Cree un chunk avec le generateur et le met
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        public void GenerateChunk(int cx, int cy)
        {
            Chunk chunk = new Chunk(new Block[16, 16]);
            string a = cx + "/" + cy;
            if (chunks.ContainsKey(a))
            {
                chunk = chunks[a];
            }

            if (!chunk.Generer)
            {
                Tuple<Chunk, Tuple<int, int, Schematique>[], Entite[]> s = generateur.Generer(cx, cy, chunk);
                AddChunk(cx, cy, s.Item1);

                foreach (Tuple<int, int, Schematique> sche in s.Item2)
                {
                    sche.Item3.AppliquerSchema(sche.Item1, sche.Item2, this);
                }

                entites.AddRange(s.Item3);
            }
        }

        /// <summary>
        /// Cree un chunk avec le generateur
        /// </summary>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        public void GenerateBlockChunk(int bx, int by)
        {
            if (bx < 0) { bx -= 15; }
            if (by < 0) { by -= 15; }

            int cx = bx / 16;
            int cy = by / 16;
            GenerateChunk(cx, cy);
        }

        /// <summary>
        /// recupere un chunk
        /// </summary>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <returns>peut etre null</returns>
        public Chunk GetBlockChunk(int bx, int by)
        {
            if (bx < 0) { bx -= 15; }
            if (by < 0) { by -= 15; }

            int cx = bx / 16;
            int cy = by / 16;
            return GetChunk(cx, cy);
        }

        /// <summary>
        /// recupere un chunk
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns>peut etre null</returns>
        public Chunk GetChunk(int cx, int cy)
        {
            string a = cx + "/" + cy;
            if (chunks.ContainsKey(a))
            {
                return chunks[a];
            }
            return null;
        }

        /// <summary>
        /// recupere un chunk de font
        /// </summary>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <returns>peut etre null</returns>
        public Chunk GetBlockBackChunk(int bx, int by)
        {
            if (bx < 0) { bx -= 15; }
            if (by < 0) { by -= 15; }

            int cx = bx / 16;
            int cy = by / 16;
            return GetBackChunk(cx, cy);
        }

        /// <summary>
        /// recupere un chunk de font
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns>peut etre null</returns>
        public Chunk GetBackChunk(int cx, int cy)
        {
            string a = cx + "/" + cy;
            if (backChunks.ContainsKey(a))
            {
                return backChunks[a];
            }
            return null;
        }

        /// <summary>
        /// overwrite un chunk
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="chunk"></param>
        public void SetChunk(int cx, int cy, Chunk chunk)
        {
            if (chunk != null)
            {
                chunks[cx + "/" + cy] = chunk;
            }
        }

        /// <summary>
        /// overwrite un chunk
        /// </summary>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="chunk"></param>
        public void SetBlockChunk(int bx, int by, Chunk chunk)
        {
            if (bx < 0) { bx -= 15; }
            if (by < 0) { by -= 15; }

            int cx = bx / 16;
            int cy = by / 16;
            SetChunk(cx, cy, chunk);
        }

        /// <summary>
        /// enleve un chunk (s'il exists)
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        public void RemoveChunk(int cx, int cy)
        {
            if (chunks.ContainsKey(cx + "/" + cy))
            {
                chunks.Remove(cx + "/" + cy);
            }
        }

        /// <summary>
        /// enleve un chunk (s'il exists)
        /// </summary>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        public void RemoveBlockChunk(int bx, int by)
        {
            if (bx < 0) { bx -= 15; }
            if (by < 0) { by -= 15; }

            int cx = bx / 16;
            int cy = by / 16;
            RemoveChunk(cx, cy);
        }

        /// <summary>
        /// ajoute un chunk
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="chunk"></param>
        public void AddChunk(int cx, int cy, Chunk chunk)
        {
            if (!chunks.ContainsKey(cx + "/" + cy))
            {
                chunks.Add(cx + "/" + cy, chunk);
            }
        }

        /// <summary>
        /// ajoute un chunk
        /// </summary>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="chunk"></param>
        public void AddBlockChunk(int bx, int by, Chunk chunk)
        {
            if (bx < 0) { bx -= 15; }
            if (by < 0) { by -= 15; }

            int cx = bx / 16;
            int cy = by / 16;
            AddChunk(cx, cy, chunk);
        }

        /// <summary>
        /// recupere la hauteur du block le plus haut de a ce x
        /// </summary>
        /// <param name="x"></param>
        /// <param name="justBlock">verifi si just un block</param>
        /// <param name="falling">verifi si peux etre dessus sans que la graviter le fasse dessendre</param>
        /// <returns>peut etre tout les valeur possible de int (si int.MinValue c'est qu'il n'y a pas de block a ce x)</returns>
        public int GetMaxHeight(int x, bool justBlock, bool falling)
        {
            int t = x;
            if (x < 0) { x -= 15; }

            int cx = t / 16;

            int tx = x % 16;
            if (x < 0) { tx += 15; }

            string[] a = chunks.Keys.ToArray();

            int h = int.MinValue;

            foreach (string i in a)
            {
                if (i.StartsWith(cx + "/"))
                {
                    int cy = int.Parse(i.Split('/')[1]);
                    if (cy * 16 > h)
                    {
                        Block[,] blocks = chunks[i].Blocks;
                        for (int y = 15; y >= 0; y--)
                        {
                            if (blocks[tx, y] != null && (justBlock || !blocks[tx, y].CanPassThrough(new Joueur(x, y + cy * 16 + 1), falling)))
                            {
                                int th = y + cy * 16;
                                if (th > h)
                                {
                                    h = th;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            return h;
        }

        /// <summary>
        /// recupere un block de font
        /// </summary>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <returns>peut etre null</returns>
        public Block GetBackBlock(int bx, int by)
        {
            Chunk chunk = GetBlockBackChunk(bx, by);
            if (chunk == null)
            {
                backChunks.Add(GenerateurParDefault.Convert(bx) + "/" + GenerateurParDefault.Convert(by), new Chunk(new Block[16, 16], true));
                chunk = GetBlockBackChunk(bx, by);
            }

            int tx = bx % 16;
            int ty = by % 16;
            if (bx < 0) { tx += 15; }
            if (by < 0) { ty += 15; }
            return chunk.GetBlock(tx, ty);
        }

        /// <summary>
        /// recupere un block
        /// </summary>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="generate"></param>
        /// <returns>peut etre null</returns>
        public Block GetBlock(int bx, int by, bool generate)
        {
            if (bx == Sauvegarde.bordureXMin || bx == Sauvegarde.bordureXMax ||
                by == Sauvegarde.bordureYMin || by == Sauvegarde.bordureYMax)
            {
                return new BedRock();
            }

            Chunk chunk = GetBlockChunk(bx, by);
            if (chunk == null)
            {
                if (generate)
                {
                    GenerateBlockChunk(bx, by);
                    chunk = GetBlockChunk(bx, by);
                }
                else
                {
                    return null;
                }
            }
            int tx = bx % 16;
            int ty = by % 16;
            if (bx < 0) { tx += 15; }
            if (by < 0) { ty += 15; }
            return chunk.GetBlock(tx, ty);
        }

        /// <summary>
        /// place un block (ou enleve si null)</br>
        /// si l'option "piece" est a true va update le font autoure de lui
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="block"></param>
        public void SetBlock(int x, int y, Block block)
        {
            Chunk chunk = GetBlockChunk(x, y);
            int tx = x % 16;
            int ty = y % 16;
            if (x < 0) { tx += 15; }
            if (y < 0) { ty += 15; }

            if (chunk == null)
            {
                chunk = new Chunk(new Block[16, 16]);
                AddBlockChunk(x, y, chunk);
            }

            chunk.SetBlock(tx, ty, block);

            if (Sauvegarde.piece)
            {
                for (int zx = x - 1; zx <= x + 1; zx++)
                    for (int zy = y - 1; zy <= y + 1; zy++)
                    {
                        if (Math.Abs(zx) != 1 || Math.Abs(zy) != 1)
                        {
                            List<Point> ps = new List<Point>();
                            Block b = Inclosed(zx, zy, zx, zy, new List<KeyValuePair<Block, int>>(), ps, 50);
                            foreach (Point p in ps)
                            {
                                if (GetBlock(p.X, p.Y, false) == null)
                                {
                                    SetBackBlock(p.X, p.Y, b);
                                }
                            }
                        }
                    }
            }
        }

        /// <summary>
        /// ajoute un block a la list
        /// </summary>
        /// <param name="bs">le nombre de fois de chacun des type de block</param>
        /// <param name="block"></param>
        private void Add(List<KeyValuePair<Block, int>> bs, Block block)
        {
            foreach (KeyValuePair<Block, int> a in bs)
            {
                if ((a.Key == null && block == null) || (a.Key != null && block != null && a.Key.Name == block.Name))
                {
                    bs.Remove(a);
                    bs.Add(new KeyValuePair<Block, int>(a.Key, a.Value + 1));
                    return;
                }
            }
            bs.Add(new KeyValuePair<Block, int>(block, 1));
        }

        /// <summary>
        /// met a jour le font autoure dun block</br>
        /// tu doit l'appeler pour les quatre cote du block
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="sx"></param>
        /// <param name="sy"></param>
        /// <param name="bs"></param>
        /// <param name="po"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        private Block Inclosed(int x, int y, int sx, int sy, List<KeyValuePair<Block, int>> bs, List<Point> po, int range)
        {
            Block b = GetBlock(x, y, false);
            po.Add(new Point(x, y));
            if (b != null || Math.Abs(x - sx) >= range || Math.Abs(y - sy) >= range)
            {
                Add(bs, b);
                return b;
            }

            if (!po.Contains(new Point(x - 1, y)))
            {
                if (Inclosed(x - 1, y, sx, sy, bs, po, range) == null)
                {
                    return null;
                }
            }
            if (!po.Contains(new Point(x + 1, y)))
            {
                if (Inclosed(x + 1, y, sx, sy, bs, po, range) == null)
                {
                    return null;
                }
            }
            if (!po.Contains(new Point(x, y - 1)))
            {
                if (Inclosed(x, y - 1, sx, sy, bs, po, range) == null)
                {
                    return null;
                }
            }
            if (!po.Contains(new Point(x, y + 1)))
            {
                if (Inclosed(x, y + 1, sx, sy, bs, po, range) == null)
                {
                    return null;
                }
            }

            KeyValuePair<Block, int> max = new KeyValuePair<Block, int>(null, int.MinValue);
            foreach (KeyValuePair<Block, int> a in bs)
            {
                if (a.Value >= max.Value)
                {
                    max = a;
                }
            }

            return max.Key;
        }

        /// <summary>
        /// place un block dans le font
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="block"></param>
        public void SetBackBlock(int x, int y, Block block)
        {
            Chunk chunk = GetBlockBackChunk(x, y);
            int tx = x % 16;
            int ty = y % 16;
            if (x < 0) { tx += 15; }
            if (y < 0) { ty += 15; }

            if (chunk == null)
            {
                chunk = new Chunk(new Block[16, 16], true);
                backChunks.Add(GenerateurParDefault.Convert(x) + "/" + GenerateurParDefault.Convert(y), chunk);
            }

            chunk.SetBlock(tx, ty, block);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>The first Entity with this coordinate</returns>
        public Entite GetEntite(float x, float y)
        {
            x = (float)Math.Round(x);
            y = (float)Math.Round(y);
            foreach (Entite ent in entites)
            {
                if ((float)Math.Round(ent.X) == x && (float)Math.Round(ent.Y) == y)
                {
                    return ent;
                }
            }
            return null;
        }

        /// <summary>
        /// clone le monde
        /// </summary>
        /// <returns></returns>
        public Monde Clone()
        {
            Monde monde = new Monde(generateur);
            foreach (KeyValuePair<string, Chunk> c in chunks)
            {
                monde.chunks.Add(c.Key, c.Value.Clone());
            }
            foreach (Entite ent in entites)
            {
                monde.entites.Add(ent.Clone());
            }
            return monde;
        }

        public Dictionary<string, Chunk> Chunks { get => chunks; }
        public Dictionary<string, Chunk> BackChunks { get => backChunks; }
        public List<Entite> Entites { get => entites; }

        public Generateur Generateur { get => generateur; }
    }
}
