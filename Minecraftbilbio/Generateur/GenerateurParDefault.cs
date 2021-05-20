using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe public GenerateurParDefault qui hérite de Generateur
    /// </summary>
    public class GenerateurParDefault : Generateur
    {
        public GenerateurParDefault(Noise noise) : base("GenerateurParDefault", noise)
        {
        }

        /// <summary>
        /// des vagues de terre 3 de haut sur le dessus</br>
        /// du fer a partir de -20 et max a -100</br>
        /// de l'or a partir de -120 et max a -200</br>
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns></returns>
        public override Tuple<Chunk, Tuple<int, int, Schematique>[], Entite[]> Generer(int cx, int cy, Chunk from)
        {
            List<Tuple<int, int, Schematique>> sche = new List<Tuple<int, int, Schematique>>();
            Block[,] blocks = from.Blocks;
            List<Entite> entities = new List<Entite>();
            for (int x = 0; x < 16; x++)
                for (int y = 0; y < 16; y++)
                {
                    if (blocks[x, y] == null)
                    {
                        int bx = x + cx * 16;
                        int by = y + cy * 16;

                        int max = (int)Math.Round(Noise.Evaluate((double)bx / 10000.0, 0) * 256);
                        if (max >= by)
                        {
                            if (max - 3 > by)
                            {
                                blocks[x, y] = new Roche_Block();

                                double loc = (Noise.Evaluate((double)bx / 10, 0, (double)by / 10, 1) + 1) / 2;
                                Random r = new Random((int)(loc * 1000000));

                                if (by < -2 && r.NextDouble() < .1)
                                {
                                    blocks[x, y] = new Minerais_Charbon();
                                }

                                if (by < -10 && r.NextDouble() < .1)
                                {
                                    blocks[x, y] = new Minerais_Fer();
                                }

                                if (by < -20 && r.NextDouble() < .1)
                                {
                                    blocks[x, y] = new Minerais_Or();
                                }

                                if (by < -50 && r.NextDouble() < .05)
                                {
                                    blocks[x, y] = new Minerais_Diamant();
                                }

                                if (by < -50 && r.NextDouble() < .05)
                                {
                                    blocks[x, y] = new Minerais_Emeraude();
                                }
                            }
                            else if (max == by)
                            {
                                blocks[x, y] = new Herbe_Block();
                            }
                            else
                            {
                                blocks[x, y] = new Terre_Block();
                            }
                        }

                        if (max >= cy * 16 && max < cy * 16 + 16 && y == 0)
                        {
                            double loc = Noise.Evaluate((double)bx / 10, 0) / 2;
                            Random r = new Random((int)(loc * 1000000));
                            if (r.NextDouble() < .1)
                            {
                                sche.Add(new Tuple<int, int, Schematique>(bx, max + 1, new Arbre()));
                            }
                            if (r.NextDouble() < .1)
                            {
                                entities.Add(new Vache(bx, max + 1));
                            }
                            if (r.NextDouble() < .01)
                            {
                                sche.Add(new Tuple<int, int, Schematique>(bx, max, new MaisonVillagePlaine1()));
                            }
                        }
                    }
                }
            return new Tuple<Chunk, Tuple<int, int, Schematique>[], Entite[]>(new Chunk(blocks, true), sche.ToArray(), entities.ToArray());
        }
    }
}
