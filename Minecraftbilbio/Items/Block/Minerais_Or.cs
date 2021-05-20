using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Minerais_Or_Item qui hérite des classes Item_Block et Cuisable
    /// </summary>
    public class Minerais_Or_Item : Item_Block, Cuisable
    {
        public Minerais_Or_Item(int quantite) : base(new Minerais_Or(), quantite)
        {
        }

        /// <summary>
        /// Méthode TempsDeCuisson qui donne le temps que ça prend pour que l'item sois cuit
        /// </summary>
        /// <returns>Le temps de cuisson du minerais d'or</returns>
        public int TempsDeCuisson()
        {
            return 10;
        }

        public Item CuitEn()
        {
            return new Or(1);
        }

        public override Item Clone()
        {
            return new Minerais_Or_Item(quantite);
        }

        public override string id()
        {
            return "Minerais_Or";
        }
        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Minerais_Or_Item(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
