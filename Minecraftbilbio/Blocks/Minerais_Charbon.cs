using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe Minerais_Fer qui hérite de Block
    /// </summary>
    public class Minerais_Charbon : Block
    {
        /// <summary>
        /// Constructeur de Minerais_fer
        /// </summary>
        public Minerais_Charbon() : base(1, "Minerais_Charbon")
        {

        }

        public override bool Detruire(Joueur joueur)
        {
            if (joueur.MainDroit() is Pioche)
            {
                joueur.AjouterItem(new Charbon(1));
                return true;
            }
            return false;
        }

        public override Block Clone()
        {
            return new Minerais_Charbon();
        }

        public override Block Charger(string path)
        {
            return new Minerais_Charbon();
        }
    }
}
