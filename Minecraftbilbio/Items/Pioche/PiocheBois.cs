using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class PiocheBois : Pioche, Brulable
    {
        public PiocheBois() : base(2, 59, 59)
        {

        }

        private PiocheBois(int durabiliter) : base(2, durabiliter, 59)
        {

        }

        public int ProduitTemperature()
        {
            return 15;
        }

        public override Item Clone()
        {
            return new PiocheBois();
        }

        public override string id()
        {
            return "PiocheBois";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new PiocheBois(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
