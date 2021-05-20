using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class EpeeDiamant : Epee
    {
        public EpeeDiamant() : base(5, 1561, 1561)
        {
        }
        private EpeeDiamant(int durabiliter) : base(5, durabiliter, 1561)
        {
        }

        public override Item Clone()
        {
            return new EpeeDiamant();
        }

        public override string id()
        {
            return "EpeeDiamant";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new EpeeDiamant(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
