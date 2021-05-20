using Minecraftbilbio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Minecraftform
{
    public partial class Minecrafting : Form
    {
        public Minecrafting()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            int w = ClientSize.Width;
            int h = ClientSize.Height;
            if (Memoire.items != null)
            {
                foreach (UI_Item it in Memoire.items)
                {
                    if (it.Item != null)
                    {
                        Size itsize = new Size((int)((16f / 177f) * (w - (w / (32 * Sauvegarde.zoom_Block)) * 2))
                            , (int)((16f / 166f) * (h - (h / (32 * Sauvegarde.zoom_Block)) * 2)));
                        float rw = (float)itsize.Width / it.Width;
                        float rh = (float)itsize.Height / it.Height;
                        it.Image = new Bitmap(it.Image, itsize);
                        it.Size = itsize;
                        it.Location = new Point((int)(it.Location.X * rw), (int)(it.Location.Y * rh));
                    }
                }
                if (Memoire.selected != null && Memoire.selected.Item != null)
                {
                    Size itsize = new Size((int)((16f / 177f) * (w - (w / (32 * Sauvegarde.zoom_Block)) * 2))
                        , (int)((16f / 166f) * (h - (h / (32 * Sauvegarde.zoom_Block)) * 2)));
                    float rw = (float)itsize.Width / Memoire.selected.Width;
                    float rh = (float)itsize.Height / Memoire.selected.Height;
                    Memoire.selected.Image = new Bitmap(Memoire.selected.Image, itsize);

                    Memoire.selected.Size = itsize;
                    Memoire.selected.Location = new Point((int)(Memoire.selected.Location.X * rw)
                        , (int)(Memoire.selected.Location.Y * rh));
                }
            }
            Actualiser();
            if (EcranDeJeu.Ecran == Ecrans.Jeu)
            {
                EcranDeJeu.ActuHotBarre();
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                Memoire.shiftKeyDown = true;
            }
            if (e.KeyCode == Keys.ControlKey)
            {
                Memoire.ctrlKeyDown = true;
            }
            if (e.KeyCode == Keys.E)
            {
                if (EcranDeJeu.Ecran != Ecrans.Jeu)
                {
                    EcranDeJeu.SauvegarderInventaire();
                }
                else
                {
                    EcranDeJeu.ChangerEcran(Ecrans.Joueur, Sauvegarde.joueur);
                }
            }
            if (EcranDeJeu.Ecran == Ecrans.Jeu)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    Sauvegarde.SauvegarderMonde(Sauvegarde.monde, System.IO.Directory.GetCurrentDirectory() + "\\Mon_Monde");
                }
                if (e.KeyCode == Keys.D1)
                {
                    Sauvegarde.joueur.Maindroite = 0;
                }
                if (e.KeyCode == Keys.D2)
                {
                    Sauvegarde.joueur.Maindroite = 1;
                }
                if (e.KeyCode == Keys.D3)
                {
                    Sauvegarde.joueur.Maindroite = 2;
                }
                if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.D2 || e.KeyCode == Keys.D3)
                {
                    EcranDeJeu.ActuHotBarre();
                }
                if (e.KeyCode == Keys.A)
                {
                    BougerThread(-1, 0);
                    Actualiser();
                }
                if (e.KeyCode == Keys.D)
                {
                    BougerThread(1, 0);
                    Actualiser();
                }
                if (e.KeyCode == Keys.W)
                {
                    BougerThread(0, 1);
                    Actualiser();
                }
                if (e.KeyCode == Keys.S)
                {
                    BougerThread(0, -1);
                    Actualiser();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nx"></param>
        /// <returns> true si le joueur est bloquee</returns>
        private bool BougerThread(float nx, int ny)
        {
            //nx /= 1.25f;
            bool a = Sauvegarde.joueur.Bouger(nx, ny, Sauvegarde.monde);
            if (Sauvegarde.joueur.Vie <= 0)
            {
                Memoire.CreateWorld();
                return true;
            }
            return a;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ShiftKey:
                    Memoire.shiftKeyDown = false;
                    break;
                case Keys.ControlKey:
                    Memoire.ctrlKeyDown = false;
                    break;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Memoire.selected != null)
            {
                Memoire.selected.Location = new Point(e.X - Memoire.selected.Size.Width / 2, e.Y - Memoire.selected.Size.Height / 2);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (EcranDeJeu.Ecran == Ecrans.Jeu)
            {
                int w = (Memoire.form.ClientSize.Width / (16 * Sauvegarde.zoom_Block)) * 16 * Sauvegarde.zoom_Block;
                int h = (Memoire.form.ClientSize.Height / (16 * Sauvegarde.zoom_Block)) * 16 * Sauvegarde.zoom_Block;
                int x = (int)Math.Round(Sauvegarde.joueur.X - (w / 2f - e.X) / (16f * Sauvegarde.zoom_Block));
                int y = (int)Math.Round(Sauvegarde.joueur.Y + (h / 2f - e.Y) / (16f * Sauvegarde.zoom_Block));

                Block block = Sauvegarde.monde.GetBlock(x, y, false);

                if (block != null)
                {
                    if (!EcranDeJeu.CanSeeBlock(x, y, Sauvegarde.monde))
                    {
                        block = null;
                    }
                }

                Entite ent = Sauvegarde.monde.GetEntite(x, y);
                bool doBlock = true;

                Item item = Sauvegarde.joueur.Barre.GetItem(Sauvegarde.joueur.Maindroite);
                if (item != null)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        Tuple<bool, Tuple<Ecrans, object>> b = item.CliqueDroite(Sauvegarde.joueur, x, y, block, ((x == Sauvegarde.joueur.X && y == Sauvegarde.joueur.Y) ? Sauvegarde.joueur : ent), Sauvegarde.monde);
                        doBlock = b.Item1;
                        if (item.Quantite <= 0)
                        {
                            Sauvegarde.joueur.Barre.SetItem(Sauvegarde.joueur.Maindroite, null);
                        }
                        if (b.Item2 != null)
                        {
                            EcranDeJeu.ChangerEcran(b.Item2.Item1, b.Item2.Item2);
                        }
                        else
                        {
                            Actualiser();
                            EcranDeJeu.ActuHotBarre();
                        }
                    }
                    if (e.Button == MouseButtons.Left)
                    {
                        Tuple<bool, Tuple<Ecrans, object>> b = item.CliqueGauche(Sauvegarde.joueur, x, y, block, ((x == Sauvegarde.joueur.X && y == Sauvegarde.joueur.Y) ? Sauvegarde.joueur : ent), Sauvegarde.monde);
                        doBlock = b.Item1;
                        if (b.Item2 != null)
                        {
                            EcranDeJeu.ChangerEcran(b.Item2.Item1, b.Item2.Item2);
                        }
                        else
                        {
                            Actualiser();
                            EcranDeJeu.ActuHotBarre();
                        }
                    }
                }


                if (ent != null && doBlock)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        Tuple<Ecrans, object> temp = ent.CliqueDroite(Sauvegarde.joueur);
                        if (temp != null)
                        {
                            EcranDeJeu.ChangerEcran(temp.Item1, temp.Item2);
                            doBlock = false;
                        }
                    }
                    if (e.Button == MouseButtons.Left)
                    {
                        if (ent.CliqueGauche(Sauvegarde.joueur, 1, Sauvegarde.monde))
                        {
                            if (!Sauvegarde.monde.Entites.Contains(Sauvegarde.joueur))
                            {
                                Memoire.CreateWorld();
                                return;
                            }
                            Actualiser();
                            EcranDeJeu.ActuHotBarre();
                        }
                    }
                }

                if (block != null && doBlock)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (block.Detruire(Sauvegarde.joueur))
                        {
                            Sauvegarde.joueur.Faim -= 0.1f;
                            Sauvegarde.monde.SetBlock(x, y, null);
                            Entite[] ents = Sauvegarde.monde.Entites.ToArray();
                            foreach (Entite enti in ents)
                            {
                                int i = -3;
                                while (true)
                                {
                                    Block b = Sauvegarde.monde.GetBlock((int)enti.X, (int)enti.Y - 1, false);
                                    if (b == null || b.CanPassThrough(enti, true))
                                    {
                                        i++;
                                        enti.Bouger(0, -1, Sauvegarde.monde);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (i > 0)
                                {
                                    enti.Vie -= i;
                                    if (enti.Vie <= 0)
                                    {
                                        if (enti == Sauvegarde.joueur)
                                        {
                                            Memoire.CreateWorld();
                                            return;
                                        }
                                        Sauvegarde.monde.Entites.Remove(enti);
                                    }
                                }
                            }
                            Actualiser();
                            EcranDeJeu.ActuHotBarre();
                        }
                    }
                    if (e.Button == MouseButtons.Right)
                    {
                        Tuple<bool, Tuple<Ecrans, object>> invs = block.CliqueDroit(Sauvegarde.joueur);
                        if (invs != null)
                        {
                            if (invs.Item1)
                            {
                                if (invs.Item2 != null)
                                {
                                    EcranDeJeu.ChangerEcran(invs.Item2.Item1, invs.Item2.Item2);
                                }
                                Actualiser();
                                if (EcranDeJeu.Ecran == Ecrans.Jeu)
                                {
                                    EcranDeJeu.ActuHotBarre();
                                }
                            }
                        }
                    }
                }
            }
        }

        public Bitmap backGround;
        private bool aActualiser = false;

        public void Actualiser()
        {
            //EcranDeJeu.Afficher(Sauvegarde.joueur, Sauvegarde.monde);
            //BackgroundImage = backGround;
            aActualiser = true;
            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //20 tick second
            if (Sauvegarde.joueur.Faim == 0)
            {
                if (Sauvegarde.joueur.Vie == 0)
                {
                    Memoire.CreateWorld();
                    return;
                }
                else
                {
                    Sauvegarde.joueur.Vie -= 0.01f;
                }
            }
            else
            {
                Sauvegarde.joueur.Faim -= 0.01f;
            }

            bool actu = false;

            foreach (Entite ent in Sauvegarde.monde.Entites)
            {
                if (ent.Comportement(Sauvegarde.monde))
                {
                    actu = true;
                }
            }

            if (EcranDeJeu.Ecran == Ecrans.Jeu)
            {
                //des fois il faisait des erreur et crashait
                try
                {
                    EcranDeJeu.ActuHotBarre();
                }
                catch (Exception)
                {
                }
            }
            if (actu)
            {
                Actualiser();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (aActualiser)
            {
                EcranDeJeu.Afficher(Sauvegarde.joueur, Sauvegarde.monde);
                BackgroundImage = backGround;
                //e.Graphics.DrawImage(backGround, 0, 0);
                aActualiser = false;
            }
        }
    }
}

