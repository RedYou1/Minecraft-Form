using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Minerais_Fer_Item qui hérite des classes Item_Block et Cuisable
    /// </summary>
   public class Minerais_Fer_Item : Item_Block, Cuisable
    {
        public Minerais_Fer_Item(int quantite) : base(new Minerais_Fer(),quantite)
        {
        }

        /// <summary>
        /// Méthode TempsDeCuisson qui donne le temps de cuisson pour les items ou les block d'items
        /// </summary>
        /// <returns>Le temps de cuisson du Minerais de fer</returns>
        public int TempsDeCuisson()
        {
            return 10;
        }

        public Item CuitEn()
        {
            return new Fer(1);
        }

        public override Item Clone()
        {
            return new Minerais_Fer_Item(quantite);
        }

        public override string id()
        {
            return "Minerais_Fer";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Minerais_Fer_Item(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
