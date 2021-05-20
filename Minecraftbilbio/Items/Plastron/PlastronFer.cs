using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class PlastronFer : Plastron
    {
        public PlastronFer() : base(3, 241, 241)
        {

        }

        private PlastronFer(int durabiliter) : base(3, durabiliter, 241)
        {

        }

        public override Item Clone()
        {
            return new PlastronFer();
        }

        public override string id()
        {
            return "PlastronFer";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new PlastronFer(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
