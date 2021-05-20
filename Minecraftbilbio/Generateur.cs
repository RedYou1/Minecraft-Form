using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public abstract class Generateur
    {
        private string nom;
        private Noise noise;
        public Generateur(string nom, Noise noise)
        {
            this.nom = nom;
            this.noise = noise;
        }

        /// <summary>
        /// Genere un Chunk avec le coordoner de chunk a partir de block deja existant dans le chunk
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public abstract Tuple<Chunk, Tuple<int, int, Schematique>[], Entite[]> Generer(int cx, int cy, Chunk from);


        /// <summary>
        /// transform un coordoner block en chunk
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int Convert(int a)
        {
            if (a < 0) { a -= 15; }

            return a / 16;
        }

        public string Nom { get => nom; }

        public Noise Noise { get => noise; }
    }
}
