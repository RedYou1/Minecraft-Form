using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class TroncDeChene_Block : Block
    {
        private bool generateur;

        public TroncDeChene_Block(bool generateur) : base(1, "TroncDeChene")
        {
            this.generateur = generateur;
        }

        public override bool CanPassThrough(Entite entite, bool byGravity)
        {
            return generateur;
        }

        public override bool Detruire(Joueur joueur)
        {
            joueur.AjouterItem(new TroncDeChene_Item(1));
            return true;
        }

        public override bool Equals(Block block)
        {
            return base.Equals(block) && block is TroncDeChene_Block t && generateur == t.generateur;
        }

        public override Block Clone()
        {
            return new TroncDeChene_Block(generateur);
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Generateur:" + generateur });
        }

        public override Block Charger(string path)
        {
            string f = System.IO.File.ReadAllLines(path + "\\info.txt")[0];
            return new TroncDeChene_Block(bool.Parse(f.Split(':')[1]));
        }

        public bool Generateur { get => generateur; }
    }
}
