using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Or qui hérite de la classe Item
    /// </summary>
   public class Or : Item
    {
        public Or(int quantite) : base(quantite, 64)
        {
        }

        public override Item Clone()
        {
            return new Or(quantite);
        }

        public override string id()
        {
            return "Or";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Or(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
