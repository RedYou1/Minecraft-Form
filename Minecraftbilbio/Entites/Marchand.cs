using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Class Marchand qui héritte de Entite
    /// </summary>
    public class Marchand : Entite
    {
        private Inventaire inventaire;
        private Echange[] echanges;

        /// <summary>
        /// genere un marchand avec des echange aleatoire
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Marchand(float x, float y) : base(x, y, 20)
        {
            inventaire = new Inventaire("MARCHAND", 9, 1);
            echanges = new Echange[5];
            Random r = new Random();
            Item[] items = Item.Items().Values.ToArray();
            for (int i = 0; i < 5; i++)
            {
                Item i1 = items[r.Next(0,items.Length)].Clone();
                i1.Quantite = r.Next(1,i1.MaxQuantite+1);
                Item i2 = null;
                if (r.NextDouble() > .5)
                {
                    i2 = items[r.Next(0, items.Length)].Clone();
                    i2.Quantite = r.Next(1, i2.MaxQuantite + 1);
                }
                Item i3 = items[r.Next(0, items.Length)].Clone();
                i3.Quantite = r.Next(1, i3.MaxQuantite + 1);

                echanges[i] = new Echange(i1,i2,i3);
            }
        }

        /// <summary>
        /// Constructeur de Marchand
        /// </summary>
        /// <param name="x">position horizontale du marchand</param>
        /// <param name="y">Position verticale de Marchand</param>
        /// <param name="echanges">echanges possible que le marchand peut faire</param>
        public Marchand(float x, float y, Echange[] echanges) : base(x, y, 20)
        {
            inventaire = new Inventaire("MARCHAND", 9, 1);
            if (echanges == null)
            {
                this.echanges = new Echange[0];
            }
            else
            {
                this.echanges = echanges;
            }
        }

        private Marchand(float x, float y, int vie, Echange[] echanges) : base(x, y, vie)
        {
            inventaire = new Inventaire("MARCHAND", 9, 1);
            if (echanges == null)
            {
                this.echanges = new Echange[0];
            }
            else
            {
                this.echanges = echanges;
            }
        }

        public override string id()
        {
            return "Marchand";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { x + "/" + y + "/" + vie });
            System.IO.Directory.CreateDirectory(path + "\\Echanges");
            for (int i = 0; i < echanges.Length; i++)
            {
                System.IO.Directory.CreateDirectory(path + "\\Echanges\\" + i);
                echanges[i].Sauvegarder(path + "\\Echanges\\" + i);
            }
        }

        public override Entite Charger(string path)
        {
            string name = System.IO.File.ReadAllLines(path + "\\info.txt")[0];
            string[] coord = name.Split('/');
            string[] echangesString = System.IO.Directory.GetDirectories(path + "\\Echanges");
            Echange[] echanges = new Echange[echangesString.Length];
            foreach (string echangeString in echangesString)
            {
                string[] p = echangeString.Split('\\');
                echanges[int.Parse(p[p.Length - 1])] = Echange.Charger(echangeString);
            }
            return new Marchand(float.Parse(coord[0]), float.Parse(coord[1]), int.Parse(coord[2]), echanges);
        }

        public override bool Equals(Entite entite)
        {
            if (!base.Equals(entite))
            {
                return false;
            }
            Marchand m = entite as Marchand;
            if (m == null)
            {
                return false;
            }
            if (echanges.Length != m.echanges.Length)
            {
                return false;
            }
            for (int i = 0; i < echanges.Length; i++)
            {
                if ((echanges[i] == null) != (m.echanges[i] == null))
                {
                    return false;
                }
                if (echanges[i] == null)
                {
                    continue;
                }
                if (!echanges[i].Equals(m.echanges[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override Entite Clone()
        {
            Marchand m = new Marchand(x, y, echanges);
            m.inventaire = m.inventaire.Clone();
            m.vie = vie;
            return m;
        }

        /// <summary>
        /// Vérifie si le joueur et le marchand a les items<br/>
        /// Les échanges si ils les ont
        /// </summary>
        /// <param name="joueur">Le joueur qui fait l'échange</param>
        /// <param name="index">L'index de l'échange</param>
        public bool Echanger(Joueur joueur, int index)
        {
            Echange echange = echanges[index];
            if (joueur.ContientItem(echange.ItemVoulu) && inventaire.ContientItem(echange.ItemDonne))
            {
                joueur.EnleverItem(echange.ItemVoulu);
                joueur.AjouterItem(echange.ItemDonne);
                inventaire.EnleverItem(echange.ItemDonne);
                inventaire.AjouterItem(echange.ItemVoulu);
                return true;
            }
            return false;
        }

        public override Tuple<Ecrans, object> CliqueDroite(Joueur joueur)
        {
            return new Tuple<Ecrans, object>(Ecrans.Marchand, this);
        }

        public Echange[] Echanges { get => echanges; }
        public Inventaire Inventaire { get => inventaire; }
    }
}
