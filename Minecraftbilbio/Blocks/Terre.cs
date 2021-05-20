using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe Terre_Block qui hérite de Block
    /// </summary>
    public class Terre_Block : Block
    {
        /// <summary>
        /// Constructeur de Terre_Block
        /// </summary>
        public Terre_Block() : base(1, "Terre")
        {

        }

        public override bool Detruire(Joueur joueur)
        {
            joueur.AjouterItem(new Terre_Item(1));
            return true;
        }

        public override Block Clone()
        {
            return new Terre_Block();
        }

        public override Block Charger(string path)
        {
            return new Terre_Block();
        }
    }
}
