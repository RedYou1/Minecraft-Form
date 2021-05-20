using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Nourriture qui est abstract et qui hérite de la classe Item
    /// </summary>
    public abstract class Nourriture : Item
    {
        private int faimRestoree;

        public Nourriture(int quantite, int maxQuantite, int faimRestoree) : base(quantite, maxQuantite)
        {
            this.faimRestoree = faimRestoree;
        }

        public override bool Equals(Item item)
        {
            return base.Equals(item) && item is Nourriture nourriture && faimRestoree == nourriture.faimRestoree;
        }

        /// <summary>
        /// mange</br>
        /// N'ENLEVE PAS L'ITEM DE L'INVENTAIRE
        /// </summary>
        /// <param name="joueur"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="block"></param>
        /// <param name="entite"></param>
        /// <param name="monde"></param>
        /// <returns>true si effectue le clique droit sur block / entite</returns>
        public override Tuple<bool, Tuple<Ecrans, object>> CliqueDroite(Joueur joueur, int bx, int by, Block block, Entite entite, Monde monde)
        {
            if (entite == null)
            {
                joueur.Faim += faimRestoree;
                quantite--;
                return new Tuple<bool, Tuple<Ecrans, object>>(false, null);
            }
            return new Tuple<bool, Tuple<Ecrans, object>>(true, null);
        }

        public int FaimRestoree { get => faimRestoree; }
    }
}
