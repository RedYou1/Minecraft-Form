using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Class BedRock qui herite de Block
    /// </summary>
    public class BedRock : Block
    {
        /// <summary>
        /// Constructeur BedRock
        /// </summary>
        public BedRock() : base(1, "BedRock")
        {

        }

        public override bool Detruire(Joueur joueur)
        {
            return false;
        }

        public override Block Clone()
        {
            return new BedRock();
        }

        public override Block Charger(string path)
        {
            return new BedRock();
        }
    }
}
