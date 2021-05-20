using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Minecraftbilbio
{
    public class Inventaire
    {
        private Item[] items;
        private int longueur;
        private int hauteur;
        private string nom;

        /// <summary>
        /// Charge un inventaire depuis une sauvegarde
        /// </summary>
        /// <param name="inventoryFolder">le dossier de l"inventaire</param>
        public Inventaire(string inventoryFolder)
        {
            string[] s = inventoryFolder.Split('\\');
            string[] info = s[s.Length - 1].Split('.');
            nom = info[0];
            longueur = int.Parse(info[1]);
            hauteur = int.Parse(info[2]);
            items = new Item[longueur * hauteur];
            string[] itemsString = Directory.GetDirectories(inventoryFolder);
            foreach (string itemString in itemsString)
            {
                s = itemString.Split('\\');
                info = s[s.Length - 1].Split('.');
                items[int.Parse(info[0])] = Item.Items()[info[1]].Charger(itemString);
            }
        }

        /// <summary>
        /// sauvegarde un inventaire
        /// </summary>
        /// <param name="path">le dossier de l'inventaire</param>
        public void Sauvegarder(string path)
        {
            Directory.CreateDirectory(path + "\\" + nom + "." + longueur + "." + hauteur);
            for (int i = 0; i < longueur * hauteur; i++)
            {
                if (items[i] != null)
                {
                    Directory.CreateDirectory(path + "\\" + nom + "." + longueur + "." + hauteur + "\\" + i + "." + items[i].id());
                    items[i].Sauvegarder(path + "\\" + nom + "." + longueur + "." + hauteur + "\\" + i + "." + items[i].id());
                }
            }
        }

        public Inventaire(string nom, int longueur, int hauteur)
        {
            this.nom = nom;
            this.longueur = longueur;
            this.hauteur = hauteur;
            items = new Item[longueur * hauteur];
        }

        private Inventaire(string nom, int longueur, int hauteur, Item[] items)
        {
            this.nom = nom;
            this.longueur = longueur;
            this.hauteur = hauteur;
            this.items = items;
        }

        /// <summary>
        /// clone l'inventaire
        /// </summary>
        /// <returns></returns>
        public Inventaire Clone()
        {
            Item[] its = new Item[longueur * hauteur];
            for (int i = 0; i < Length; i++)
            {
                if (items[i] != null)
                {
                    its[i] = items[i].Clone();
                }
            }
            return new Inventaire(nom, longueur, hauteur, its);
        }

        public bool Equals(Inventaire inv)
        {
            if (inv == null)
            {
                return false;
            }
            if (longueur != inv.longueur || hauteur != inv.hauteur)
            {
                return false;
            }

            for (int i = 0; i < Length; i++)
            {
                if ((items[i] == null) != (inv.items[i] == null))
                {
                    return false;
                }
                if (items[i] == null && inv.items[i] == null)
                {
                    continue;
                }
                if (!items[i].Equals(inv.items[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// si contient l'item (pas l'instance) (si tu peux enlever le meme nombre de cette item)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ContientItem(Item item)
        {
            if (item != null)
            {
                int itr = item.Quantite;
                for (int i = 0; i < items.Length; i++)
                {
                    Item it = items[i];
                    if (it != null && it.id() == item.id())
                    {
                        itr -= item.Quantite;
                        if (itr <= 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// ajoute l'item dans l'inventaire (pas l'instance)
        /// </summary>
        /// <param name="item"></param>
        /// <returns>le nombre d'item restant</returns>
        public int AjouterItem(Item item)
        {
            if (item != null)
            {
                item = item.Clone();
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] == null)
                    {
                        items[i] = item.Clone();
                        item.Quantite = 0;
                        return 0;
                    }
                    if (items[i].id() == item.id() && items[i].Quantite != items[i].MaxQuantite)
                    {
                        int restant = items[i].MaxQuantite - items[i].Quantite;
                        if (restant >= item.Quantite)
                        {
                            items[i].Quantite += item.Quantite;
                            return 0;
                        }
                        else
                        {
                            items[i].Quantite = items[i].MaxQuantite;
                            item.Quantite -= restant;
                        }
                    }
                }
                return item.Quantite;
            }
            return 0;
        }

        /// <summary>
        /// enleve l'item dans l'inventaire (pas l'instance)
        /// </summary>
        /// <param name="item"></param>
        /// <returns>le nombre d'item qui n'a pas pus enlever</returns>
        public int EnleverItem(Item item)
        {
            if (item != null)
            {
                item = item.Clone();
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null && item.id() == items[i].id())
                    {
                        if (item.Quantite == items[i].Quantite)
                        {
                            items[i] = null;
                            return 0;
                        }
                        if (item.Quantite > items[i].Quantite)
                        {
                            item.Quantite -= items[i].Quantite;
                            items[i] = null;
                            continue;
                        }
                        if (item.Quantite < items[i].Quantite)
                        {
                            items[i].Quantite -= item.Quantite;
                            return 0;
                        }
                    }
                }
            }
            return item.Quantite;
        }

        /// <summary>
        /// recupere un item
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Item GetItem(int index)
        {
            return items[index];
        }

        /// <summary>
        /// place l'item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void SetItem(int index, Item item)
        {
            items[index] = item;
        }

        public string Nom { get => nom; }
        public int Longueur { get => longueur; }
        public int Hauteur { get => hauteur; }
        public int Length { get => items.Length; }

        /// <summary>
        /// la list d'item (pas null) (l'instance des item)
        /// </summary>
        public KeyValuePair<int, Item>[] Items
        {
            get
            {
                List<KeyValuePair<int, Item>> i = new List<KeyValuePair<int, Item>>();
                for (int x = 0; x < items.Length; x++)
                {
                    Item it = items[x];
                    if (it != null)
                    {
                        i.Add(new KeyValuePair<int, Item>(x, it));
                    }
                }
                return i.ToArray();
            }
        }
    }
}
