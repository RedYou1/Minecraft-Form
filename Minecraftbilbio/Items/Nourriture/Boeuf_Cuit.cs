using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
   public class Boeuf_Cuit : Nourriture
    {
        public Boeuf_Cuit(int quantite) : base(quantite, 64,8)
        {
        }

        public override Item Clone()
        {
            return new Boeuf_Cuit(quantite);
        }

        public override string id()
        {
            return "Boeuf_Cuit";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Boeuf_Cuit(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
