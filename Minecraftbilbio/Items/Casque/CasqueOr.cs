using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class CasqueOr : Casque
    {
        public CasqueOr() : base(5, 78, 78)
        {

        }

        private CasqueOr(int durabiliter) : base(5, durabiliter, 78)
        {

        }

        public override Item Clone()
        {
            return new CasqueOr();
        }

        public override string id()
        {
            return "CasqueOr";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new CasqueOr(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
