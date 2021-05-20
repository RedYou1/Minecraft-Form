using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Class Porte_Block qui herite de Block
    /// </summary>
    public class Porte_Block : Block
    {
        private bool ouvert;
        /// <summary>
        /// Constructeur Porte_Block
        /// </summary>
        public Porte_Block(bool ouvert) : base(1, "Porte_" + (ouvert ? "Ouvert" : "Fermee"))
        {
            this.ouvert = ouvert;
        }

        public override bool Equals(Block block)
        {
            return base.Equals(block) && block is Porte_Block porte && ouvert == porte.ouvert;
        }

        public override Tuple<bool, Tuple<Ecrans, object>> CliqueDroit(Joueur joueur)
        {
            ouvert = !ouvert;
            if (ouvert)
            {
                name = "Porte_Ouvert";
            }
            else
            {
                name = "Porte_Fermee";
            }
            return new Tuple<bool, Tuple<Ecrans, object>>(true, null);
        }

        public override bool CanPassThrough(Entite ent, bool byGravity)
        {
            return ouvert;
        }

        public override bool Detruire(Joueur joueur)
        {
            joueur.AjouterItem(new Porte_Item(1));
            return true;
        }

        public override Block Clone()
        {
            return new Porte_Block(ouvert);
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Ouvert:" + ouvert });
        }

        public override Block Charger(string path)
        {
            string f = System.IO.File.ReadAllLines(path + "\\info.txt")[0];
            return new Porte_Block(bool.Parse(f.Split(':')[1]));
        }
    }
}
