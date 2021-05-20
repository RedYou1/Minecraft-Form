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
    public class VoidGenerateur : Generateur
    {

        public VoidGenerateur(Noise noise) : base("VoidGenerateur", noise)
        {

        }

        /// <summary>
        /// que du null
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns></returns>
        public override Tuple<Chunk, Tuple<int, int, Schematique>[], Entite[]> Generer(int cx, int cy, Chunk from)
        {
            Block[,] blocks = from.Blocks;
            return new Tuple<Chunk, Tuple<int, int, Schematique>[], Entite[]>(new Chunk(blocks, true), new Tuple<int, int, Schematique>[0], new Entite[0]);
        }
    }
}
