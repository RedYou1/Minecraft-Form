using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Class Echelle_Block qui herite de Block
    /// </summary>
    public class Echelle_Block : Block
    {
        /// <summary>
        /// Constructeur Echelle_Block
        /// </summary>
        public Echelle_Block() : base(1, "Echelle")
        {

        }

        public override bool CanPassThrough(Entite ent, bool byGravity)
        {
            return !byGravity;
        }

        public override bool Detruire(Joueur joueur)
        {
            joueur.AjouterItem(new Echelle_Item(1));
            return true;
        }

        public override Block Clone()
        {
            return new Echelle_Block();
        }

        public override Block Charger(string path)
        {
            return new Echelle_Block();
        }
    }
}
