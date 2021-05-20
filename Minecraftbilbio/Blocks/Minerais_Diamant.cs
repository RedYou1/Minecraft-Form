using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe Minerais_Diamant qui hérite de Block
    /// </summary>
    public class Minerais_Diamant : Block
    {
        /// <summary>
        /// Constructeur de Minerais_Diamant
        /// </summary>
        public Minerais_Diamant() : base(1, "Minerais_Diamant")
        {

        }

        public override bool Detruire(Joueur joueur)
        {
            if (joueur.MainDroit() is Pioche pioche && pioche.Vitesse >= 4)
            {
                joueur.AjouterItem(new Diamant(1));
                return true;
            }
            return false;
        }

        public override Block Clone()
        {
            return new Minerais_Diamant();
        }

        public override Block Charger(string path)
        {
            return new Minerais_Diamant();
        }
    }
}
