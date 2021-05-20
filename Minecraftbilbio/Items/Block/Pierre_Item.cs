using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Pierre_Item qui hérite des classes Item_Block et Cuisable
    /// </summary>
    public class Pierre_Item : Item_Block, Cuisable
    {
        public Pierre_Item(int quantite) : base(new Pierre_Block(),quantite)
        {

        }
        /// <summary>
        /// Méthode TempsDeCuisson qui donne un temps de cuisson à l'item pour savoir quand la cuisson va être fini
        /// </summary>
        /// <returns>Le temps de cuisson de la pierre</returns>

        public int TempsDeCuisson()
        {
            return 10;
        }

        public Item CuitEn()
        {
            return new Roche_Item(1);
        }

        public override string id()
        {
            return "Pierre";
        }

        public override Item Clone()
        {
            return new Pierre_Item(quantite);
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Pierre_Item(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
