using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class FeuilleDeChene_Block : Block
    {

        public FeuilleDeChene_Block() : base(1, "FeuilleDeChene")
        {

        }

        public override bool CanPassThrough(Entite ent, bool byGravity)
        {
            return true;
        }

        public override bool Detruire(Joueur joueur)
        {
            //si ciseau en main
            if (joueur.MainDroit() is Ciseau cis)
            {
                joueur.AjouterItem(new FeuilleDeChene_Item(1));
                cis.Durabiliter--;
                if (cis.Durabiliter <= 0)
                {
                    joueur.EnleverItem(cis);
                }
            }
            return true;
        }

        public override Block Clone()
        {
            return new FeuilleDeChene_Block();
        }

        public override Block Charger(string path)
        {
            return new FeuilleDeChene_Block();
        }
    }
}
