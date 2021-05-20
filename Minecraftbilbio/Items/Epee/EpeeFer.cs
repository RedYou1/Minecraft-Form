using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class EpeeFer : Epee
    {
        public EpeeFer() : base(4, 250, 250)
        {
        }

        private EpeeFer(int durabiliter) : base(4, durabiliter, 250)
        {
        }

        public override Item Clone()
        {
            return new EpeeFer();
        }

        public override string id()
        {
            return "EpeeFer";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new EpeeFer(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
