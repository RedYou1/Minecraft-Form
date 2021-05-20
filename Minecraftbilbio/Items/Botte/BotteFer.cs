using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe BotteFer qui hérite de la classe Botte
    /// </summary>
    public class BotteFer : Botte
    {
        public BotteFer() : base(5, 196, 196)
        {
        }

        private BotteFer(int durabiliter) : base(5, durabiliter, 196)
        {
        }

        public override Item Clone()
        {
            return new BotteFer();
        }

        public override string id()
        {
            return "BotteFer";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new BotteFer(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
