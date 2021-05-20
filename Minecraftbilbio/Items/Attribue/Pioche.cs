using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Pioche qui est abstract et qui hérite de la classe Item et de l'interface Cassablee
    /// </summary>
    public abstract class Pioche : Item, Cassable
    {
        private int vitesse;
        protected int durabiliter;
        private int maxDurabiliter;
        public Pioche(int vitesse, int durabiliter, int maxDurabiliter) : base(1, 1)
        {
            this.vitesse = vitesse;
            this.durabiliter = durabiliter;
            this.maxDurabiliter = maxDurabiliter;
        }

        public override bool Equals(Item item)
        {
            return base.Equals(item) && item is Pioche pioche && vitesse == pioche.vitesse && durabiliter == pioche.durabiliter && maxDurabiliter == pioche.maxDurabiliter;
        }

        public override Tuple<bool, Tuple<Ecrans, object>> CliqueGauche(Joueur joueur, int bx, int by, Block block, Entite entite, Monde monde)
        {
            if (block != null)
            {
                durabiliter--;
                if (durabiliter == 0)
                {
                    joueur.EnleverItem(this);
                }
            }
            return new Tuple<bool, Tuple<Ecrans, object>>(true, null);
        }

        public int Vitesse { get => vitesse; }
        public int Durabiliter { get => durabiliter; set => durabiliter = value; }
        public int MaxDurabiliter { get => maxDurabiliter; }
    }
}
