using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class PlastronOr : Plastron
    {
        public PlastronOr() : base(5, 114, 114)
        {

        }

        private PlastronOr(int durabiliter) : base(5, durabiliter, 114)
        {

        }

        public override Item Clone()
        {
            return new PlastronOr();
        }

        public override string id()
        {
            return "PlastronOr";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new PlastronOr(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
