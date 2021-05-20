using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Emeraude qui hérite de la classe Item
    /// </summary>
   public class Emeraude : Item
    {
        public Emeraude(int quantite) : base(quantite, 64)
        {
        }

        public override Item Clone()
        {
            return new Emeraude(quantite);
        }

        public override string id()
        {
            return "Emeraude";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Emeraude(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
