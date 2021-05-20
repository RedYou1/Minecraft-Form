using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe Minerais_Or qui hérite de Block
    /// </summary>
    public class Minerais_Or : Block
    {
        /// <summary>
        /// Constructeur de Minerais_Or
        /// </summary>
        public Minerais_Or() : base(1, "Minerais_Or")
        {

        }

        public override bool Detruire(Joueur joueur)
        {
            if (joueur.MainDroit() is Pioche)
            {
                joueur.AjouterItem(new Minerais_Or_Item(1));
                return true;
            }
            return false;
        }

        public override Block Clone()
        {
            return new Minerais_Or();
        }

        public override Block Charger(string path)
        {
            return new Minerais_Or();
        }
    }
}
