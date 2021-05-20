using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
/// <summary>
/// Public classe TroncDeChene_Item qui hérite des classe Item_Block et des interfaces Brulable et Cuisable
/// </summary>
    public class TroncDeChene_Item : Item_Block, Brulable, Cuisable
    {
        public TroncDeChene_Item(int quantite) : base(new TroncDeChene_Block(false), quantite)
        {
        }

        public Item CuitEn()
        {
            return new Charbon(1);
        }

        /// <summary>
        /// Méthode TempsDeCuisson qui donne un temps de cuisson à un item
        /// </summary>
        /// <returns>Le temps de cuisson de TroncDeChene</returns>
        public int TempsDeCuisson()
        {
            return 10;
        }

        /// <summary>
        /// Méthode ProduitTemperature qui donne une température à laquelle l'objet brûle
        /// </summary>
        /// <returns>La température que le TroncDeChene cuit</returns>
        public int ProduitTemperature()
        {
            return 15;
        }

        public override Item Clone()
        {
            return new TroncDeChene_Item(quantite);
        }

        public override string id()
        {
            return "TroncDeChene";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new TroncDeChene_Item(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
