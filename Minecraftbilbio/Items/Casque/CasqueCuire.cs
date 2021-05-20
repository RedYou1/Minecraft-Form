using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class CasqueCuire : Casque
    {
        public CasqueCuire() : base(1, 56, 56)
        {

        }

        private CasqueCuire(int durabiliter) : base(1, durabiliter, 56)
        {

        }

        public override Item Clone()
        {
            return new CasqueCuire();
        }

        public override string id()
        {
            return "CasqueCuire";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new CasqueCuire(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
