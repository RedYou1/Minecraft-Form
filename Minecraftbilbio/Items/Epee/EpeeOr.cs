using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class EpeeOr : Epee
    {
        public EpeeOr() : base(4, 32, 32)
        {
        }

        private EpeeOr(int durabiliter) : base(4, durabiliter, 32)
        {
        }

        public override Item Clone()
        {
            return new EpeeOr();
        }

        public override string id()
        {
            return "EpeeOr";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new EpeeOr(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
