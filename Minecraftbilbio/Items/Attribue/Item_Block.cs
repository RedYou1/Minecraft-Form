using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// un item qui represente un block
    /// </summary>
    public abstract class Item_Block : Item
    {
        private Block block;

        protected Item_Block(Block block, int quantite) : base(quantite, 64)
        {
            this.block = block;
        }

        public override bool Equals(Item item)
        {
            return base.Equals(item) && item is Item_Block item_Block && block.Equals(item_Block.block);
        }

        /// <summary>
        /// place un block si ni a rien
        /// </summary>
        /// <param name="joueur"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="block"></param>
        /// <param name="entite"></param>
        /// <param name="monde"></param>
        /// <returns></returns>
        public override Tuple<bool, Tuple<Ecrans, object>> CliqueDroite(Joueur joueur, int bx, int by, Block block, Entite entite, Monde monde)
        {
            if (block == null && entite == null)
            {
                monde.SetBlock(bx, by, this.block.Clone());
                Item it = Clone();
                it.Quantite = 1;
                joueur.EnleverItem(it);
                return new Tuple<bool, Tuple<Ecrans, object>>(false, null);
            }

            return new Tuple<bool, Tuple<Ecrans, object>>(true, null);
        }
    }
}
