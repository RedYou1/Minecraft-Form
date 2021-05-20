using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Class Block_fer qui herite de Block
    /// </summary>
    public class Block_Fer : Block
    {
        /// <summary>
        /// Constructeur Block_fer
        /// </summary>
        public Block_Fer() : base(1, "Block_Fer")
        {

        }

        public override bool Detruire(Joueur joueur)
        {
            if (joueur.MainDroit() is Pioche pioche)
            {
                joueur.AjouterItem(new Fer_Item_Block(1));
                return true;
            }
            return false;
        }

        public override Block Clone()
        {
            return new Block_Fer();
        }

        public override Block Charger(string path)
        {
            return new Block_Fer();
        }
    }
}
