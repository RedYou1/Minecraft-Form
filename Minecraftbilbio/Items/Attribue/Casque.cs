using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Casque qui est abstract et qui hérite de la classe Armure
    /// </summary>
    public abstract class Casque : Armure
    {
        public Casque(int resistance, int durabiliter, int maxDurabiliter) : base(resistance, durabiliter, maxDurabiliter)
        {

        }
    }
}
