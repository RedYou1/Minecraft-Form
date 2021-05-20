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
    public class Pierre_Block : Block
    {
        /// <summary>
        /// Constructeur de Pierre_Block
        /// </summary>
        public Pierre_Block() : base(2, "Pierre")
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
            return new Pierre_Block();
        }

        public override Block Charger(string path)
        {
            return new Pierre_Block();
        }
    }
}
