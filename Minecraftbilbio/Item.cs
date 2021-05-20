using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public abstract class Item : IEquatable<Item>
    {
        protected int quantite;
        private int maxQuantite;


        protected Item(int quantite, int maxQuantite)
        {
            this.quantite = quantite;
            this.maxQuantite = maxQuantite;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="joueur"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="block"></param>
        /// <param name="monde"></param>
        /// <returns>true si effectue le clique droit sur block / entite</returns>
        public virtual Tuple<bool, Tuple<Ecrans, object>> CliqueDroite(Joueur joueur, int bx, int by, Block block, Entite entite, Monde monde)
        {
            return new Tuple<bool, Tuple<Ecrans, object>>(true, null);
        }

        public abstract string id();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="joueur"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="block"></param>
        /// <param name="monde"></param>
        /// <returns>
        /// true si effectue le clique droit sur block / entite</br>
        /// null pour rester dans l'ecran</br>
        /// </returns>
        public virtual Tuple<bool, Tuple<Ecrans, object>> CliqueGauche(Joueur joueur, int bx, int by, Block block, Entite entite, Monde monde)
        {
            //Attaquer et creuser
            return new Tuple<bool, Tuple<Ecrans, object>>(true, null);
        }

        public abstract Item Clone();

        public virtual bool Equals(Item item)
        {
            return item != null && id() == item.id() && quantite == item.quantite;
        }

        /// <summary>
        /// sauvegarde l'item
        /// </summary>
        /// <param name="path">le dossier de l'item</param>
        public abstract void Sauvegarder(string path);

        /// <summary>
        /// charge l'item de la sauvegarde
        /// </summary>
        /// <param name="path">le dossier de l'item</param>
        /// <returns></returns>
        public abstract Item Charger(string path);

        public int Quantite { get => quantite; set => quantite = value; }
        public int MaxQuantite { get => maxQuantite; }

        /// <summary>
        /// la list d'item selon leur id possible
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Item> Items()
        {
            Dictionary<string, Item> d = new Dictionary<string, Item>();
            d[new Baton(1).id()] = new Baton(1);
            d[new Charbon(1).id()] = new Charbon(1);
            d[new Ciseau().id()] = new Ciseau();
            d[new Cuire(1).id()] = new Cuire(1);
            d[new Diamant(1).id()] = new Diamant(1);
            d[new Emeraude(1).id()] = new Emeraude(1);
            d[new Fer(1).id()] = new Fer(1);
            d[new Or(1).id()] = new Or(1);
            d[new Sacados().id()] = new Sacados();

            d[new Coffre_Item(1).id()] = new Coffre_Item(1);
            d[new Echelle_Item(1).id()] = new Echelle_Item(1);
            d[new Fer_Item_Block(1).id()] = new Fer_Item_Block(1);
            d[new FeuilleDeChene_Item(1).id()] = new FeuilleDeChene_Item(1);
            d[new Four_Item(1).id()] = new Four_Item(1);
            d[new Herbe_Item(1).id()] = new Herbe_Item(1);
            d[new Minerais_Fer_Item(1).id()] = new Minerais_Fer_Item(1);
            d[new Minerais_Or_Item(1).id()] = new Minerais_Or_Item(1);
            d[new Pierre_Item(1).id()] = new Pierre_Item(1);
            d[new PlancheDeChene(1).id()] = new PlancheDeChene(1);
            d[new Porte_Item(1).id()] = new Porte_Item(1);
            d[new Roche_Item(1).id()] = new Roche_Item(1);
            d[new TableDeCraft_Item(1).id()] = new TableDeCraft_Item(1);
            d[new Terre_Item(1).id()] = new Terre_Item(1);
            d[new TroncDeChene_Item(1).id()] = new TroncDeChene_Item(1);

            d[new BotteCuire().id()] = new BotteCuire();
            d[new BotteDiamant().id()] = new BotteDiamant();
            d[new BotteFer().id()] = new BotteFer();
            d[new BotteOr().id()] = new BotteOr();

            d[new CasqueCuire().id()] = new CasqueCuire();
            d[new CasqueDiamant().id()] = new CasqueDiamant();
            d[new CasqueFer().id()] = new CasqueFer();
            d[new CasqueOr().id()] = new CasqueOr();

            d[new JambiereCuire().id()] = new JambiereCuire();
            d[new JambiereDiamant().id()] = new JambiereDiamant();
            d[new JambiereFer().id()] = new JambiereFer();
            d[new JambiereOr().id()] = new JambiereOr();

            d[new PlastronCuire().id()] = new PlastronCuire();
            d[new PlastronDiamant().id()] = new PlastronDiamant();
            d[new PlastronFer().id()] = new PlastronFer();
            d[new PlastronOr().id()] = new PlastronOr();

            d[new EpeeBois().id()] = new EpeeBois();
            d[new EpeeDiamant().id()] = new EpeeDiamant();
            d[new EpeeFer().id()] = new EpeeFer();
            d[new EpeeOr().id()] = new EpeeOr();
            d[new EpeePierre().id()] = new EpeePierre();

            d[new PiocheBois().id()] = new PiocheBois();
            d[new PiocheDiamant().id()] = new PiocheDiamant();
            d[new PiocheFer().id()] = new PiocheFer();
            d[new PiocheOr().id()] = new PiocheOr();
            d[new PiochePierre().id()] = new PiochePierre();

            d[new Boeuf(1).id()] = new Boeuf(1);
            d[new Boeuf_Cuit(1).id()] = new Boeuf_Cuit(1);
            return d;
        }
    }
}
