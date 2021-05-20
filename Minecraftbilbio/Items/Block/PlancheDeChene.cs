using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe PlancheDeChene qui hérite de la classe Item_Block et de l'interface Brulable
    /// </summary>
   public class PlancheDeChene : Item_Block,Brulable
    {
        public PlancheDeChene(int quantite) : base(new PlancheDeChene_Block(),quantite)
        {
        }

        /// <summary>
        /// Méthode ProduitTemperature qui donne une température à un item à laquelle il va être cuit
        /// </summary>
        /// <returns>La température de la planche de chene</returns>
        public int ProduitTemperature()
        {
            return 15;
        }

        public override Item Clone()
        {
            return new PlancheDeChene(quantite);
        }

        public override string id()
        {
            return "PlancheDeChene";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new PlancheDeChene(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
