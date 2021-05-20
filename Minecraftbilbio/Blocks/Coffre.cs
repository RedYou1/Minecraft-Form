using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Class Coffre_block qui hérite de Block
    /// </summary>
    public class Coffre_Block : Block
    {
        private Inventaire inventaire;

        /// <summary>
        /// Constructeur de Coffre_Block
        /// </summary>
        public Coffre_Block() : base(1, "Coffre")
        {
            inventaire = new Inventaire("COFFRE", 3, 2);
        }

        /// <summary>
        /// Constructeur Coffre_Block qui permet de créé un coffre directement avec ton inventaire 
        /// </summary>
        /// <param name="inv">L'inventaire de coffre(3,2)</param>
        private Coffre_Block(Inventaire inv) : base(1, "Coffre")
        {
            inventaire = inv;
        }

        public override bool Equals(Block block)
        {
            return base.Equals(block) && block is Coffre_Block coffre_Block && inventaire.Equals(coffre_Block.inventaire);
        }

        public override Tuple<bool, Tuple<Ecrans, object>> CliqueDroit(Joueur joueur)
        {
            return new Tuple<bool, Tuple<Ecrans, object>>(true, new Tuple<Ecrans, object>(Ecrans.Inventaire, new Inventaire[] { inventaire, joueur.Inventaire, joueur.Barre }));
        }


        public override bool Detruire(Joueur joueur)
        {
            foreach (KeyValuePair<int, Item> it in inventaire.Items)
            {
                joueur.AjouterItem(it.Value);
            }
            joueur.AjouterItem(new Coffre_Item(1));
            return true;
        }

        public override Block Clone()
        {
            Inventaire inv = inventaire.Clone();
            return new Coffre_Block(inv);
        }

        public override void Sauvegarder(string path)
        {
            inventaire.Sauvegarder(path);
        }

        public override Block Charger(string path)
        {
            return new Coffre_Block(new Inventaire(System.IO.Directory.GetDirectories(path)[0]));
        }

        /// <summary>
        /// Get de Inventaire
        /// </summary>
        public Inventaire Inventaire { get => inventaire; }
    }
}
