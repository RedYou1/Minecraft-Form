using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Minecraftbilbio
{
    public class Echange
    {
        private Item itemVoulu;
        private Item itemVoulu2;
        private Item itemDonne;

        public Echange(Item itemVoulu, Item itemDonne) : this(itemVoulu, null, itemDonne)
        { }

        public Echange(Item itemVoulu, Item itemVoulu2, Item itemDonne)
        {
            if (itemVoulu == null && itemVoulu2 != null)
            {
                itemVoulu = itemVoulu2;
                itemVoulu2 = null;
            }

            this.itemVoulu = itemVoulu;
            this.itemVoulu2 = itemVoulu2;
            this.itemDonne = itemDonne;
        }

        /// <summary>
        /// sauvegarde l'echange
        /// </summary>
        /// <param name="path">son dossier</param>
        public void Sauvegarder(string path)
        {
            string v1 = itemVoulu == null ? "null" : itemVoulu.id();
            string v2 = itemVoulu2 == null ? "null" : itemVoulu2.id();
            string d = itemDonne == null ? "null" : itemDonne.id();
            Directory.CreateDirectory(path + "\\itemVoulu." + v1);
            Directory.CreateDirectory(path + "\\itemVoulu2." + v2);
            Directory.CreateDirectory(path + "\\itemDonne." + d);
            if (itemVoulu != null)
            {
                ItemVoulu.Sauvegarder(path + "\\itemVoulu." + v1);
            }
            if (itemVoulu2 != null)
            {
                itemVoulu2.Sauvegarder(path + "\\itemVoulu2." + v2);
            }
            if (itemDonne != null)
            {
                itemDonne.Sauvegarder(path + "\\itemDonne." + d);
            }
        }

        /// <summary>
        /// charge l'echande
        /// </summary>
        /// <param name="path">son dossier</param>
        /// <returns></returns>
        public static Echange Charger(string path)
        {
            Echange echange = new Echange(null, null, null);
            if (!Directory.Exists(path + "\\itemVoulu.null"))
            {
                string[] s = Directory.GetDirectories(path);
                foreach (string h in s)
                {
                    if (h.StartsWith(path + "\\itemVoulu."))
                    {
                        string[] j = h.Split('\\');
                        string t = j[j.Length - 1].Split('.')[1];
                        echange.itemVoulu = Item.Items()[t].Charger(h);
                        break;
                    }
                }
            }
            if (!Directory.Exists(path + "\\itemVoulu2.null"))
            {
                string[] s = Directory.GetDirectories(path);
                foreach (string h in s)
                {
                    if (h.StartsWith(path + "\\itemVoulu2."))
                    {
                        string[] j = h.Split('\\');
                        string t = j[j.Length - 1].Split('.')[1];
                        echange.itemVoulu2 = Item.Items()[t].Charger(h);
                        break;
                    }
                }
            }
            if (!Directory.Exists(path + "\\itemDonne.null"))
            {
                string[] s = Directory.GetDirectories(path);
                foreach (string h in s)
                {
                    if (h.StartsWith(path + "\\itemDonne."))
                    {
                        string[] j = h.Split('\\');
                        string t = j[j.Length - 1].Split('.')[1];
                        echange.itemDonne = Item.Items()[t].Charger(h);
                        break;
                    }
                }
            }
            return echange;
        }

        public bool Equals(Echange echange)
        {
            if (echange == null)
            {
                return false;
            }

            return (((itemVoulu == null) == (echange.itemVoulu == null)) || (itemVoulu != null && itemVoulu.Equals(echange.itemVoulu)))
                && (((itemVoulu2 == null) == (echange.itemVoulu2 == null)) || (itemVoulu2 != null && itemVoulu2.Equals(echange.itemVoulu2)))
                && (((itemDonne == null) == (echange.itemDonne == null)) || (itemDonne != null && itemDonne.Equals(echange.itemDonne)));
        }

        public Echange Clone()
        {
            return new Echange(ItemVoulu, ItemVoulu2, ItemDonne);
        }

        public Item ItemVoulu { get => itemVoulu; }
        public Item ItemVoulu2 { get => itemVoulu2; }
        public Item ItemDonne { get => itemDonne; }
    }
}
