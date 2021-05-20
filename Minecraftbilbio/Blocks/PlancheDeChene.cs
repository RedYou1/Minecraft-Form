using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe de PlancheDeChene_Block qui hérite de Block
    /// </summary>
    public class PlancheDeChene_Block : Block
    {
        /// <summary>
        /// Constructeur de PlancheDeChene_Block
        /// </summary>
        public PlancheDeChene_Block() : base(1, "PlancheDeChene")
        {

        }

        public override bool Detruire(Joueur joueur)
        {
            joueur.AjouterItem(new PlancheDeChene(1));
            return true;
        }

        public override Block Clone()
        {
            return new PlancheDeChene_Block();
        }

        public override Block Charger(string path)
        {
            return new PlancheDeChene_Block();
        }
    }
}
