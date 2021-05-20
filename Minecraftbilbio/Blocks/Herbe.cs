using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe Herbe_Block qui hérite de Block
    /// </summary>
    public class Herbe_Block : Block
    {
        /// <summary>
        /// Constructeur de Herbe_Block
        /// </summary>
        public Herbe_Block() : base(1, "Herbe")
        {

        }

        public override bool Detruire(Joueur joueur)
        {
            joueur.AjouterItem(new Herbe_Item(1));
            return true;
        }

        public override Block Clone()
        {
            return new Herbe_Block();
        }

        public override Block Charger(string path)
        {
            return new Herbe_Block();
        }
    }
}
