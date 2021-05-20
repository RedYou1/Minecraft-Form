using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe BotteOr qui hérite de la classe Botte
    /// </summary>
    public class BotteOr : Botte
    {
        public BotteOr() : base(3, 92, 92)
        {
        }

        private BotteOr(int durabiliter) : base(3, durabiliter, 92)
        {
        }

        public override Item Clone()
        {
            return new BotteOr();
        }

        public override string id()
        {
            return "BotteOr";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new BotteOr(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
