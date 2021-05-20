using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public abstract class Entite
    {
        protected float x;
        protected float y;
        protected float vie;

        protected Entite(float x, float y, int vie)
        {
            this.x = x;
            this.y = y;
            this.vie = vie;
        }

        /// <summary>
        /// la list des entites selon les ids possible
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Entite> Entites()
        {
            Dictionary<string, Entite> d = new Dictionary<string, Entite>();
            d[new Joueur(0, 0).id()] = new Joueur(0, 0);
            d[new Vache(0, 0).id()] = new Vache(0, 0);
            d[new Zombie(0, 0).id()] = new Zombie(0, 0);
            d[new Marchand(0, 0, null).id()] = new Marchand(0, 0, null);
            return d;
        }

        /// <summary>
        /// sauvegarde l'entite
        /// </summary>
        /// <param name="path">son dossier</param>
        public abstract void Sauvegarder(string path);

        /// <summary>
        /// charge l'entite
        /// </summary>
        /// <param name="path">son dossier</param>
        /// <returns></returns>
        public abstract Entite Charger(string path);

        public abstract string id();

        /// <summary>
        /// Faire bouger le personnage(déplacement) (maximum 1 block de distance)
        /// </summary>
        /// <returns>s'il est bloquer</returns>
        public virtual bool Bouger(float nx, int ny, Monde monde)
        {
            if (nx > 1) { nx = 1; }
            if (nx < -1) { nx = -1; }
            if (ny > 1) { ny = 1; }
            if (ny < -1) { ny = -1; }
            if (nx != 0 && ny != 0)
            {
                ny = 0;
            }

            Func<Block, bool, bool> pass = (iblock, byGrav) =>
            {
                return iblock == null || iblock.CanPassThrough(this, byGrav);
            };

            Block block = monde.GetBlock((int)(x + nx), (int)y + ny,true);
            if (pass(block, false))
            {
                int tomber = -3;
                x += nx;
                y += ny;
                while (pass(monde.GetBlock((int)x, (int)y - 1,true), true))
                {
                    y--;
                    tomber++;
                }
                if (tomber > 0)
                {
                    Vie -= tomber;
                }
            }
            else if (ny == 0)
            {
                block = monde.GetBlock((int)(x + nx), (int)y + 1,true);
                Block dessus = monde.GetBlock((int)x, (int)y + 1,true);
                if (pass(block, false) && pass(dessus, false))
                {
                    x += nx;
                    y++;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// force l'entite a etre a un endroit precis
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Tp(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// fait des chose selon un timer de 20 tick par seconde
        /// </summary>
        /// <param name="monde"></param>
        /// <returns>si besion d'actualiser l'ecran</returns>
        public virtual bool Comportement(Monde monde)
        {
            return false;
        }

        /// <summary>
        /// event clique droit
        /// </summary>
        /// <param name="joueur"></param>
        /// <returns>null ou le ChangerEcrans que tu doit faire</returns>
        public virtual Tuple<Ecrans, object> CliqueDroite(Joueur joueur)
        {
            return null;
        }

        public virtual bool Equals(Entite ent)
        {
            return ent != null && GetType() == ent.GetType() && x == ent.x && y == ent.y && vie == ent.vie;
        }

        /// <summary>
        /// attaque
        /// </summary>
        /// <param name="joueur"></param>
        /// <returns>return true si tu dois rafraichir l'image</returns>
        public virtual bool CliqueGauche(Entite ent, int dommage, Monde monde)
        {
            int dommageAFaire = dommage;
            if (ent != null && ent is Joueur joueur)
            {
                Item item = joueur.Barre.GetItem(joueur.Maindroite);
                if (item is Arme arme)
                {
                    dommageAFaire = arme.Degat;
                }
            }
            vie -= dommageAFaire;
            if (vie <= 0)
            {
                monde.Entites.Remove(this);
                return true;
            }
            return false;
        }

        public abstract Entite Clone();

        public float Vie
        {
            get => vie;
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
                vie = value;
            }
        }
        public float X { get => x; }
        public float Y { get => y; }
    }
}
