using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class EpeePierre : Epee
    {
        public EpeePierre() : base(3, 131, 131)
        {

        }
        private EpeePierre(int durabiliter) : base(3, durabiliter, 131)
        {

        }

        public override Item Clone()
        {
            return new EpeePierre();
        }

        public override string id()
        {
            return "EpeePierre";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new EpeePierre(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
