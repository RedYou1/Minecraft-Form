using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class PlastronDiamant : Plastron
    {
        public PlastronDiamant() : base(7, 529, 529)
        {

        }

        private PlastronDiamant(int durabiliter) : base(7, durabiliter, 529)
        {

        }

        public override Item Clone()
        {
            return new PlastronDiamant();
        }

        public override string id()
        {
            return "PlastronDiamant";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new PlastronDiamant(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
