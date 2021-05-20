using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Charbon qui hérite des classes Item et Brulable
    /// </summary>
   public class Charbon : Item, Brulable
    {
        public Charbon(int quantite) : base(quantite,64)
        {
        }

        /// <summary>
        /// Méthode ProduitTemperature qui donne la température qui se créé quand on le brûle
        /// </summary>
        /// <returns>La température de l'item quand on le brûle</returns>
        public int ProduitTemperature()
        {
            return 80;
        }

        public override Item Clone()
        {
            return new Charbon(quantite);
        }

        public override string id()
        {
            return "Charbon";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new Charbon(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
