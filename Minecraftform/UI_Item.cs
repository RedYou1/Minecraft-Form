using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Forms;
using Minecraftbilbio;
using System.Drawing;

namespace Minecraftform
{
    public class UI_Item : PictureBox
    {
        private Item item;

        public UI_Item(Item item, Point location)
        {
            this.item = item;
            int w = Memoire.form.ClientSize.Width;
            int h = Memoire.form.ClientSize.Height;
            BackColor = System.Drawing.Color.Transparent;
            Size itsize = new Size((int)((16f / 177f) * (w - (w / 32) * 2)), (int)((16f / 166f) * (h - (h / 32) * 2)));
            if (item != null)
            {
                Bitmap img = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject("Item_" + item.id()),
                    itsize);
                Graphics g = Graphics.FromImage(img);
                SizeF s = g.MeasureString(item.Quantite + "", SystemFonts.DefaultFont);
                if (item is Cassable cas)
                {
                    float pour = cas.Durabiliter / (float)cas.MaxDurabiliter;
                    g.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(
                        255 - (int)(255 * pour), (int)(255 * pour), 0
                        ))
                        , new Rectangle(0, itsize.Height / 2 + itsize.Height / 4
                            , (int)(itsize.Width * pour), itsize.Height / 4));
                }
                if (item.Quantite > 1)
                {
                    g.DrawString(item.Quantite + "", SystemFonts.DefaultFont, System.Drawing.Brushes.Black
                        , new PointF(itsize.Width - s.Width, itsize.Height - s.Height));
                }
                Image = img;
                Size = itsize;
            }
            else
            {
                /*
                Image = new Bitmap(itsize.Width, itsize.Height);
                Graphics g = Graphics.FromImage(Image);
                g.FillRectangle(System.Drawing.Brushes.Black, new Rectangle(0, 0, itsize.Width, itsize.Height));
                */
                Hide();
                Size = itsize;
            }
            Location = location;
            MouseMove += OnMouseMove;
            MouseDown += OnMouseDown;
        }

        private void AnnulerMove(UI_Item item)
        {
            Memoire.items.Remove(item);
            Memoire.form.Controls.Remove(item);
            Memoire.form.Controls.Remove(Memoire.selected);
            UI_Item temp = Memoire.selected;
            Memoire.selected = item;
            Memoire.form.Controls.Add(Memoire.selected);
            Memoire.items.Add(temp);
            Memoire.form.Controls.Add(temp);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (Memoire.selected != null)
            {
                Memoire.selected.Location = new Point(e.X + Location.X - Memoire.selected.Size.Width / 2, e.Y + Location.Y - Memoire.selected.Size.Height / 2);
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (Memoire.items != null && Memoire.selected != null)
            {
                foreach (UI_Item it in Memoire.items)
                {
                    if (it.Location.X <= e.X + Location.X && it.Location.X + it.Size.Width >= e.X + Location.X
                        && it.Location.Y <= e.Y + Location.Y && it.Location.Y + it.Size.Height >= e.Y + Location.Y)
                    {
                        if (Memoire.shiftKeyDown && Memoire.selected.item != null && it.item == null && Memoire.selected.item.Quantite > 1)
                        {
                            Memoire.items.Remove(it);
                            Memoire.form.Controls.Remove(it);

                            Item item = Memoire.selected.item.Clone();
                            item.Quantite = (int)Math.Floor((decimal)item.Quantite / 2);

                            UI_Item nit = new UI_Item(item, it.Location);
                            Memoire.items.Add(nit);
                            Memoire.form.Controls.Add(nit);


                            Memoire.form.Controls.Remove(Memoire.selected);
                            Memoire.selected.item.Quantite = (int)Math.Ceiling((decimal)Memoire.selected.item.Quantite / 2);
                            nit = new UI_Item(Memoire.selected.item, Memoire.selected.Location);
                            Memoire.form.Controls.Add(nit);
                            Memoire.selected = nit;
                        }
                        else if (Memoire.shiftKeyDown && Memoire.selected.item == null && it.item != null && it.item.Quantite > 1)
                        {
                            Memoire.items.Remove(it);
                            Memoire.form.Controls.Remove(it);

                            Item item = it.item.Clone();
                            item.Quantite = (int)Math.Floor((decimal)item.Quantite / 2);

                            UI_Item nit = new UI_Item(item, it.Location);
                            Memoire.items.Add(nit);
                            Memoire.form.Controls.Add(nit);


                            Memoire.form.Controls.Remove(Memoire.selected);
                            Memoire.selected.item = it.item.Clone();
                            Memoire.selected.item.Quantite = (int)Math.Ceiling((decimal)Memoire.selected.item.Quantite / 2);
                            nit = new UI_Item(Memoire.selected.item, Memoire.selected.Location);
                            Memoire.form.Controls.Add(nit);
                            Memoire.selected = nit;
                        }
                        else if (Memoire.ctrlKeyDown && Memoire.selected.item != null && it.item == null)
                        {
                            Memoire.items.Remove(it);
                            Memoire.form.Controls.Remove(it);

                            Item item = Memoire.selected.item.Clone();
                            item.Quantite = 1;

                            UI_Item nit = new UI_Item(item, it.Location);
                            Memoire.items.Add(nit);
                            Memoire.form.Controls.Add(nit);


                            Memoire.form.Controls.Remove(Memoire.selected);
                            Memoire.selected.item.Quantite--;
                            if (Memoire.selected.item.Quantite == 0)
                            {
                                Memoire.selected.item = null;
                            }
                            nit = new UI_Item(Memoire.selected.item, Memoire.selected.Location);
                            Memoire.form.Controls.Add(nit);
                            Memoire.selected = nit;
                        }
                        else if (Memoire.ctrlKeyDown && Memoire.selected.item != null && it.item != null
                            && Memoire.selected.item.id() == it.item.id() && it.item.Quantite < it.item.MaxQuantite)
                        {
                            Memoire.items.Remove(it);
                            Memoire.form.Controls.Remove(it);

                            it.item.Quantite++;

                            UI_Item nit = new UI_Item(it.item, it.Location);
                            Memoire.items.Add(nit);
                            Memoire.form.Controls.Add(nit);


                            Memoire.form.Controls.Remove(Memoire.selected);
                            Memoire.selected.item.Quantite--;
                            if (Memoire.selected.item.Quantite == 0)
                            {
                                Memoire.selected.item = null;
                            }
                            nit = new UI_Item(Memoire.selected.item, Memoire.selected.Location);
                            Memoire.form.Controls.Add(nit);
                            Memoire.selected = nit;
                        }
                        else if (Memoire.selected.item != null && it.item != null
                          && Memoire.selected.item.id() == it.item.id()
                          && Memoire.selected.item.Quantite + it.item.Quantite <= it.item.MaxQuantite)
                        {
                            it.item.Quantite += Memoire.selected.item.Quantite;
                            Item item = it.item;
                            Point p = it.Location;

                            Memoire.items.Remove(Memoire.selected);
                            Memoire.form.Controls.Remove(Memoire.selected);
                            Memoire.items.Remove(it);
                            Memoire.form.Controls.Remove(it);

                            UI_Item nit = new UI_Item(item, p);
                            Memoire.items.Add(nit);
                            Memoire.form.Controls.Add(nit);

                            Memoire.selected = new UI_Item(null, new Point(0, 0));
                            Memoire.items.Add(Memoire.selected);
                            Memoire.form.Controls.Add(Memoire.selected);
                        }
                        else
                        {
                            Memoire.selected.Location = it.Location;
                            Memoire.items.Add(Memoire.selected);
                            Memoire.selected = it;
                            Memoire.items.Remove(it);
                        }

                        if (it.Location.Y <= Memoire.form.Height / 2)
                        {
                            switch (EcranDeJeu.Ecran)
                            {
                                case Ecrans.Joueur:
                                    DansJoueur(Memoire.items);
                                    break;
                                case Ecrans.Marchand:
                                    DansMarchand(Memoire.items);
                                    break;
                                case Ecrans.Four:
                                    DansFour(Memoire.items);
                                    break;
                                case Ecrans.TableCraft:
                                    DansTableDeCraft(Memoire.items);
                                    break;
                                case Ecrans.Inventaire:
                                    DansAutreInventaire(Memoire.items);
                                    break;
                            }
                        }

                        return;
                    }
                }
            }
        }

        public void DansJoueur(List<UI_Item> Uitems)
        {
            UI_Item[,] crafts = new UI_Item[2, 2];
            UI_Item tocraft = null;
            UI_Item casque = null;
            UI_Item plastron = null;
            UI_Item jambiere = null;
            UI_Item botte = null;
            foreach (UI_Item init in Uitems)
            {
                Point p = Memoire.ScreenToInv(init.Location.X, init.Location.Y);
                if (p.X == 7)
                {
                    if (p.Y == 7)
                    {
                        casque = init;
                    }
                    if (p.Y == 25)
                    {
                        plastron = init;
                    }
                    if (p.Y == 43)
                    {
                        jambiere = init;
                    }
                    if (p.Y == 61)
                    {
                        botte = init;
                    }
                }
                if ((p.X == 97 || p.X == 115) && (p.Y == 17 || p.Y == 35))
                {
                    Sauvegarde.joueur.Crafting.SetItem((p.X - 97) / 18 + (((p.Y - 17) / 18) * 2), init.item);
                    crafts[(p.X - 97) / 18, (p.Y - 17) / 18] = init;
                }
                if (p.X == 153 && p.Y == 27)
                {
                    tocraft = init;
                }
            }

            if (casque.item is Casque c)
            {
                Sauvegarde.joueur.Casque = c;
            }
            else if (casque.item == null)
            {
                Sauvegarde.joueur.Casque = null;
            }
            else
            {
                Sauvegarde.joueur.Casque = null;
                AnnulerMove(casque);
            }
            if (plastron.item is Plastron pl)
            {
                Sauvegarde.joueur.Plastron = pl;
            }
            else if (plastron.item == null)
            {
                Sauvegarde.joueur.Plastron = null;
            }
            else
            {
                Sauvegarde.joueur.Plastron = null;
                AnnulerMove(plastron);
            }
            if (jambiere.item is Jambiere j)
            {
                Sauvegarde.joueur.Jambiere = j;
            }
            else if (jambiere.item == null)
            {
                Sauvegarde.joueur.Jambiere = null;
            }
            else
            {
                Sauvegarde.joueur.Jambiere = null;
                AnnulerMove(jambiere);
            }
            if (botte.item is Botte b)
            {
                Sauvegarde.joueur.Botte = b;
            }
            else if (botte.item == null)
            {
                Sauvegarde.joueur.Botte = null;
            }
            else
            {
                Sauvegarde.joueur.Botte = null;
                AnnulerMove(botte);
            }

            Craft(crafts, tocraft, Sauvegarde.joueur.Crafting, Memoire.InvToScreen(153, 27), new Point(97, 17));
        }

        public void DansMarchand(List<UI_Item> Uitems)
        {
            UI_Item UitV1 = null;
            UI_Item UitV2 = null;
            UI_Item itDo = null;
            foreach (UI_Item init in Uitems)
            {
                Point p = Memoire.ScreenToInv(init.Location.X, init.Location.Y);
                if (p.Y == 14)
                {
                    if (p.X == 77)
                    {
                        UitV1 = init;
                    }
                    if (p.X == 95)
                    {
                        UitV2 = init;
                    }
                    if (p.X == 147)
                    {
                        itDo = init;
                    }
                }
            }

            if (Memoire.selected.item != null && itDo != null && itDo.item != null
                && Memoire.selected.item.id() != itDo.item.id())
            {
                AnnulerMove(itDo);
                return;
            }

            if (UitV1.item == null && UitV2.item != null)
            {
                Point l1 = UitV1.Location;
                UitV1.Location = UitV2.Location;
                UitV2.Location = l1;

                UI_Item i1 = UitV1;
                UitV1 = UitV2;
                UitV2 = i1;
            }

            if (UitV1.item != null)
            {
                foreach (Echange echange in EcranDeJeu.marchand.Echanges)
                {
                    bool iv2 = echange.ItemVoulu2 == null;
                    bool i2 = UitV2.item == null;
                    if (echange.ItemVoulu != null &&
                        echange.ItemVoulu.id() == UitV1.item.id() && echange.ItemVoulu.Quantite <= UitV1.item.Quantite
                        && (iv2 == i2)
                        && ((
                            (!iv2 && !i2)
                                && echange.ItemVoulu2.id() == UitV2.item.id() && echange.ItemVoulu2.Quantite <= UitV2.item.Quantite)
                            || (iv2 && i2)))
                    {
                        if (itDo == null)
                        {
                            itDo = new UI_Item(echange.ItemDonne.Clone(), Memoire.InvToScreen(147, 14));
                            Memoire.items.Add(itDo);
                            Memoire.form.Controls.Add(itDo);
                        }
                        else
                        {
                            Memoire.items.Remove(itDo);
                            Memoire.form.Controls.Remove(itDo);

                            if (Memoire.selected.item == null && itDo.item != null)
                            {
                                Memoire.form.Controls.Remove(Memoire.selected);
                                Memoire.selected = new UI_Item(itDo.item, Memoire.selected.Location);
                                itDo.item = null;
                                Memoire.form.Controls.Add(Memoire.selected);
                            }

                            if (Memoire.selected.item != null && Memoire.selected.item.id() == echange.ItemDonne.id())
                            {
                                bool a = true;
                                if (UitV1.item != null && echange.ItemVoulu != null)
                                {
                                    Memoire.items.Remove(UitV1);
                                    Memoire.form.Controls.Remove(UitV1);
                                    UitV1.item.Quantite -= echange.ItemVoulu.Quantite;

                                    if (UitV1.item.Quantite <= 0)
                                    {
                                        UitV1.item = null;
                                        a = false;
                                    }
                                    UitV1 = new UI_Item(UitV1.item, UitV1.Location);
                                    Memoire.items.Add(UitV1);
                                    Memoire.form.Controls.Add(UitV1);
                                }
                                if (UitV2.item != null && echange.ItemVoulu2 != null)
                                {
                                    Memoire.items.Remove(UitV2);
                                    Memoire.form.Controls.Remove(UitV2);
                                    UitV2.item.Quantite -= echange.ItemVoulu2.Quantite;
                                    if (UitV2.item.Quantite <= 0)
                                    {
                                        UitV2.item = null;
                                        a = false;
                                    }
                                    UitV2 = new UI_Item(UitV2.item, UitV2.Location);
                                    Memoire.items.Add(UitV2);
                                    Memoire.form.Controls.Add(UitV2);
                                }
                                if (a)
                                {
                                    itDo = new UI_Item(echange.ItemDonne.Clone(), itDo.Location);
                                    Memoire.items.Add(itDo);
                                    Memoire.form.Controls.Add(itDo);
                                }
                            }
                            else
                            {
                                itDo = new UI_Item(echange.ItemDonne.Clone(), itDo.Location);
                                Memoire.items.Add(itDo);
                                Memoire.form.Controls.Add(itDo);
                            }
                        }
                        return;
                    }
                }
            }
            if (itDo != null)
            {
                Memoire.form.Controls.Remove(itDo);
                Memoire.items.Remove(itDo);
            }
        }

        public void DansFour(List<UI_Item> Uitems)
        {
            UI_Item[] inv = new UI_Item[3];
            foreach (UI_Item init in Uitems)
            {
                Point p = Memoire.ScreenToInv(init.Location.X, init.Location.Y);
                if (p.X == 55)
                {
                    if (p.Y == 16)
                    {
                        EcranDeJeu.four.Inventaire.SetItem(0, init.item);
                        inv[0] = init;
                    }
                    else if (p.Y == 52)
                    {
                        EcranDeJeu.four.Inventaire.SetItem(1, init.item);
                        inv[1] = init;
                    }
                }
                if (p.X == 116 && p.Y == 35)
                {
                    EcranDeJeu.four.Inventaire.SetItem(2, init.item);
                    inv[2] = init;
                }
            }
            if (inv[0] != null && inv[1] != null && inv[2] != null)
            {
                EcranDeJeu.four.Update();

                for (int i = 0; i <= 2; i++)
                {
                    Memoire.items.Remove(inv[i]);
                    Memoire.form.Controls.Remove(inv[i]);
                }

                UI_Item it1 = new UI_Item(EcranDeJeu.four.Inventaire.GetItem(0), Memoire.InvToScreen(55, 16));
                Memoire.items.Add(it1);
                Memoire.form.Controls.Add(it1);
                UI_Item it2 = new UI_Item(EcranDeJeu.four.Inventaire.GetItem(1), Memoire.InvToScreen(55, 52));
                Memoire.items.Add(it2);
                Memoire.form.Controls.Add(it2);
                UI_Item it3 = new UI_Item(EcranDeJeu.four.Inventaire.GetItem(2), Memoire.InvToScreen(116, 35));
                Memoire.items.Add(it3);
                Memoire.form.Controls.Add(it3);
            }
        }

        public void DansTableDeCraft(List<UI_Item> Uitems)
        {
            Inventaire invcraft = EcranDeJeu.inventaires[0];
            UI_Item[,] inv = new UI_Item[3, 3];
            UI_Item c = null;
            foreach (UI_Item init in Uitems)
            {
                Point p = Memoire.ScreenToInv(init.Location.X, init.Location.Y);
                if (p.X >= 28 && p.X <= 83 && p.Y >= 16 && p.Y <= 52)
                {
                    int i = (p.X - 29) / 18 + ((p.Y - 16) / 18 * invcraft.Longueur);
                    invcraft.SetItem(i, init.item);
                    inv[(p.X - 29) / 18, (p.Y - 16) / 18] = init;
                }
                if (p.X == 125 && p.Y == 36)
                {
                    c = init;
                }
            }
            Craft(inv, c, invcraft, Memoire.InvToScreen(125, 36), new Point(29, 16));
        }

        public void Craft(UI_Item[,] items, UI_Item to, Inventaire invcraft, Point toCoord, Point start)
        {
            Craft craft = TableDeCraft.CheckAll(invcraft);
            if (craft != null)
            {
                for (int x = 0; x < items.GetLength(0); x++)
                    for (int y = 0; y < items.GetLength(1); y++)
                    {
                        Memoire.items.Remove(items[x, y]);
                        Memoire.form.Controls.Remove(items[x, y]);
                    }

                if (to != null)
                {
                    Memoire.items.Remove(to);
                    Memoire.form.Controls.Remove(to);

                    if (Memoire.selected.item == null && to.item != null)
                    {
                        Memoire.form.Controls.Remove(Memoire.selected);
                        Memoire.selected = to;
                        to = null;
                        Memoire.form.Controls.Add(Memoire.selected);
                    }
                }

                bool a = true;
                for (int x = 0; x < items.GetLength(0); x++)
                    for (int y = 0; y < items.GetLength(1); y++)
                    {
                        Item item = invcraft.GetItem(x + (y * items.GetLength(0)));
                        if (Memoire.selected != null && Memoire.selected.item != null &&
                            craft.To.id() == Memoire.selected.item.id()
                            && item != null && craft.From[x, y] != null)
                        {
                            item.Quantite -= craft.From[x, y].Quantite;
                            if (item.Quantite == 0)
                            {
                                a = false;
                                invcraft.SetItem(x + (y * items.GetLength(0)), null);
                                item = null;
                            }
                        }
                        UI_Item uit = new UI_Item(item, Memoire.InvToScreen(start.X + x * 18, start.Y + y * 18));
                        Memoire.items.Add(uit);
                        Memoire.form.Controls.Add(uit);
                    }
                if (a)
                {
                    UI_Item cit = new UI_Item(craft.To.Clone(), toCoord);
                    Memoire.items.Add(cit);
                    Memoire.form.Controls.Add(cit);
                }
            }
            if (craft == null && to != null && to.item != null)
            {
                Memoire.items.Remove(to);
                Memoire.form.Controls.Remove(to);
            }
        }

        public void DansAutreInventaire(List<UI_Item> Uitems)
        {
            Inventaire inv = EcranDeJeu.inventaires[0];
            UI_Item anul = null;
            foreach (UI_Item init in Uitems)
            {
                Point p = Memoire.ScreenToInv(init.Location.X, init.Location.Y);
                int ix = (p.X - 7) / 18;
                int iy = ((p.Y - 53) / 18) * -1;
                if (ix >= 0 && ix < inv.Longueur && iy >= 0 && iy < inv.Hauteur)
                {
                    int i = ix + (iy * inv.Longueur);
                    if (inv.Nom == "SacADos" && init.item != null && init.item.id() == "SacADos")
                    {
                        anul = init;
                        continue;
                    }
                    inv.SetItem(i, init.item);
                }
            }
            if (anul != null)
            {
                AnnulerMove(anul);
            }
        }

        public Item Item
        {
            get => item;
        }
    }
}
