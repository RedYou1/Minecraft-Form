using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class JambiereOr : Jambiere
    {
        public JambiereOr() : base(5, 106, 106)
        {

        }

        private JambiereOr(int durabiliter) : base(5, durabiliter, 106)
        {

        }

        public override Item Clone()
        {
            return new JambiereOr();
        }

        public override string id()
        {
            return "JambiereOr";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new JambiereOr(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
