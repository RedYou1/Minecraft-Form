using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Botte qui est abstract et qui hérite de la classe Armrure
    /// </summary>
    public abstract class Botte : Armure
    {
        public Botte(int resistance, int durabiliter, int maxDurabiliter) : base(resistance, durabiliter, maxDurabiliter)
        {

        }
    }
}
