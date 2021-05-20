using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minecraftbilbio;
using System.Windows.Forms;
using System.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Threading;

namespace Minecraftform
{
    public static class EcranDeJeu
    {
        private static Ecrans ecran = Ecrans.Inventaire;

        public static Inventaire[] inventaires;
        public static Marchand marchand;
        public static Four_Block four;

        /// <summary>
        /// Actualise le fond d'ecran du form (la map)
        /// </summary>
        /// <param name="joueur"></param>
        /// <param name="monde"></param>
        public static void Afficher(Joueur joueur, Monde monde)
        {
            switch (ecran)
            {
                case Ecrans.Jeu:
                    AfficherMap(joueur, monde);
                    break;
                case Ecrans.Inventaire:
                    AfficherInventaire();
                    break;
                case Ecrans.Marchand:
                    AfficherMarchand();
                    break;
                case Ecrans.TableCraft:
                    AfficherCraft();
                    break;
                case Ecrans.Four:
                    AfficherFour();
                    break;
                case Ecrans.Joueur:
                    AfficherJoueur();
                    break;
            }
        }

        /// <summary>
        /// invenatire de four
        /// </summary>
        public static void AfficherFour()
        {
            AfficherMap(Sauvegarde.joueur, Sauvegarde.monde);
            Graphics gfx = Graphics.FromImage(Memoire.form.backGround);

            int w = Memoire.form.ClientSize.Width;
            int h = Memoire.form.ClientSize.Height;
            gfx.DrawImage(Properties.Resources.Inventaire_Four
                , w / 32, h / 32, w - (w / 32) * 2, h - (h / 32) * 2);
        }

        /// <summary>
        /// inventaire de table de craft
        /// </summary>
        public static void AfficherCraft()
        {
            AfficherMap(Sauvegarde.joueur, Sauvegarde.monde);
            Graphics gfx = Graphics.FromImage(Memoire.form.backGround);

            int w = Memoire.form.ClientSize.Width;
            int h = Memoire.form.ClientSize.Height;
            gfx.DrawImage(Properties.Resources.Inventaire_TableDeCraft
                , w / 32, h / 32, w - (w / 32) * 2, h - (h / 32) * 2);
        }

