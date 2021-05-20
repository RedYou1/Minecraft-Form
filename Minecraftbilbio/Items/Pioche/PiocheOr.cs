using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class PiocheOr : Pioche
    {
        public PiocheOr() : base(6, 32, 32)
        {
        }
        private PiocheOr(int durabiliter) : base(6, durabiliter, 32)
        {
        }

        public override Item Clone()
        {
            return new PiocheOr();
        }

        public override string id()
        {
            return "PiocheOr";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new PiocheOr(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
