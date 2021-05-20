using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe Four_Block qui hérite de Block
    /// </summary>
    public class Four_Block : Block
    {
        private Inventaire inventaire;

        private int carburant;

        /// <summary>
        /// Constructeur de Four_Block
        /// </summary>
        public Four_Block() : base(1, "Four")
        {
            carburant = 0;
            inventaire = new Inventaire("FOUR", 1, 3);
        }

        private Four_Block(Inventaire inv, int carburant) : base(1, "Four")
        {
            this.carburant = carburant;
            inventaire = inv;
        }

        public override bool Detruire(Joueur joueur)
        {
            if (joueur.MainDroit() is Pioche)
            {
                joueur.AjouterItem(new Four_Item(1));
                if (inventaire.GetItem(0) != null) { joueur.AjouterItem(inventaire.GetItem(0)); }
                if (inventaire.GetItem(1) != null) { joueur.AjouterItem(inventaire.GetItem(1)); }
                if (inventaire.GetItem(2) != null) { joueur.AjouterItem(inventaire.GetItem(2)); }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Met a jour ce qui est dans le four</br>
        /// fait cuire les items
        /// </summary>
        public void Update()
        {
            //dessus
            Item it1 = inventaire.GetItem(0);
            //dessous
            Item it2 = inventaire.GetItem(1);
            //resultat
            Item it3 = inventaire.GetItem(2);

            //s'il a des item a cuire
            if (it1 != null)
            {
                if (it1 is Cuisable cuit)
                {
                    //si le four peut sortir les items
                    if (it3 == null
                        || (it3.id() == cuit.CuitEn().id()
                            && it3.Quantite + cuit.CuitEn().Quantite <= it3.MaxQuantite))
                    {
                        while (true)
                        {
                            //s'il ne rest plus d'item a cuire
                            if (it1.Quantite == 0)
                            {
                                inventaire.SetItem(0, null);
                                break;
                            }
                            if (it1 is Cuisable cui)
                            {
                                //s'il reste du carburant
                                if (cui.TempsDeCuisson() <= carburant)
                                {
                                    it1.Quantite--;
                                    carburant -= cui.TempsDeCuisson();
                                    //l'ajoute
                                    if (it3 == null)
                                    {
                                        inventaire.SetItem(2, cui.CuitEn().Clone());
                                        it3 = inventaire.GetItem(2);
                                    }
                                    else
                                    {
                                        it3.Quantite += cui.CuitEn().Quantite;
                                        if (it3.Quantite + cui.CuitEn().Quantite > it3.MaxQuantite)
                                        {
                                            break;
                                        }
                                    }
                                    continue;
                                }
                                else
                                {
                                    if (it2 == null)
                                    {
                                        //s'arrete s'il ne reste plus de carburant meme dans le slot des carburant
                                        break;
                                    }
                                    if (it2 is Brulable bru)
                                    {
                                        carburant += bru.ProduitTemperature();
                                        it2.Quantite--;
                                        if (it2.Quantite == 0)
                                        {
                                            inventaire.SetItem(1, null);
                                            it2 = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public override Tuple<bool, Tuple<Ecrans, object>> CliqueDroit(Joueur joueur)
        {
            return new Tuple<bool, Tuple<Ecrans, object>>(true, new Tuple<Ecrans, object>(Ecrans.Four, new KeyValuePair<Four_Block, Inventaire[]>(this, new Inventaire[] { inventaire, joueur.Inventaire, joueur.Barre })));
        }

        public override bool Equals(Block block)
        {
            return base.Equals(block) && block is Four_Block four && inventaire.Equals(four.inventaire) && carburant == four.carburant;
        }

        public override Block Clone()
        {
            Four_Block b = new Four_Block();
            b.inventaire = inventaire;
            b.carburant = carburant;
            return b;
        }

        public override void Sauvegarder(string path)
        {
            inventaire.Sauvegarder(path);
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Carburant:" + carburant });
        }

        public override Block Charger(string path)
        {
            int carburant = int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]);
            return new Four_Block(new Inventaire(System.IO.Directory.GetDirectories(path)[0]), carburant);
        }

        public Inventaire Inventaire { get => inventaire; }
        public int Carburant { get => carburant; }
    }
}
