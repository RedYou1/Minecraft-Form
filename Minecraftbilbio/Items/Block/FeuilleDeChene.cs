using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe FeuilleDeChene_Item qui hérite de la classe Item_Block
    /// </summary>
   public class FeuilleDeChene_Item : Item_Block
    {
        public FeuilleDeChene_Item(int quantite) : base(new FeuilleDeChene_Block(),quantite)
        {
        }

        public override Item Clone()
        {
            return new FeuilleDeChene_Item(quantite);
        }

        public override string id()
        {
            return "FeuilleDeChene";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new FeuilleDeChene_Item(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
