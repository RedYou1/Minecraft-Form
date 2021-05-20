using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minecraftbilbio;
using System.Drawing;

namespace Minecraftform
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string optionPath = System.IO.Directory.GetCurrentDirectory() + "\\Option.txt";
            if (System.IO.File.Exists(optionPath))
            {
                Sauvegarde.ChargerOption(optionPath);
            }
            else
            {
                Sauvegarde.SauvegarderOption(optionPath);
            }

            Sauvegarde.ChargerMonde(System.IO.Directory.GetCurrentDirectory() + "\\Mon_Monde");

            if (Sauvegarde.monde == null)
            {
                Memoire.CreateWorld();
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Memoire.form = new Minecrafting();
            EcranDeJeu.ChangerEcran(Ecrans.Jeu, null);
            EcranDeJeu.Afficher(Sauvegarde.joueur, Sauvegarde.monde);
            Application.Run(Memoire.form);
        }
    }
}
