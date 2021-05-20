using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Fer_Item_Block qui hérite de la classe Item_Block
    /// </summary>
   public class Fer_Item_Block : Item_Block
    {
        public Fer_Item_Block(int quantite) : base(new Block_Fer(),quantite)
        {
        }

        public override Item Clone()
        {
            return new Fer_Item_Block(quantite);
        }

        public override string id()
        {
            return "Block_Fer";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Fer_Item_Block(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
