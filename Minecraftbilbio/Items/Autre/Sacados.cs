using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Sacados qui hérite de la classe Item
    /// </summary>
    public class Sacados : Item
    {
        private Inventaire inventaire;



        public Sacados() : base(1, 1)
        {
            inventaire = new Inventaire("SacADos", 2, 2);
        }

        private Sacados(Inventaire inv) : base(1, 1)
        {
            inventaire = inv;
        }

        public override bool Equals(Item item)
        {
            return base.Equals(item) && item is Sacados sacados && inventaire.Equals(sacados.inventaire);
        }

        public override Item Clone()
        {
            Sacados it = new Sacados();
            for (int x = 0; x < inventaire.Length; x++)
            {
                it.inventaire.SetItem(x, inventaire.GetItem(x));
            }
            return it;
        }

        public override Tuple<bool, Tuple<Ecrans, object>> CliqueDroite(Joueur joueur, int bx, int by, Block block, Entite entite, Monde monde)
        {
            //ouvre inventaire
            return new Tuple<bool, Tuple<Ecrans, object>>(false, new Tuple<Ecrans, object>(Ecrans.Inventaire, new Inventaire[] { inventaire, joueur.Inventaire, joueur.Barre }));
        }

        public override void Sauvegarder(string path)
        {
            inventaire.Sauvegarder(path);
        }

        public override Item Charger(string path)
        {
            return new Sacados(new Inventaire(System.IO.Directory.GetDirectories(path)[0]));
        }


        public override string id()
        {
            return "SacADos";
        }

        /// <summary>
        /// Get de Inventaire qui donne la donné à l'inventaire
        /// </summary>
        public Inventaire Inventaire { get => inventaire; }
    }
}
