using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class Arbre : Schematique
    {
        public Arbre() : base()
        {
            blocks.Add("0/0", new TroncDeChene_Block(true));
            blocks.Add("0/1", new TroncDeChene_Block(true));
            blocks.Add("0/2", new TroncDeChene_Block(true));
            blocks.Add("0/3", new TroncDeChene_Block(true));
            blocks.Add("0/4", new TroncDeChene_Block(true));

            blocks.Add("-2/3", new FeuilleDeChene_Block());
            blocks.Add("-1/3", new FeuilleDeChene_Block());
            blocks.Add("1/3", new FeuilleDeChene_Block());
            blocks.Add("2/3", new FeuilleDeChene_Block());

            blocks.Add("-2/4", new FeuilleDeChene_Block());
            blocks.Add("-1/4", new FeuilleDeChene_Block());
            blocks.Add("1/4", new FeuilleDeChene_Block());
            blocks.Add("2/4", new FeuilleDeChene_Block());

            blocks.Add("-1/5", new FeuilleDeChene_Block());
            blocks.Add("0/5", new FeuilleDeChene_Block());
            blocks.Add("1/5", new FeuilleDeChene_Block());
        }
    }
}
