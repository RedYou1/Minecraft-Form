using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Baton qui hérite des classes Item et Brulable
    /// </summary>
    public class Baton : Item, Brulable
    {
        public Baton(int quantite) : base(quantite, 64)
        {
        }

        public override Item Clone()
        {
            return new Baton(quantite);
        }

        /// <summary>
        /// Méthode ProduitTemperature qui donne une temperature à l'item
        /// </summary>
        /// <returns>La température de Baton</returns>
        public int ProduitTemperature()
        {
            return 5;
        }

        public override string id()
        {
            return "Baton";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Baton(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
