using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class PlastronCuire : Plastron
    {
        public PlastronCuire() : base(1, 82, 82)
        {

        }

        private PlastronCuire(int durabiliter) : base(1, durabiliter, 82)
        {

        }

        public override Item Clone()
        {
            return new PlastronCuire();
        }

        public override string id()
        {
            return "PlastronCuire";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new PlastronCuire(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
