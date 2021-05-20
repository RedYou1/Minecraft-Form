using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Echelle_Item qui hérite de la classe Item_Block
    /// </summary>
   public class Echelle_Item : Item_Block
    {
        public Echelle_Item(int quantite) : base(new Echelle_Block(),quantite)
        {
        }

        public override Item Clone()
        {
            return new Echelle_Item(quantite);
        }

        public override string id()
        {
            return "Echelle";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Echelle_Item(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
