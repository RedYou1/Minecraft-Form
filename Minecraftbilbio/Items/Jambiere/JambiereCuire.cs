using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class JambiereCuire : Jambiere
    {
        public JambiereCuire() : base(1, 76, 76)
        {

        }

        private JambiereCuire(int durabiliter) : base(1, durabiliter, 76)
        {

        }

        public override Item Clone()
        {
            return new JambiereCuire();
        }

        public override string id()
        {
            return "JambiereCuire";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new JambiereCuire(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
