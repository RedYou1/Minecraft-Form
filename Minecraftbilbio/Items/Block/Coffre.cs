using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Coffre_Item qui hérite des classe Item_Block et de la classe Brulable
    /// </summary>
    public class Coffre_Item : Item_Block, Brulable
    {
        public Coffre_Item(int quantite) : base(new Coffre_Block(), quantite)
        {

        }

        /// <summary>
        /// Méthode ProduitTemperature qui donne une température à l'item
        /// </summary>
        /// <returns>La température de l'item</returns>
        public int ProduitTemperature()
        {
            return 15;
        }

        public override string id()
        {
            return "Coffre";
        }

        public override Item Clone()
        {
            return new Coffre_Item(quantite);
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Coffre_Item(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
