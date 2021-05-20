using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public abstract class Block
    {
        private int durabiliter;
        protected string name;
        public Block(int durabiliter, string name)
        {
            this.durabiliter = durabiliter;
            this.name = name;
        }


        public virtual bool Equals(Block block)
        {
            return block != null && GetType() == block.GetType() && name == block.name && durabiliter == block.durabiliter;
        }

        /// <summary>
        /// si l'entite peut le traverser
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="byGravity"></param>
        /// <returns></returns>
        public virtual bool CanPassThrough(Entite ent, bool byGravity)
        {
            return false;
        }

        /// <summary>
        /// event clique droit
        /// </summary>
        /// <param name="joueur"></param>
        /// <returns>null ou le ChangerEcrans que tu doit faire</returns>
        public virtual Tuple<bool, Tuple<Ecrans, object>> CliqueDroit(Joueur joueur)
        {
            return null;
        }

        public abstract Block Clone();

        /// <summary>
        /// qu'est-ce que le block veux faire avant de ce faire mettre a null</br>
        /// ce mettre dans l'inventaire du joueur (par default)</br>
        /// </summary>
        /// <param name="joueur"></param>
        /// <returns>si tu peux le mettre a null</returns>
        public abstract bool Detruire(Joueur joueur);

        /// <summary>
        /// sauvegarde le block
        /// </summary>
        /// <param name="path">son dossier</param>
        public virtual void Sauvegarder(string path) { }

        /// <summary>
        /// charge le block
        /// </summary>
        /// <param name="path">son dossier</param>
        /// <returns></returns>
        public abstract Block Charger(string path);

        /// <summary>
        /// la list de block selon ses ids possible
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Block> Blocks()
        {
            Dictionary<string, Block> d = new Dictionary<string, Block>();
            d[new BedRock().Name] = new BedRock();
            d[new Block_Fer().Name] = new Block_Fer();
            d[new Coffre_Block().Name] = new Coffre_Block();
            d[new Echelle_Block().Name] = new Echelle_Block();
            d[new FeuilleDeChene_Block().Name] = new FeuilleDeChene_Block();
            d[new Four_Block().Name] = new Four_Block();
            d[new Herbe_Block().Name] = new Herbe_Block();
            d[new Minerais_Charbon().Name] = new Minerais_Charbon();
            d[new Minerais_Diamant().Name] = new Minerais_Diamant();
            d[new Minerais_Emeraude().Name] = new Minerais_Emeraude();
            d[new Minerais_Fer().Name] = new Minerais_Fer();
            d[new Minerais_Or().Name] = new Minerais_Or();
            d[new Pierre_Block().Name] = new Pierre_Block();
            d[new PlancheDeChene_Block().Name] = new PlancheDeChene_Block();
            d[new Porte_Block(false).Name] = new Porte_Block(false);
            d[new Porte_Block(true).Name] = new Porte_Block(true);
            d[new Roche_Block().Name] = new Roche_Block();
            d[new TableDeCraft().Name] = new TableDeCraft();
            d[new Terre_Block().Name] = new Terre_Block();
            d[new TroncDeChene_Block(false).Name] = new TroncDeChene_Block(false);
            d[new TroncDeChene_Block(true).Name] = new TroncDeChene_Block(true);
            return d;
        }

        public string Name { get => name; }

        public int Durabiliter { get => durabiliter; }
    }
}
