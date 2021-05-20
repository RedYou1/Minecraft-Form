using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe BotteDiamant qui hérite de la classe Botte
    /// </summary>
    public class BotteDiamant : Botte
    {
        public BotteDiamant() : base(7, 430, 430)
        {
        }

        private BotteDiamant(int durabiliter) : base(7, durabiliter, 430)
        {
        }

        public override Item Clone()
        {
            return new BotteDiamant();
        }

        public override string id()
        {
            return "BotteDiamant";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new BotteDiamant(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
