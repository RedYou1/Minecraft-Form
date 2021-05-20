using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Epee qui est abstract et qui hérite de Arme et de Cassable
    /// </summary>
    public abstract class Epee : Arme, Cassable
    {
        protected int durabiliter;
        private int maxDurabiliter;
        public Epee(int degat, int durabiliter, int maxDurabiliter) : base(degat)
        {
            this.durabiliter = durabiliter;
            this.maxDurabiliter = maxDurabiliter;
        }

        public override bool Equals(Item item)
        {
            return base.Equals(item) && item is Epee epee && durabiliter == epee.durabiliter && maxDurabiliter == epee.maxDurabiliter;
        }

        public override Tuple<bool, Tuple<Ecrans, object>> CliqueDroite(Joueur joueur, int bx, int by, Block block, Entite entite, Monde monde)
        {
            //TODO : systeme de paré(se proteger) 
            return new Tuple<bool, Tuple<Ecrans, object>>(true, null);
        }

        public override Tuple<bool, Tuple<Ecrans, object>> CliqueGauche(Joueur joueur, int bx, int by, Block block, Entite entite, Monde monde)
        {
            //dans le system de degat. les degats de larme son deja pris en compte
            if (entite != null)
            {
                durabiliter--;
                if (durabiliter == 0)
                {
                    joueur.EnleverItem(this);
                }
            }
            return new Tuple<bool, Tuple<Ecrans, object>>(true, null);
        }

        public int Durabiliter { get => durabiliter; set => durabiliter = value; }
        public int MaxDurabiliter { get => maxDurabiliter; }
    }
}
