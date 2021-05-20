using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe public Zombie qui hérite de Entite
    /// </summary>
    public class Zombie : Entite
    {

        public Zombie(float x, float y) : base(x, y, 20)
        {

        }

        private Zombie(float x, float y, int vie) : base(x, y, vie)
        {

        }

        public override string id()
        {
            return "Zombie";
        }

        public override Entite Clone()
        {
            Zombie z = new Zombie(x, y);
            z.vie = vie;
            return z;
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { x + "/" + y + "/" + vie });
        }

        public override Entite Charger(string path)
        {
            string name = System.IO.File.ReadAllLines(path + "\\info.txt")[0];
            string[] coord = name.Split('/');
            return new Zombie(float.Parse(coord[0]), float.Parse(coord[1]), int.Parse(coord[2]));
        }

        public override bool CliqueGauche(Entite ent, int dommage, Monde monde)
        {
            base.CliqueGauche(ent, dommage, monde);
            if (ent != null && ent is Joueur joueur)
            {
                joueur.CliqueGauche(this, 1, monde);
            }
            return true;
        }

        public override bool Comportement(Monde monde)
        {
            foreach (Entite ent in monde.Entites)
            {
                if (ent is Joueur j)
                {
                    if (X != j.X && X != j.X - 1 && X != j.X + 1)
                    {
                        float tx = x > j.X ? -1 : 1;
                        Block b = monde.GetBlock((int)(x + tx), (int)y, false);
                        Block b2 = monde.GetBlock((int)(x + tx), (int)y + 1, false);
                        if (b == null || b.CanPassThrough(this, false)
                            || b2 == null || b2.CanPassThrough(this, false))
                        {
                            Bouger(tx / 2, 0, monde);
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }
    }
}
