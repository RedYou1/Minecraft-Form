using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Armure qui est abstract et qui hérite de la classe Item et de l'interface Cassable
    /// </summary>
    public abstract class Armure : Item, Cassable
    {
        protected int resistance;
        protected int durabiliter;
        protected int maxDurabiliter;

        protected Armure(int resistance, int durabiliter, int maxDurabiliter) : base(1, 1)
        {
            this.resistance = resistance;
            this.durabiliter = durabiliter;
            this.maxDurabiliter = maxDurabiliter;
        }

        public override bool Equals(Item item)
        {
            return base.Equals(item) && item is Armure armure
                && resistance == armure.resistance && durabiliter == armure.durabiliter && maxDurabiliter == armure.maxDurabiliter;
        }

        public override Tuple<bool, Tuple<Ecrans, object>> CliqueDroite(Joueur joueur, int bx, int by, Block block, Entite entite, Monde monde)
        {
            Item it = joueur.MainDroit();
            if (it is Casque casque)
            {
                joueur.Barre.SetItem(joueur.Maindroite, joueur.Casque);
                joueur.Casque = casque;
            }
            if (it is Plastron plastron)
            {
                joueur.Barre.SetItem(joueur.Maindroite, joueur.Plastron);
                joueur.Plastron = plastron;
            }
            if (it is Jambiere jambiere)
            {
                joueur.Barre.SetItem(joueur.Maindroite, joueur.Jambiere);
                joueur.Jambiere = jambiere;
            }
            if (it is Botte Botte)
            {
                joueur.Barre.SetItem(joueur.Maindroite, joueur.Botte);
                joueur.Botte = Botte;
            }
            return new Tuple<bool, Tuple<Ecrans, object>>(false, null);
        }

        public int Resistance { get => resistance; }
        public int Durabiliter { get => durabiliter; set => durabiliter = value; }
        public int MaxDurabiliter { get => maxDurabiliter; }
    }
}
