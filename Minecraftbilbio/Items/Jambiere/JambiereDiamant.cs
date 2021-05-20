using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class JambiereDiamant : Jambiere
    {
        public JambiereDiamant() : base(7, 496, 496)
        {

        }

        private JambiereDiamant(int durabiliter) : base(7, durabiliter, 496)
        {

        }

        public override Item Clone()
        {
            return new JambiereDiamant();
        }

        public override string id()
        {
            return "JambiereDiamant";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new JambiereDiamant(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
