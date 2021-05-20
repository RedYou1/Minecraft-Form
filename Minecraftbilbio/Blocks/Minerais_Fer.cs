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
    public class Minerais_Fer : Block
    {
        /// <summary>
        /// Constructeur de Minerais_fer
        /// </summary>
        public Minerais_Fer() : base(1, "Minerais_Fer")
        {

        }

        public override bool Detruire(Joueur joueur)
        {
            if (joueur.MainDroit() is Pioche)
            {
                joueur.AjouterItem(new Minerais_Fer_Item(1));
                return true;
            }
            return false;
        }

        public override Block Clone()
        {
            return new Minerais_Fer();
        }

        public override Block Charger(string path)
        {
            return new Minerais_Fer();
        }
    }
}
