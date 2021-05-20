using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class JambiereFer : Jambiere
    {
        public JambiereFer() : base(3, 226, 226)
        {

        }
        private JambiereFer(int durabiliter) : base(3, durabiliter, 226)
        {

        }

        public override Item Clone()
        {
            return new JambiereFer();
        }

        public override string id()
        {
            return "JambiereFer";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new JambiereFer(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
