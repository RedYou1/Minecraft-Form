using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe TableDeCraft qui hérite de Block
    /// </summary>
    public class TableDeCraft : Block
    {
        private Inventaire inventaire;
        /// <summary>
        /// Constructeur de TableDeCraft
        /// </summary>
        public TableDeCraft() : base(1, "TableDeCraft")
        {
            inventaire = new Inventaire("CRAFT", 3, 3);
        }

        /// <summary>
        /// Constructeur de TableDeCraft qui permet de créé une table de craft directement dans la table de craft
        /// </summary>
        /// <param name="inv">l'inventaire de la table de craft</param>
        private TableDeCraft(Inventaire inv) : base(1, "TableDeCraft")
        {
            inventaire = inv;
        }

        public override bool Equals(Block block)
        {
            return base.Equals(block) && block is TableDeCraft t && inventaire.Equals(t.inventaire);
        }

        public override Block Clone()
        {
            return new TableDeCraft(inventaire.Clone());
        }

        public override void Sauvegarder(string path)
        {
            inventaire.Sauvegarder(path);
        }

        public override Block Charger(string path)
        {
            return new TableDeCraft(new Inventaire(System.IO.Directory.GetDirectories(path)[0]));
        }

        public override Tuple<bool, Tuple<Ecrans, object>> CliqueDroit(Joueur joueur)
        {
            return new Tuple<bool, Tuple<Ecrans, object>>(true, new Tuple<Ecrans, object>(Ecrans.TableCraft, new Inventaire[] { inventaire, joueur.Inventaire, joueur.Barre }));
        }

        public override bool Detruire(Joueur joueur)
        {
            if (joueur != null)
            {
                joueur.AjouterItem(new TableDeCraft_Item(1));
                foreach (KeyValuePair<int, Item> item in inventaire.Items)
                {
                    joueur.AjouterItem(item.Value);
                }
            }
            return true;
        }

        /// <summary>
        /// regarde s'il a un craft possible dans l'inventaire (les 3x3 premier slot)
        /// </summary>
        /// <param name="inv"></param>
        /// <returns></returns>
        public static Craft CheckAll(Inventaire inv)
        {
            foreach (Craft craft in Craft.crafts)
            {
                if (Check(inv, craft))
                {
                    return craft;
                }
            }
            return null;
        }

        private static bool Check(Inventaire inv, Craft craft)
        {
            int longueur = inv.Longueur;
            int hauteur = inv.Hauteur;

            //max 3x3
            if (longueur > 3) { longueur = 3; }
            if (hauteur > 3) { hauteur = 3; }

            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    int slot = x + (y * inv.Longueur);

                    bool invnull = true;
                    if (x < longueur && y < hauteur)
                    {
                        invnull = inv.GetItem(slot) == null;
                    }
                    bool craftnull = craft.From[x, y] == null;
                    //les deux slot sont null
                    if (invnull && craftnull)
                    {
                        continue;
                    }
                    //seulement un des deux est null
                    if (invnull != craftnull)
                    {
                        return false;
                    }

                    if (x < longueur && y < hauteur)
                    {
                        //si pas meme item ou pas assez de cette item
                        if (craft.From[x, y].id() != inv.GetItem(slot).id()
                        || craft.From[x, y].Quantite > inv.GetItem(slot).Quantite)
                        {
                            return false;
                        }
                    }
                }
            return true;
        }

        /// <summary>
        /// effectue le craft dans l'inventaire</br>
        /// enleve les items du craft dans l'inventaire</br>
        /// ajoute l'item crafter dans l'inventaire du joueur</br>
        /// reset si plus de place
        /// </summary>
        /// <param name="inv"></param>
        /// <param name="joueur"></param>
        /// <returns></returns>
        public static bool CraftIt(Inventaire inv, Joueur joueur)
        {
            foreach (Craft craft in Craft.crafts)
            {
                if (Check(inv, craft))
                {
                    Item cl = craft.To.Clone();
                    int i = joueur.AjouterItem(cl);
                    if (i > 0)
                    {
                        Item cl2 = craft.To.Clone();
                        cl2.Quantite -= i;
                        joueur.EnleverItem(cl2);
                    }
                    else
                    {
                        int longueur = inv.Longueur;
                        int hauteur = inv.Hauteur;

                        //max 3x3
                        if (longueur > 3) { longueur = 3; }
                        if (hauteur > 3) { hauteur = 3; }

                        for (int x = 0; x < longueur; x++)
                            for (int y = 0; y < hauteur; y++)
                            {
                                int slot = x + (y * longueur);
                                Item it = inv.GetItem(slot);
                                if (it != null)
                                {
                                    it.Quantite -= craft.From[x, y].Quantite;
                                    if (it.Quantite == 0)
                                    {
                                        inv.SetItem(slot, null);
                                    }
                                }
                            }
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Get de inventaire
        /// </summary>
        public Inventaire Inventaire { get => inventaire; }
    }
}
