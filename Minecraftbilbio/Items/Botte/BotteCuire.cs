using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe BotteCuire qui hérite de la classe Botte
    /// </summary>
    public class BotteCuire : Botte
    {
        public BotteCuire() : base(2, 66, 66)
        {

        }

        private BotteCuire(int durabiliter) : base(2, durabiliter, 66)
        {

        }

        public override Item Clone()
        {
            return new BotteCuire();
        }

        public override string id()
        {
            return "BotteCuire";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new BotteCuire(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
