using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class MaisonVillagePlaine1 : Schematique
    {
        public MaisonVillagePlaine1() : base()
        {
            blocks.Add("-3/0", new PlancheDeChene_Block());
            blocks.Add("-2/0", new PlancheDeChene_Block());
            blocks.Add("-1/0", new PlancheDeChene_Block());
            blocks.Add("0/0", new PlancheDeChene_Block());
            blocks.Add("1/0", new PlancheDeChene_Block());
            blocks.Add("2/0", new PlancheDeChene_Block());
            blocks.Add("3/0", new PlancheDeChene_Block());

            blocks.Add("-3/1", new Porte_Block(false));
            blocks.Add("-2/1", null);
            blocks.Add("-1/1", null);
            blocks.Add("0/1", null);
            blocks.Add("1/1", null);
            blocks.Add("2/1", null);
            blocks.Add("3/1", new Porte_Block(false));

            blocks.Add("-3/2", new PlancheDeChene_Block());
            blocks.Add("-2/2", null);
            blocks.Add("-1/2", null);
            blocks.Add("0/2", null);
            blocks.Add("1/2", null);
            blocks.Add("2/2", null);
            blocks.Add("3/2", new PlancheDeChene_Block());

            blocks.Add("-3/3", new PlancheDeChene_Block());
            blocks.Add("-2/3", null);
            blocks.Add("-1/3", null);
            blocks.Add("0/3", null);
            blocks.Add("1/3", null);
            blocks.Add("2/3", null);
            blocks.Add("3/3", new PlancheDeChene_Block());

            blocks.Add("-3/4", new PlancheDeChene_Block());
            blocks.Add("-2/4", new PlancheDeChene_Block());
            blocks.Add("-1/4", new PlancheDeChene_Block());
            blocks.Add("0/4", new PlancheDeChene_Block());
            blocks.Add("1/4", new PlancheDeChene_Block());
            blocks.Add("2/4", new PlancheDeChene_Block());
            blocks.Add("3/4", new PlancheDeChene_Block());

            entites.Add(new Marchand(0, 1));
        }
    }
}
