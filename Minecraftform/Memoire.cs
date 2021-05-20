using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minecraftbilbio;

namespace Minecraftform
{
    public static class Memoire
    {
        /// <summary>
        /// le jeu est overt
        /// </summary>
        public static bool ouvert = true;
        public static Minecrafting form;

        /// <summary>
        /// la list d'item dans l'inventaire
        /// </summary>
        public static List<UI_Item> items;
        /// <summary>
        /// l'item selectionner dans l'inventaire
        /// </summary>
        public static UI_Item selected;

        public static System.Windows.Forms.PictureBox barre;
        public static System.Windows.Forms.PictureBox selector;
        public static System.Windows.Forms.PictureBox coeur;
        public static System.Windows.Forms.PictureBox nourriture;

        public static bool ctrlKeyDown = false;
        public static bool shiftKeyDown = false;


        /// <summary>
        /// Reset le monde et actualise l'ecran si possible
        /// </summary>
        public static void CreateWorld()
        {
            Sauvegarde.monde = new Monde(new GenerateurParDefault(new Noise()));
            Sauvegarde.monde.GenerateBlockChunk(0, 0);
            Sauvegarde.monde.GenerateBlockChunk(-1, 0);
            Sauvegarde.monde.GenerateBlockChunk(0, -1);
            Sauvegarde.monde.GenerateBlockChunk(-1, -1);
            Sauvegarde.joueur = new Joueur(1, Sauvegarde.monde.GetMaxHeight(1, false, true) + 1);

            Sauvegarde.monde.Entites.Add(Sauvegarde.joueur);

            Inventaire barre = Sauvegarde.joueur.Barre;
            barre.SetItem(2, new Boeuf_Cuit(10));

            Zombie zomb = new Zombie(4, Sauvegarde.monde.GetMaxHeight(4,false,false) + 1);
            Sauvegarde.monde.Entites.Add(zomb);

            if (form != null && EcranDeJeu.Ecran == Ecrans.Jeu)
            {
                form.Actualiser();
            }
        }

        /// <summary>
        /// Inventory coordinate to Screen coordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static System.Drawing.Point InvToScreen(int x, int y)
        {
            float rw = (Memoire.form.ClientSize.Width - (Memoire.form.ClientSize.Width / 32) * 2) / 176f;
            float rh = (Memoire.form.ClientSize.Height - (Memoire.form.ClientSize.Height / 32) * 2) / 166f;
            return new System.Drawing.Point((int)Math.Round((x * rw) + (Memoire.form.ClientSize.Width / 32f)), (int)Math.Round((y * rh) + (Memoire.form.ClientSize.Height / 32f)));
        }

        /// <summary>
        /// Screens coordinate to Inventory coordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static System.Drawing.Point ScreenToInv(int x, int y)
        {
            float rw = 176f / (Memoire.form.ClientSize.Width - (Memoire.form.ClientSize.Width / 32) * 2);
            float rh = 166f / (Memoire.form.ClientSize.Height - (Memoire.form.ClientSize.Height / 32) * 2);
            return new System.Drawing.Point((int)Math.Round((x - Memoire.form.ClientSize.Width / 32f) * rw), (int)Math.Round((y - Memoire.form.ClientSize.Height / 32f) * rh));
        }
    }
}
