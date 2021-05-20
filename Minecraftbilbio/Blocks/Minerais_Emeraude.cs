using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe Minerais_Emeraude qui hérite de Block
    /// </summary>
    public class Minerais_Emeraude : Block
    {
        /// <summary>
        /// Constructeur de Minerais_Diamant
        /// </summary>
        public Minerais_Emeraude() : base(1, "Minerais_Emeraude")
        {

        }

        public override bool Detruire(Joueur joueur)
        {
            if (joueur.MainDroit() is Pioche pioche && pioche.Vitesse >= 4)
            {
                joueur.AjouterItem(new Emeraude(1));
                return true;
            }
            return false;
        }

        public override Block Clone()
        {
            return new Minerais_Emeraude();
        }

        public override Block Charger(string path)
        {
            return new Minerais_Emeraude();
        }
    }
}
