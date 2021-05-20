using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe Ciseau qui hérite des classes Item
    /// </summary>
    public class Ciseau : Item, Cassable
    {
        private int durabiliter = 238;
        private int maxDurabiliter = 238;
        public Ciseau() : base(1, 1)
        {

        }

        private Ciseau(int durabiliter) : base(1, 1)
        {
            this.durabiliter = durabiliter;
        }

        public override bool Equals(Item item)
        {
            return base.Equals(item) && item is Ciseau ciseau && durabiliter == ciseau.durabiliter && maxDurabiliter == ciseau.maxDurabiliter;
        }

        public override Item Clone()
        {
            return new Ciseau();
        }

        public override string id()
        {
            return "Ciseau";
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Durabiliter:" + durabiliter });
        }

        public override Item Charger(string path)
        {
            return new Ciseau(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }

        public int Durabiliter { get => durabiliter; set => durabiliter = value; }

        public int MaxDurabiliter { get => maxDurabiliter; }
    }
}
