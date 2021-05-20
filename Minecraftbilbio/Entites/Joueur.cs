using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Minecraftbilbio
{
    /// <summary>
    /// Class Joueur qui herite de Entite
    /// </summary>
    public class Joueur : Entite
    {
        private Inventaire inventaire;
        private Inventaire barre;
        private float faim;
        private Casque casque;
        private Plastron plastron;
        private Jambiere jambiere;
        private Botte botte;
        private int maindroite;
        private Inventaire crafting;

        /// <summary>
        /// Constructeur de Joueur
        /// </summary>
        /// <param name="x">La position horizontale du joueur</param>
        /// <param name="y">La position verticale du joueur</param>
        public Joueur(float x, float y) : base(x, y, 20)
        {
            crafting = new Inventaire("CRAFT", 2, 2);
            barre = new Inventaire("BARRE", 1, 3);
            inventaire = new Inventaire("INVENTAIRE", 2, 3);
            faim = 20;
        }

        private Joueur(float x, float y, int vie, float faim, int maindroite, Inventaire inventaire, Inventaire barre, Inventaire crafting) : base(x, y, vie)
        {
            this.faim = faim;
            this.maindroite = maindroite;
            this.inventaire = inventaire;
            this.barre = barre;
            this.crafting = crafting;
        }

        /// <summary>
        /// Methode id
        /// </summary>
        /// <returns>l'id du joueur</returns>
        public override string id()
        {
            return "Joueur";
        }

        public override void Sauvegarder(string path)
        {
            File.WriteAllLines(path + "\\info.txt", new string[] { x + "/" + y + "/" + vie + "/" + faim + "/" + maindroite });
            inventaire.Sauvegarder(path);
            barre.Sauvegarder(path);
            crafting.Sauvegarder(path);

            string s = casque == null ? "null" : casque.id();
            Directory.CreateDirectory(path + "\\Casque." + s);
            if (casque != null)
            {
                casque.Sauvegarder(path + "\\Casque." + s);
            }

            s = plastron == null ? "null" : plastron.id();
            Directory.CreateDirectory(path + "\\Plastron." + s);
            if (plastron != null)
            {
                plastron.Sauvegarder(path + "\\Plastron." + s);
            }

            s = jambiere == null ? "null" : jambiere.id();
            Directory.CreateDirectory(path + "\\Jambiere." + s);
            if (jambiere != null)
            {
                jambiere.Sauvegarder(path + "\\Jambiere." + s);
            }

            s = botte == null ? "null" : botte.id();
            Directory.CreateDirectory(path + "\\Botte." + s);
            if (botte != null)
            {
                botte.Sauvegarder(path + "\\Botte." + s);
            }
        }

        public override Entite Charger(string path)
        {
            string name = File.ReadAllLines(path + "\\info.txt")[0];
            string[] coord = name.Split('/');
            Joueur j = new Joueur(float.Parse(coord[0]), float.Parse(coord[1]), int.Parse(coord[2]), float.Parse(coord[3]), int.Parse(coord[4])
                , new Inventaire(path + "\\INVENTAIRE.2.3"), new Inventaire(path + "\\BARRE.1.3"), new Inventaire(path + "\\CRAFT.2.2"));
            string[] folder = Directory.GetDirectories(path);
            if (!Directory.Exists(path + "\\Casque.null"))
            {
                foreach (string n in folder)
                {
                    string[] h = n.Split('\\');
                    string[] t = h[h.Length - 1].Split('.');
                    if (t[0] == "Casque")
                    {
                        j.casque = Item.Items()[t[1]].Charger(n) as Casque;
                        break;
                    }
                }
            }
            if (!Directory.Exists(path + "\\Plastron.null"))
            {
                foreach (string n in folder)
                {
                    string[] h = n.Split('\\');
                    string[] t = h[h.Length - 1].Split('.');
                    if (t[0] == "Plastron")
                    {
                        j.plastron = Item.Items()[t[1]].Charger(n) as Plastron;
                        break;
                    }
                }
            }
            if (!Directory.Exists(path + "\\Jambiere.null"))
            {
                foreach (string n in folder)
                {
                    string[] h = n.Split('\\');
                    string[] t = h[h.Length - 1].Split('.');
                    if (t[0] == "Jambiere")
                    {
                        j.jambiere = Item.Items()[t[1]].Charger(n) as Jambiere;
                        break;
                    }
                }
            }
            if (!Directory.Exists(path + "\\Botte.null"))
            {
                foreach (string n in folder)
                {
                    string[] h = n.Split('\\');
                    string[] t = h[h.Length - 1].Split('.');
                    if (t[0] == "Botte")
                    {
                        j.botte = Item.Items()[t[1]].Charger(n) as Botte;
                        break;
                    }
                }
            }
            return j;
        }

        public override bool Bouger(float nx, int ny, Monde monde)
        {
            bool a = base.Bouger(nx, ny, monde);
            if (!a)
            {
                Faim -= 0.05f;
            }
            return a;
        }

        /// <summary>
        /// Methode Equals
        /// </summary>
        /// <param name="joueur">Le joueur</param>
        /// <returns>Si c'est un autre qui lui ressemble</returns>
        public override bool Equals(Entite entite)
        {
            return base.Equals(entite) && entite is Joueur joueur && faim == joueur.faim && maindroite == joueur.maindroite
                && (((casque == null) == (joueur.casque == null)) || (casque != null && casque.Equals(joueur.casque)))
                && (((plastron == null) == (joueur.plastron == null)) || (plastron != null && plastron.Equals(joueur.plastron)))
                && (((jambiere == null) == (joueur.jambiere == null)) || (jambiere != null && jambiere.Equals(joueur.jambiere)))
                && (((botte == null) == (joueur.botte == null)) || (botte != null && botte.Equals(joueur.botte)))
                && inventaire.Equals(joueur.inventaire) && barre.Equals(joueur.barre) && crafting.Equals(joueur.crafting);
        }

        public override Entite Clone()
        {
            Joueur j = new Joueur(x, y);
            j.vie = vie;
            j.barre = barre.Clone();
            j.inventaire = inventaire.Clone();
            j.crafting = crafting.Clone();
            if (casque != null) { j.casque = (Casque)casque.Clone(); }
            if (plastron != null) { j.plastron = (Plastron)plastron.Clone(); }
            if (jambiere != null) { j.jambiere = (Jambiere)jambiere.Clone(); }
            if (botte != null) { j.botte = (Botte)botte.Clone(); }
            j.faim = faim;
            j.maindroite = maindroite;
            return j;
        }

        /// <summary>
        /// recupère l'item dans sa main droit
        /// </summary>
        /// <returns></returns>
        public Item MainDroit()
        {
            return barre.GetItem(maindroite);
        }

        public override bool CliqueGauche(Entite ent, int dommage, Monde monde)
        {
            if (casque != null)
            {
                casque.Durabiliter--;
                dommage -= casque.Resistance;
                if (casque.Durabiliter <= 0)
                {
                    casque = null;
                }
            }
            if (dommage <= 0)
            {
                return false;
            }
            if (plastron != null)
            {
                plastron.Durabiliter--;
                dommage -= plastron.Resistance;
                if (plastron.Durabiliter <= 0)
                {
                    plastron = null;
                }
            }
            if (dommage <= 0)
            {
                return false;
            }
            if (jambiere != null)
            {
                jambiere.Durabiliter--;
                dommage -= jambiere.Resistance;
                if (jambiere.Durabiliter <= 0)
                {
                    jambiere = null;
                }
            }
            if (dommage <= 0)
            {
                return false;
            }
            if (botte != null)
            {
                botte.Durabiliter--;
                dommage -= botte.Resistance;
                if (botte.Durabiliter <= 0)
                {
                    botte = null;
                }
            }
            if (dommage <= 0)
            {
                return false;
            }

            return base.CliqueGauche(ent, dommage, monde);
        }

        /// <summary>
        /// Methode AjouterItem
        /// </summary>
        /// <param name="item">Item ajouter</param>
        /// <returns>L'item ajouter</returns>
        public int AjouterItem(Item item)
        {
            int i = barre.AjouterItem(item);
            if (i > 0)
            {
                inventaire.AjouterItem(item);
            }
            return item.Quantite;
        }

        /// <summary>
        /// Methode EnleverItem
        /// </summary>
        /// <param name="item">Item enlever</param>
        /// <returns>L'item qui a été retirer</returns>
        public int EnleverItem(Item item)
        {
            if (barre.EnleverItem(item) > 0)
            {
                inventaire.EnleverItem(item);
            }
            return item.Quantite;
        }

        /// <summary>
        /// Methode ContientItem
        /// </summary>
        /// <param name="item">Item qui est contenu</param>
        /// <returns>L'item dans la barre ou dans l'inventaire</returns>
        public bool ContientItem(Item item)
        {
            return barre.ContientItem(item) || inventaire.ContientItem(item);
        }

        /// <summary>
        /// Get set de Faim
        /// </summary>
        public float Faim
        {
            get => faim;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                if (value > 20)
                {
                    value = 20;
                }
                faim = value;
            }
        }
        /// <summary>
        /// Get set de Maindroite
        /// </summary>
        public int Maindroite
        {
            get => maindroite;
            set
            {
                if (value >= 0 && value < barre.Length)
                {
                    maindroite = value;
                }
            }
        }

        /// <summary>
        /// Get de inventaire
        /// </summary>
        public Inventaire Inventaire { get => inventaire; }
        /// <summary>
        /// Get de barre
        /// </summary>
        public Inventaire Barre { get => barre; }
        /// <summary>
        /// Get de Crafting
        /// </summary>
        public Inventaire Crafting { get => crafting; }
        /// <summary>
        /// Get de casque
        /// </summary>
        public Casque Casque { get => casque; set => casque = value; }
        /// <summary>
        /// Get de plastron
        /// </summary>
        public Plastron Plastron { get => plastron; set => plastron = value; }
        /// <summary>
        /// Get de jambière
        /// </summary>
        public Jambiere Jambiere { get => jambiere; set => jambiere = value; }
        /// <summary>
        /// Get de botte
        /// </summary>
        public Botte Botte { get => botte; set => botte = value; }
    }
}
