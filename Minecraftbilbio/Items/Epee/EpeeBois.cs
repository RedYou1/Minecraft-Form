using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class EpeeBois : Epee, Brulable
    {
        public EpeeBois() : base(2, 59, 59)
        {

        }

        private EpeeBois(int durabiliter) : base(2, durabiliter, 59)
        {

        }

        public int ProduitTemperature()
        {
            return 15;
        }

        public override Item Clone()
        {
            return new EpeeBois();
        }

        public override string id()
        {
            return "EpeeBois";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new EpeeBois(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
