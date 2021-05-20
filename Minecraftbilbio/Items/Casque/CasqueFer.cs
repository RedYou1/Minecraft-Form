using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class CasqueFer : Casque
    {
        public CasqueFer() : base(3, 166, 166)
        {

        }
        private CasqueFer(int durabiliter) : base(3, durabiliter, 166)
        {

        }

        public override Item Clone()
        {
            return new CasqueFer();
        }

        public override string id()
        {
            return "CasqueFer";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new CasqueFer(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
