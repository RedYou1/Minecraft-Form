using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Four_Item qui hérite de la classe Item_Block
    /// </summary>
    public class Four_Item : Item_Block
    {

        public Four_Item(int quantite) : base(new Four_Block(), quantite)
        {

        }

        public override Item Clone()
        {
            return new Four_Item(quantite);
        }

        public override string id()
        {
            return "Four";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Four_Item(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
