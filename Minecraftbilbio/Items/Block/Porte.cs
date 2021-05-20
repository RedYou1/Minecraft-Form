using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Porte_Item qui hérite de la classe Item_Block
    /// </summary>
    public class Porte_Item : Item_Block
    {
        public Porte_Item(int quantite) : base(new Porte_Block(false), quantite)
        {
        }

        public override Item Clone()
        {
            return new Porte_Item(quantite);
        }

        public override string id()
        {
            return "Porte";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Porte_Item(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
