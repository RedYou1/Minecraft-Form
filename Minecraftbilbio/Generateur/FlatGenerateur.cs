using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe public FlatGenerateur qui hérite de Generateur
    /// </summary>
    public class FlatGenerateur : Generateur
    {

        public FlatGenerateur(Noise noise) : base("FlatGenerateur", noise)
        {

        }

        /// <summary>
        /// met de la terre en dessous de y=0
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns></returns>
        public override Tuple<Chunk, Tuple<int, int, Schematique>[], Entite[]> Generer(int cx, int cy, Chunk from)
        {
            Block[,] blocks = from.Blocks;
            if (cy < 0)
            {
                for (int x = 0; x < 16; x++)
                    for (int y = 0; y < 16; y++)
                    {
                        blocks[x, y] = new Terre_Block();
                    }
            }
            return new Tuple<Chunk, Tuple<int, int, Schematique>[], Entite[]>(new Chunk(blocks, true), new Tuple<int, int, Schematique>[0], new Entite[0]);
        }
    }
}
