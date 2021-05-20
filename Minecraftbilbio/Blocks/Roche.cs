using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe de Pierre_Block qui hérite de Block
    /// </summary>
    public class Roche_Block : Block
    {
        /// <summary>
        /// Constructeur de Pierre_Block
        /// </summary>
        public Roche_Block() : base(2, "Roche")
        {

        }

        public override bool Detruire(Joueur joueur)
        {
            if (joueur.MainDroit() is Pioche)
            {
                joueur.AjouterItem(new Pierre_Item(1));
                return true;
            }
            return false;
        }

        public override Block Clone()
        {
            return new Roche_Block();
        }

        public override Block Charger(string path)
        {
            return new Roche_Block();
        }
    }
}
