using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Terre_Item qui hérite de la classe Item_Block
    /// </summary>
    public class Terre_Item : Item_Block
    {
        public Terre_Item(int quantite) : base(new Terre_Block(),quantite)
        {

        }

        public override string id()
        {
            return "Terre";
        }

        public override Item Clone()
        {
            return new Terre_Item(quantite);
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Terre_Item(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
