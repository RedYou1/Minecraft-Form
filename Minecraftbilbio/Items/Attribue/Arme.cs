using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe public Arme qui est abstract et qui hérite de la classe Item
    /// </summary>
    public abstract class Arme : Item
    {

        protected int degat;

        protected Arme(int degat) : base(1,1)
        {
            this.degat = degat;
        }
        
        public override bool Equals(Item item)
        {
            return base.Equals(item) && item is Arme arme && degat == arme.degat;
        }

        public int Degat { get => degat; }
    }
}
