using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Classe public Vache qui hérite de Entite
    /// </summary>
    public class Vache : Entite
    {

        public Vache(float x, float y) : base(x, y, 20)
        {

        }

        private Vache(float x, float y, int vie) : base(x, y, vie)
        {

        }

        public override string id()
        {
            return "Vache";
        }

        public override Entite Clone()
        {
            Vache v = new Vache(x, y);
            v.vie = vie;
            return v;
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { x + "/" + y + "/" + vie });
        }

        public override Entite Charger(string path)
        {
            string name = System.IO.File.ReadAllLines(path + "\\info.txt")[0];
            string[] coord = name.Split('/');
            return new Vache(float.Parse(coord[0]), float.Parse(coord[1]), int.Parse(coord[2]));
        }

        public override bool CliqueGauche(Entite ent, int dommage, Monde monde)
        {
            bool a = base.CliqueGauche(ent, dommage, monde);
            if (a && ent is Joueur joueur)
            {
                Random r = new Random();
                joueur.AjouterItem(new Boeuf(r.Next(1, 4)));
                int cuire = r.Next(0, 3);
                if (cuire > 0)
                {
                    joueur.AjouterItem(new Cuire(cuire));
                }
            }
            return a;
        }
    }
}
