using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class Boeuf : Nourriture, Cuisable
    {
        public Boeuf(int quantite) : base(quantite, 64, 3)
        {
        }

        public override Item Clone()
        {
            return new Boeuf(quantite);
        }

        public override string id()
        {
            return "Boeuf";
        }

        public int TempsDeCuisson()
        {
            return 10;
        }

        public Item CuitEn()
        {
            return new Boeuf_Cuit(1);
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Boeuf(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
