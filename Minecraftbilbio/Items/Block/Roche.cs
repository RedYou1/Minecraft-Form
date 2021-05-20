using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Roche_Item qui hérite de la classe Item_Block
    /// </summary>
    public class Roche_Item : Item_Block
    {
        public Roche_Item(int quantite) : base(new Roche_Block(),quantite)
        {

        }

        public override string id()
        {
            return "Roche";
        }

        public override Item Clone()
        {
            return new Roche_Item(quantite);
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Roche_Item(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
