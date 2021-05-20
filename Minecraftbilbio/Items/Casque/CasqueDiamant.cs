using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class CasqueDiamant : Casque
    {
        public CasqueDiamant() : base(7, 364, 364)
        {

        }

        private CasqueDiamant(int durabiliter) : base(7, durabiliter, 364)
        {

        }

        public override Item Clone()
        {
            return new CasqueDiamant();
        }

        public override string id()
        {
            return "CasqueDiamant";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new CasqueDiamant(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