        /// <summary>
        /// liste d'echange d'un marchand
        /// </summary>
        public static void AfficherMarchand()
        {
            AfficherMap(Sauvegarde.joueur, Sauvegarde.monde);
            Graphics gfx = Graphics.FromImage(Memoire.form.backGround);

            int w = Memoire.form.ClientSize.Width;
            int h = Memoire.form.ClientSize.Height;
            gfx.DrawImage(Properties.Resources.Inventaire_Marchand
                , w / 32, h / 32, w - (w / 32) * 2, h - (h / 32) * 2);
            Size itsize = new Size((int)((16f / 177f) * (w - (w / 32) * 2)), (int)((16f / 166f) * (h - (h / 32) * 2)));

            for (int i = 0; i < 5; i++)
            {
                if (i >= marchand.Echanges.Length) { break; }

                Echange echange = marchand.Echanges[i];
                if (echange.ItemVoulu != null)
                {
                    Bitmap img = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject("Item_" + echange.ItemVoulu.id()),
                        itsize);
                    Graphics g = Graphics.FromImage(img);
                    SizeF s = g.MeasureString(echange.ItemVoulu.Quantite + "", SystemFonts.DefaultFont);
                    if (echange.ItemVoulu.Quantite > 1)
                    {
                        g.DrawString(echange.ItemVoulu.Quantite + "", SystemFonts.DefaultFont, Brushes.Black, new PointF(itsize.Width - s.Width, itsize.Height - s.Height));
                    }
                    Point start = Memoire.InvToScreen(84, 47 + i * 22);
                    gfx.DrawImage(img, start.X, start.Y, itsize.Width, itsize.Height);
                }
                if (echange.ItemVoulu2 != null)
                {
                    Bitmap img = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject("Item_" + echange.ItemVoulu2.id()),
                        itsize);
                    Graphics g = Graphics.FromImage(img);
                    SizeF s = g.MeasureString(echange.ItemVoulu2.Quantite + "", SystemFonts.DefaultFont);
                    if (echange.ItemVoulu2.Quantite > 1)
                    {
                        g.DrawString(echange.ItemVoulu2.Quantite + "", SystemFonts.DefaultFont, Brushes.Black, new PointF(itsize.Width - s.Width, itsize.Height - s.Height));
                    }
                    Point start = Memoire.InvToScreen(102, 47 + i * 22);
                    gfx.DrawImage(img, start.X, start.Y, itsize.Width, itsize.Height);
                }
                if (echange.ItemDonne != null)
                {
                    Bitmap img = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject("Item_" + echange.ItemDonne.id()),
                        itsize);
                    Graphics g = Graphics.FromImage(img);
                    SizeF s = g.MeasureString(echange.ItemDonne.Quantite + "", SystemFonts.DefaultFont);
                    if (echange.ItemDonne.Quantite > 1)
                    {
                        g.DrawString(echange.ItemDonne.Quantite + "", SystemFonts.DefaultFont, Brushes.Black, new PointF(itsize.Width - s.Width, itsize.Height - s.Height));
                    }
                    Point start = Memoire.InvToScreen(140, 47 + i * 22);
                    gfx.DrawImage(img, start.X, start.Y, itsize.Width, itsize.Height);
                }
            }
        }

        /// <summary>
        /// inventaire de coffre ou autre
        /// </summary>
        public static void AfficherInventaire()
        {
            AfficherMap(Sauvegarde.joueur, Sauvegarde.monde);
            Graphics gfx = Graphics.FromImage(Memoire.form.backGround);

            int w = Memoire.form.ClientSize.Width;
            int h = Memoire.form.ClientSize.Height;

            Bitmap b = Properties.Resources.Inventaire_Inventaire;
            Graphics g = Graphics.FromImage(b);

            Inventaire inv = inventaires[0];
            for (int x = 0; x < inv.Longueur; x++)
                for (int y = 0; y < inv.Hauteur; y++)
                {
                    g.DrawImage(Properties.Resources.Inventaire_Inventaire_Emplacement, new Point(7 + x * 18, 53 - y * 18));
                }

            gfx.DrawImage(b
                , w / 32, h / 32, w - (w / 32) * 2, h - (h / 32) * 2);
        }

        /// <summary>
        /// inventaire d'un joueur
        /// </summary>
        public static void AfficherJoueur()
        {
            AfficherMap(Sauvegarde.joueur, Sauvegarde.monde);
            Graphics gfx = Graphics.FromImage(Memoire.form.backGround);

            int w = Memoire.form.ClientSize.Width;
            int h = Memoire.form.ClientSize.Height;
            gfx.DrawImage(Properties.Resources.Inventaire_Joueur
                , w / 32, h / 32, w - (w / 32) * 2, h - (h / 32) * 2);
        }

        /// <summary>
        /// si le block a de l'air autoure de lui
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="monde"></param>
        /// <returns></returns>
        public static bool CanSeeBlock(int x, int y, Monde monde)
        {
            Joueur j = new Joueur(x, y);
            Func<Block, bool> seeme = (block) =>
            {
                return block == null || block.CanPassThrough(j, false);
            };
            return seeme(monde.GetBlock(x - 1, y,false))
                  || seeme(monde.GetBlock(x + 1, y,false))
                  || seeme(monde.GetBlock(x, y - 1,false))
                  || seeme(monde.GetBlock(x, y + 1,false))
                  || seeme(monde.GetBlock(x - 1, y - 1,false))
                  || seeme(monde.GetBlock(x + 1, y - 1,false))
                  || seeme(monde.GetBlock(x - 1, y + 1,false))
                  || seeme(monde.GetBlock(x + 1, y + 1,false));
        }


        public static void AfficherMap(Joueur joueur, Monde monde)
        {
            if (Memoire.form != null)
            {
                int blocksize = 16 * Sauvegarde.zoom_Block;
                int w = (Memoire.form.ClientSize.Width / blocksize) * blocksize;
                int h = (Memoire.form.ClientSize.Height / blocksize) * blocksize;
                int px = (int)Math.Round(joueur.X);
                int py = (int)Math.Round(joueur.Y);
                Bitmap image = new Bitmap(w, h);
                Graphics gfx = Graphics.FromImage(image);
                //gfx.FillRectangle(new SolidBrush(Color.FromArgb(135, 206, 235))
                //    , new Rectangle(0, 0, Memoire.form.ClientSize.Width, Memoire.form.ClientSize.Height));
                int halfScreenW = w / (2 * blocksize);
                int halfScreenH = h / (2 * blocksize);

                for (int x = -halfScreenW; x <= halfScreenW; x++)
                    for (int y = -halfScreenH; y <= halfScreenH; y++)
                    {
                        int bx = px + x;
                        int by = py - y;
                        if (Sauvegarde.piece)
                        {
                            Block b = Sauvegarde.monde.GetBackBlock(bx, by);
                            if (b != null)
                            {
                                int tx = x * blocksize + w / 2 - (blocksize / 2);
                                int ty = y * blocksize + h / 2 - (blocksize / 4);
                                gfx.DrawImage((Bitmap)Properties.Resources.ResourceManager.GetObject("Block_" + b.Name),
                                        tx, ty
                                        , blocksize, blocksize);
                                gfx.FillRectangle(new SolidBrush(Color.FromArgb(100, 0, 0, 0)),
                                        tx, ty
                                        , blocksize, blocksize);
                            }
                        }
                        Block block = monde.GetBlock(bx, by, true);
                        if (block != null)
                        {
                            if (CanSeeBlock(bx, by, monde))
                            {
                                gfx.DrawImage((Bitmap)Properties.Resources.ResourceManager.GetObject("Block_" + block.Name),
                                    x * blocksize + w / 2 - (blocksize / 2), y * blocksize + h / 2 - (blocksize / 4)
                                    , blocksize, blocksize);
                            }
                            else
                            {
                                gfx.FillRectangle(Brushes.Black,
                                    x * blocksize + w / 2 - (blocksize / 2), y * blocksize + h / 2 - (blocksize / 4)
                                    , blocksize, blocksize);
                            }
                        }
                    }
                Entite[] ents = Sauvegarde.monde.Entites.ToArray();
                foreach (Entite ent in ents)
                {
                    gfx.DrawImage((Bitmap)Properties.Resources.ResourceManager.GetObject("Entite_" + ent.id()),
                        (ent.X - px) * blocksize + w / 2 - (blocksize / 2)
                        , (ent.Y - py) * -blocksize + h / 2 - (blocksize / 4)
                        , blocksize, blocksize);
                }

                Memoire.form.backGround = image;
            }
        }

        /// <summary>
        /// actualise la hotbarre
        /// </summary>
        public static void ActuHotBarre()
        {
            if (Memoire.form.Controls.Contains(Memoire.barre)) { Memoire.form.Controls.Remove(Memoire.barre); }
            if (Memoire.form.Controls.Contains(Memoire.selector)) { Memoire.form.Controls.Remove(Memoire.selector); }
            if (Memoire.form.Controls.Contains(Memoire.coeur)) { Memoire.form.Controls.Remove(Memoire.coeur); }
            if (Memoire.form.Controls.Contains(Memoire.nourriture)) { Memoire.form.Controls.Remove(Memoire.nourriture); }

            Memoire.selector = new PictureBox();
            Memoire.selector.BackColor = Color.Transparent;
            Memoire.selector.Image = new Bitmap(Properties.Resources.GUI_Selector, 24 * Sauvegarde.zoom_HotBarre, 24 * Sauvegarde.zoom_HotBarre);
            Memoire.selector.Size = new Size(24 * Sauvegarde.zoom_HotBarre, 24 * Sauvegarde.zoom_HotBarre);
            Memoire.selector.Location = new Point(
                Memoire.form.ClientSize.Width / 2 - 32 * Sauvegarde.zoom_HotBarre + Sauvegarde.joueur.Maindroite * 20 * Sauvegarde.zoom_HotBarre
                , Memoire.form.ClientSize.Height - 24 * Sauvegarde.zoom_HotBarre);
            {
                Item item = Sauvegarde.joueur.Barre.GetItem(Sauvegarde.joueur.Maindroite);
                if (item != null)
                {
                    Graphics g = Graphics.FromImage(Memoire.selector.Image);
                    Bitmap timg = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject("Item_" + item.id()),
                                new Size(16 * Sauvegarde.zoom_HotBarre, 16 * Sauvegarde.zoom_HotBarre));
                    Graphics git = Graphics.FromImage(timg);
                    SizeF s = git.MeasureString(item.Quantite + "", SystemFonts.DefaultFont);
                    if (item is Cassable cas)
                    {
                        float pour = cas.Durabiliter / (float)cas.MaxDurabiliter;
                        git.FillRectangle(new SolidBrush(Color.FromArgb(
                            255 - (int)(255 * pour), (int)(255 * pour), 0
                            ))
                            , new Rectangle(0, 16 * Sauvegarde.zoom_HotBarre / 2 + 16 * Sauvegarde.zoom_HotBarre / 4
                                , (int)(16 * Sauvegarde.zoom_HotBarre * pour), 16 * Sauvegarde.zoom_HotBarre / 4));
                    }
                    if (item.Quantite > 1)
                    {
                        git.DrawString(item.Quantite + "", SystemFonts.DefaultFont, System.Drawing.Brushes.Black
                            , new PointF(16 * Sauvegarde.zoom_HotBarre - s.Width, 16 * Sauvegarde.zoom_HotBarre - s.Height));
                    }
                    g.DrawImage(timg, new Point(3 * Sauvegarde.zoom_HotBarre, 3 * Sauvegarde.zoom_HotBarre));
                }
            }

            Memoire.barre = new PictureBox();
            Memoire.barre.BackColor = Color.Transparent;
            Memoire.barre.Size = new Size(62 * Sauvegarde.zoom_HotBarre, 22 * Sauvegarde.zoom_HotBarre);
            Memoire.barre.Location = new Point(
                Memoire.form.ClientSize.Width / 2 - 31 * Sauvegarde.zoom_HotBarre
                , Memoire.form.ClientSize.Height - 23 * Sauvegarde.zoom_HotBarre);
            Memoire.barre.Image = new Bitmap(Properties.Resources.GUI_HotBarre, 62 * Sauvegarde.zoom_HotBarre, 22 * Sauvegarde.zoom_HotBarre);
            {
                Graphics g = Graphics.FromImage(Memoire.barre.Image);

                for (int i = 0; i < 3; i++)
                {
                    Item item = Sauvegarde.joueur.Barre.GetItem(i);
                    if (item != null)
                    {
                        Bitmap timg = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject("Item_" + item.id()),
                            new Size(16 * Sauvegarde.zoom_HotBarre, 16 * Sauvegarde.zoom_HotBarre));
                        Graphics git = Graphics.FromImage(timg);
                        SizeF s = git.MeasureString(item.Quantite + "", SystemFonts.DefaultFont);
                        if (item is Cassable cas)
                        {
                            float pour = cas.Durabiliter / (float)cas.MaxDurabiliter;
                            git.FillRectangle(new SolidBrush(Color.FromArgb(
                                255 - (int)(255 * pour), (int)(255 * pour), 0
                                ))
                                , new Rectangle(0, 16 * Sauvegarde.zoom_HotBarre / 2 + 16 * Sauvegarde.zoom_HotBarre / 4
                                    , (int)(16 * Sauvegarde.zoom_HotBarre * pour), 16 * Sauvegarde.zoom_HotBarre / 4));
                        }
                        if (item.Quantite > 1)
                        {
                            git.DrawString(item.Quantite + "", SystemFonts.DefaultFont, System.Drawing.Brushes.Black
                                , new PointF(16 * Sauvegarde.zoom_HotBarre - s.Width, 16 * Sauvegarde.zoom_HotBarre - s.Height));
                        }
                        g.DrawImage(timg, new Point(3 * Sauvegarde.zoom_HotBarre + i * 20 * Sauvegarde.zoom_HotBarre, 3 * Sauvegarde.zoom_HotBarre));
                    }
                }
            }


            Memoire.coeur = new PictureBox();
            Memoire.coeur.BackColor = Color.Transparent;
            Memoire.coeur.Size = new Size(90 * Sauvegarde.zoom_HotBarre, 9 * Sauvegarde.zoom_HotBarre);
            Memoire.coeur.Location = new Point(
                Memoire.form.ClientSize.Width / 2 - 45 * Sauvegarde.zoom_HotBarre
                , Memoire.form.ClientSize.Height - 43 * Sauvegarde.zoom_HotBarre);

            Memoire.coeur.Image = new Bitmap(90 * Sauvegarde.zoom_HotBarre, 9 * Sauvegarde.zoom_HotBarre);
            int vie = (int)Math.Floor(Sauvegarde.joueur.Vie);
            {
                Graphics g = Graphics.FromImage(Memoire.coeur.Image);

                for (int i = 0; i < 10; i++)
                {
                    g.DrawImage(Properties.Resources.Coeurs, new Rectangle(i * 9 * Sauvegarde.zoom_HotBarre, 0, 9 * Sauvegarde.zoom_HotBarre, 9 * Sauvegarde.zoom_HotBarre)
                        , new Rectangle(0, 9, 9, 9), GraphicsUnit.Pixel);
                    if (i * 2 + 2 <= vie)
                    {
                        g.DrawImage(Properties.Resources.Coeurs
                            , new Rectangle(i * 9 * Sauvegarde.zoom_HotBarre, 0, 9 * Sauvegarde.zoom_HotBarre, 9 * Sauvegarde.zoom_HotBarre)
                            , new Rectangle(0, 0, 9, 9), GraphicsUnit.Pixel);
                    }
                    if (i * 2 + 1 == vie)
                    {
                        g.DrawImage(Properties.Resources.Coeurs
                            , new Rectangle(i * 9 * Sauvegarde.zoom_HotBarre, 0, 9 * Sauvegarde.zoom_HotBarre, 9 * Sauvegarde.zoom_HotBarre)
                            , new Rectangle(9, 0, 9, 9), GraphicsUnit.Pixel);
                    }
                }
            }

            Memoire.nourriture = new PictureBox();
            Memoire.nourriture.BackColor = Color.Transparent;
            Memoire.nourriture.Size = new Size(90 * Sauvegarde.zoom_HotBarre, 9 * Sauvegarde.zoom_HotBarre);
            Memoire.nourriture.Location = new Point(
                Memoire.form.ClientSize.Width / 2 - 45 * Sauvegarde.zoom_HotBarre
                , Memoire.form.ClientSize.Height - 34 * Sauvegarde.zoom_HotBarre);

            Memoire.nourriture.Image = new Bitmap(90 * Sauvegarde.zoom_HotBarre, 9 * Sauvegarde.zoom_HotBarre);
            int faim = (int)Math.Floor(Sauvegarde.joueur.Faim);
            {
                Graphics g = Graphics.FromImage(Memoire.nourriture.Image);

                for (int i = 0; i < 10; i++)
                {
                    g.DrawImage(Properties.Resources.Nourriture
                        , new Rectangle(i * 9 * Sauvegarde.zoom_HotBarre, 0, 9 * Sauvegarde.zoom_HotBarre, 9 * Sauvegarde.zoom_HotBarre)
                        , new Rectangle(0, 9, 9, 9), GraphicsUnit.Pixel);
                    if (i * 2 + 2 <= faim)
                    {
                        g.DrawImage(Properties.Resources.Nourriture
                            , new Rectangle(i * 9 * Sauvegarde.zoom_HotBarre, 0, 9 * Sauvegarde.zoom_HotBarre, 9 * Sauvegarde.zoom_HotBarre)
                            , new Rectangle(0, 0, 9, 9), GraphicsUnit.Pixel);
                    }
                    if (i * 2 + 1 == faim)
                    {
                        g.DrawImage(Properties.Resources.Nourriture
                            , new Rectangle(i * 9 * Sauvegarde.zoom_HotBarre, 0, 9 * Sauvegarde.zoom_HotBarre, 9 * Sauvegarde.zoom_HotBarre)
                            , new Rectangle(9, 0, 9, 9), GraphicsUnit.Pixel);
                    }
                }
            }

            Memoire.form.Controls.Add(Memoire.coeur);
            Memoire.form.Controls.Add(Memoire.nourriture);
            Memoire.form.Controls.Add(Memoire.selector);
            Memoire.form.Controls.Add(Memoire.barre);
        }

        /// <summary>
        /// change l'ecran</br>
        /// IL FAUT ACTUALISE LE FORM
        /// </summary>
        /// <param name="necran"></param>
        public static void ChangerEcran(Ecrans necran, object autre)
        {
            if (ecran == necran) { return; }

            List<UI_Item> items = new List<UI_Item>();

            if (necran == Ecrans.Inventaire)
            {
                OuvrireInventaire();
                inventaires = (Inventaire[])autre;
                Inventaire inv = inventaires[0];
                if (inv.Hauteur > 3)
                {
                    throw new ArgumentException("inventaire trop haut");
                }
                if (inv.Longueur > 9)
                {
                    throw new ArgumentException("inventaire trop long");
                }
                for (int x = 0; x < inv.Longueur; x++)
                    for (int y = 0; y < inv.Hauteur; y++)
                    {
                        items.Add(new UI_Item(inv.GetItem(x + (y * inv.Longueur)), Memoire.InvToScreen(7 + x * 18, 53 - y * 18)));
                    }
            }
            if (necran == Ecrans.Joueur)
            {
                OuvrireInventaire();
                Joueur joueur = autre as Joueur;
                inventaires = new Inventaire[] { joueur.Inventaire, joueur.Barre };

                items.Add(new UI_Item(joueur.Casque, Memoire.InvToScreen(7, 7)));
                items.Add(new UI_Item(joueur.Plastron, Memoire.InvToScreen(7, 25)));
                items.Add(new UI_Item(joueur.Jambiere, Memoire.InvToScreen(7, 43)));
                items.Add(new UI_Item(joueur.Botte, Memoire.InvToScreen(7, 61)));

                items.Add(new UI_Item(joueur.Crafting.GetItem(0), Memoire.InvToScreen(97, 17)));
                items.Add(new UI_Item(joueur.Crafting.GetItem(2), Memoire.InvToScreen(97, 35)));
                items.Add(new UI_Item(joueur.Crafting.GetItem(1), Memoire.InvToScreen(115, 17)));
                items.Add(new UI_Item(joueur.Crafting.GetItem(3), Memoire.InvToScreen(115, 35)));
            }
            if (necran == Ecrans.Marchand)
            {
                OuvrireInventaire();
                marchand = (Marchand)autre;
                inventaires = new Inventaire[] { Sauvegarde.joueur.Inventaire, Sauvegarde.joueur.Barre };

                items.Add(new UI_Item(null, Memoire.InvToScreen(77, 14)));
                items.Add(new UI_Item(null, Memoire.InvToScreen(95, 14)));
                //items.Add(new UI_Item(null, Memoire.InvToScreen(147, 14)));
            }
            if (necran == Ecrans.TableCraft)
            {
                OuvrireInventaire();
                Inventaire inv = ((Inventaire[])autre)[0];

                items.Add(new UI_Item(inv.GetItem(0), Memoire.InvToScreen(29, 16)));
                items.Add(new UI_Item(inv.GetItem(1), Memoire.InvToScreen(47, 16)));
                items.Add(new UI_Item(inv.GetItem(2), Memoire.InvToScreen(65, 16)));

                items.Add(new UI_Item(inv.GetItem(3), Memoire.InvToScreen(29, 34)));
                items.Add(new UI_Item(inv.GetItem(4), Memoire.InvToScreen(47, 34)));
                items.Add(new UI_Item(inv.GetItem(5), Memoire.InvToScreen(65, 34)));

                items.Add(new UI_Item(inv.GetItem(6), Memoire.InvToScreen(29, 52)));
                items.Add(new UI_Item(inv.GetItem(7), Memoire.InvToScreen(47, 52)));
                items.Add(new UI_Item(inv.GetItem(8), Memoire.InvToScreen(65, 52)));

                inventaires = (Inventaire[])autre;

            }
            if (necran == Ecrans.Four)
            {
                OuvrireInventaire();

                KeyValuePair<Four_Block, Inventaire[]> returne = (KeyValuePair<Four_Block, Inventaire[]>)autre;
                four = returne.Key;
                inventaires = returne.Value;
                Inventaire inv = returne.Value[0];
                items.Add(new UI_Item(inv.GetItem(0), Memoire.InvToScreen(55, 16)));
                items.Add(new UI_Item(inv.GetItem(1), Memoire.InvToScreen(55, 52)));
                items.Add(new UI_Item(inv.GetItem(2), Memoire.InvToScreen(116, 35)));
            }
            if (necran == Ecrans.Jeu)
            {
                if (Memoire.items != null)
                {
                    SauvegarderInventaire();
                }
                inventaires = null;
                marchand = null;
                four = null;
                ActuHotBarre();
            }
            foreach (UI_Item it in items)
            {
                Memoire.items.Add(it);
                Memoire.form.Controls.Add(it);
            }

            if (ecran == Ecrans.Jeu)
            {
                Memoire.form.Controls.Remove(Memoire.barre);
                Memoire.form.Controls.Remove(Memoire.selector);
                Memoire.form.Controls.Remove(Memoire.coeur);
                Memoire.form.Controls.Remove(Memoire.nourriture);
            }

            ecran = necran;
            Memoire.form.Actualiser();
        }

        /// <summary>
        /// place les items du joueur sur l'ecran
        /// </summary>
        public static void OuvrireInventaire()
        {
            Memoire.items = new List<UI_Item>();
            Memoire.items.Add(new UI_Item(Sauvegarde.joueur.Barre.GetItem(0), Memoire.InvToScreen(7, 141)));
            Memoire.items.Add(new UI_Item(Sauvegarde.joueur.Barre.GetItem(1), Memoire.InvToScreen(25, 141)));
            Memoire.items.Add(new UI_Item(Sauvegarde.joueur.Barre.GetItem(2), Memoire.InvToScreen(43, 141)));

            Memoire.items.Add(new UI_Item(Sauvegarde.joueur.Inventaire.GetItem(0), Memoire.InvToScreen(7, 119)));
            Memoire.items.Add(new UI_Item(Sauvegarde.joueur.Inventaire.GetItem(1), Memoire.InvToScreen(25, 119)));
            Memoire.items.Add(new UI_Item(Sauvegarde.joueur.Inventaire.GetItem(2), Memoire.InvToScreen(43, 119)));
            Memoire.items.Add(new UI_Item(Sauvegarde.joueur.Inventaire.GetItem(3), Memoire.InvToScreen(7, 101)));
            Memoire.items.Add(new UI_Item(Sauvegarde.joueur.Inventaire.GetItem(4), Memoire.InvToScreen(25, 101)));
            Memoire.items.Add(new UI_Item(Sauvegarde.joueur.Inventaire.GetItem(5), Memoire.InvToScreen(43, 101)));

            foreach (UI_Item it in Memoire.items)
            {
                Memoire.form.Controls.Add(it);
            }

            Memoire.selected = new UI_Item(null, new Point(Memoire.form.ClientSize.Width / 2, Memoire.form.ClientSize.Height / 2));
            Memoire.form.Controls.Add(Memoire.selected);
        }

        /// <summary>
        /// sauvegarde l'inventaire du joueur (inventaire et barre)
        /// </summary>
        public static void SauvegarderInventaire()
        {
            foreach (UI_Item it in Memoire.items)
            {
                Point p = Memoire.ScreenToInv(it.Location.X, it.Location.Y);
                if (p.Y == 141)
                {
                    int i = (p.X - 7) / 18;
                    Sauvegarde.joueur.Barre.SetItem(i, it.Item);
                }
                else
                {
                    int y;
                    if (p.Y == 101)
                    {
                        y = 1;
                    }
                    else if (p.Y == 119)
                    {
                        y = 0;
                    }
                    else
                    {
                        continue;
                    }
                    int i = ((p.X - 7) / 18) + (y * (Sauvegarde.joueur.Inventaire.Longueur + 1));
                    Sauvegarde.joueur.Inventaire.SetItem(i, it.Item);
                }
            }
            if (Memoire.selected != null)
            {
                Memoire.selected = null;
            }
            Memoire.form.Controls.Clear();
            Memoire.items = null;
            ChangerEcran(Ecrans.Jeu, null);
        }

        public static Ecrans Ecran { get => ecran; }
    }

}
