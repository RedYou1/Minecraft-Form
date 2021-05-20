using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class Craft
    {
        private Item[,] from;
        private Item to;
        public Craft(Item[,] from, Item to)
        {
            this.from = from;
            this.to = to;
        }

        public Item[,] From { get => from; }
        public Item To { get => to; }

        /// <summary>
        /// la list de craft possible
        /// </summary>
        public static Craft[] crafts = new Craft[]
        {
            new Craft(new Item[,]{ { new PlancheDeChene(1),new PlancheDeChene(1), new PlancheDeChene(1) },
                                    { new PlancheDeChene(1),new PlancheDeChene(1),new PlancheDeChene(1)},
                                    { null, null,null } },new Porte_Item(3)),
            new Craft(new Item[,]{ { null,null,null },
                                    { new PlancheDeChene(1),new PlancheDeChene(1),new PlancheDeChene(1)},
                                    { new PlancheDeChene(1),new PlancheDeChene(1), new PlancheDeChene(1) } },new Porte_Item(3)),

            new Craft(new Item[,]{ { null,new Cuire(1), null },
                                    { null,null,new Cuire(1)},
                                    { null, new Cuire(1),null } },new Sacados()),
            new Craft(new Item[,]{ { new Cuire(1), null,null },
                                    { null,new Cuire(1),null},
                                    {  new Cuire(1),null,null } },new Sacados()),

            new Craft(new Item[,]{ { null,new Fer(1), null },
                                    { new Fer(1),null,null},
                                    { null, null,null } },new Ciseau()),
            new Craft(new Item[,]{ { null,null,new Fer(1) },
                                    { null,new Fer(1),null},
                                    { null, null,null } },new Ciseau()),
            new Craft(new Item[,]{ { null,null, null },
                                    { null,new Fer(1),null},
                                    { new Fer(1), null,null } },new Ciseau()),
            new Craft(new Item[,]{ { null,null,null },
                                    { null,null,new Fer(1)},
                                    { null, new Fer(1),null } },new Ciseau()),

            new Craft(new Item[,]{ { new Baton(1),new Baton(1), new Baton(1) },
                                    { null,new Baton(1),null},
                                    { new Baton(1), new Baton(1),new Baton(1) } },new Echelle_Item(3)),

            new Craft(new Item[,]{ { new PlancheDeChene(1),new PlancheDeChene(1), new PlancheDeChene(1) },
                                    { new PlancheDeChene(1),null,new PlancheDeChene(1)},
                                    { new PlancheDeChene(1), new PlancheDeChene(1),new PlancheDeChene(1) } },new Coffre_Item(1)),

            new Craft(new Item[,]{ { new Pierre_Item(1),new Pierre_Item(1), new Pierre_Item(1) },
                                    { new Pierre_Item(1),null,new Pierre_Item(1)},
                                    { new Pierre_Item(1), new Pierre_Item(1),new Pierre_Item(1) } },new Four_Item(1)),

            new Craft(new Item[,]{ { new PlancheDeChene(1),new PlancheDeChene(1), null },
                                    { new PlancheDeChene(1),new PlancheDeChene(1),null},
                                    { null, null,null } },new TableDeCraft_Item(1)),
            new Craft(new Item[,]{ { null,new PlancheDeChene(1), new PlancheDeChene(1) },
                                    { null,new PlancheDeChene(1),new PlancheDeChene(1)},
                                    { null, null,null } },new TableDeCraft_Item(1)),
            new Craft(new Item[,]{ { null,null, null },
                                    { new PlancheDeChene(1),new PlancheDeChene(1),null},
                                    { new PlancheDeChene(1), new PlancheDeChene(1),null } },new TableDeCraft_Item(1)),
            new Craft(new Item[,]{ { null,null, null },
                                    { null,new PlancheDeChene(1),new PlancheDeChene(1)},
                                    { null, new PlancheDeChene(1),new PlancheDeChene(1) } },new TableDeCraft_Item(1)),

            new Craft(new Item[,]{ { new TroncDeChene_Item(1),null, null },
                                    { null,null,null},
                                    { null, null,null } },new PlancheDeChene(4)),
            new Craft(new Item[,]{ { null,new TroncDeChene_Item(1), null },
                                    { null,null,null},
                                    { null, null,null } },new PlancheDeChene(4)),
            new Craft(new Item[,]{ { null,null,new TroncDeChene_Item(1) },
                                    { null,null,null},
                                    { null, null,null } },new PlancheDeChene(4)),
            new Craft(new Item[,]{ { null,null,null},
                                    { new TroncDeChene_Item(1),null, null },
                                    { null, null,null } },new PlancheDeChene(4)),
            new Craft(new Item[,]{ { null,null,null},
                                    { null,new TroncDeChene_Item(1), null },
                                    { null, null,null } },new PlancheDeChene(4)),
            new Craft(new Item[,]{ { null,null,null},
                                    { null,null,new TroncDeChene_Item(1) },
                                    { null, null,null } },new PlancheDeChene(4)),
            new Craft(new Item[,]{ { null,null,null},
                                    { null, null,null },
                                    { new TroncDeChene_Item(1),null, null }},new PlancheDeChene(4)),
            new Craft(new Item[,]{ { null,null,null},
                                    { null, null,null },
                                    { null,new TroncDeChene_Item(1), null }},new PlancheDeChene(4)),
            new Craft(new Item[,]{ { null,null,null},
                                    { null, null,null },
                                    { null,null,new TroncDeChene_Item(1) }},new PlancheDeChene(4)),

            new Craft(new Item[,]{ { new Fer_Item_Block(1),null, null },
                                    { null,null,null},
                                    { null, null,null } },new Fer(9)),
            new Craft(new Item[,]{ { null,new Fer_Item_Block(1), null },
                                    { null,null,null},
                                    { null, null,null } },new Fer(9)),
            new Craft(new Item[,]{ { null,null,new Fer_Item_Block(1) },
                                    { null,null,null},
                                    { null, null,null } },new Fer(9)),
            new Craft(new Item[,]{ { null,null,null},
                                    { new Fer_Item_Block(1),null, null },
                                    { null, null,null } },new Fer(9)),
            new Craft(new Item[,]{ { null,null,null},
                                    { null,new Fer_Item_Block(1), null },
                                    { null, null,null } },new Fer(9)),
            new Craft(new Item[,]{ { null,null,null},
                                    { null,null,new Fer_Item_Block(1) },
                                    { null, null,null } },new Fer(9)),
            new Craft(new Item[,]{ { null,null,null},
                                    { null, null,null },
                                    { new Fer_Item_Block(1),null, null }},new Fer(9)),
            new Craft(new Item[,]{ { null,null,null},
                                    { null, null,null },
                                    { null,new Fer_Item_Block(1), null }},new Fer(9)),
            new Craft(new Item[,]{ { null,null,null},
                                    { null, null,null },
                                    { null,null,new Fer_Item_Block(1) }},new Fer(9)),

            new Craft(new Item[,]{ { new Fer(1),new Fer(1), new Fer(1) },
                                    { new Fer(1),new Fer(1),new Fer(1)},
                                    { new Fer(1), new Fer(1),new Fer(1) } },new Fer_Item_Block(1)),

            new Craft(new Item[,]{ { new PlancheDeChene(1),new PlancheDeChene(1), null },
                                    { null,null,null},
                                    { null, null,null } },new Baton(4)),
            new Craft(new Item[,]{ { null,new PlancheDeChene(1), new PlancheDeChene(1) },
                                    { null,null,null},
                                    { null, null,null } },new Baton(4)),
            new Craft(new Item[,]{ { null,null,null},
                                    { new PlancheDeChene(1),new PlancheDeChene(1), null },
                                    { null, null,null } },new Baton(4)),
            new Craft(new Item[,]{ { null,null,null},
                                    { null,new PlancheDeChene(1), new PlancheDeChene(1) },
                                    { null, null,null } },new Baton(4)),
             new Craft(new Item[,]{ { null,null,null},
                                    { null, null,null },
                                    { new PlancheDeChene(1),new PlancheDeChene(1), null }},new Baton(4)),
             new Craft(new Item[,]{ { null,null,null},
                                    { null, null,null },
                                    { null,new PlancheDeChene(1), new PlancheDeChene(1) }},new Baton(4)),

             new Craft(new Item[,]{ { new PlancheDeChene(1),null,null},
                                    { new PlancheDeChene(1),new Baton(1), new Baton(1) },
                                    { new PlancheDeChene(1), null,null } },new PiocheBois()),
             new Craft(new Item[,]{ { new Pierre_Item(1),null,null},
                                    { new Pierre_Item(1),new Baton(1), new Baton(1) },
                                    { new Pierre_Item(1), null,null } },new PiochePierre()),
             new Craft(new Item[,]{ { new Or(1),null,null},
                                    { new Or(1),new Baton(1), new Baton(1) },
                                    { new Or(1), null,null } },new PiocheOr()),
             new Craft(new Item[,]{ { new Fer(1),null,null},
                                    { new Fer(1),new Baton(1), new Baton(1) },
                                    { new Fer(1), null,null } },new PiocheFer()),
             new Craft(new Item[,]{ { new Diamant(1),null,null},
                                    { new Diamant(1),new Baton(1), new Baton(1) },
                                    { new Diamant(1), null,null } },new PiocheDiamant()),

             new Craft(new Item[,]{ { new PlancheDeChene(1),new PlancheDeChene(1), new Baton(1) },
                                    { null,null,null},
                                    { null, null,null } },new EpeeBois()),
             new Craft(new Item[,]{ { null,null,null},
                                    { new PlancheDeChene(1),new PlancheDeChene(1), new Baton(1) },
                                    { null, null,null } },new EpeeBois()),
             new Craft(new Item[,]{ { null, null,null },
                                    { null,null,null},
                                    { new PlancheDeChene(1),new PlancheDeChene(1), new Baton(1) } },new EpeeBois()),

             new Craft(new Item[,]{ { new Pierre_Item(1),new Pierre_Item(1), new Baton(1) },
                                    { null,null,null},
                                    { null, null,null } },new EpeePierre()),
             new Craft(new Item[,]{ { null,null,null},
                                    { new Pierre_Item(1),new Pierre_Item(1), new Baton(1) },
                                    { null, null,null } },new EpeePierre()),
             new Craft(new Item[,]{ { null, null,null },
                                    { null,null,null},
                                    { new Pierre_Item(1),new Pierre_Item(1), new Baton(1) } },new EpeePierre()),

             new Craft(new Item[,]{ { new Or(1),new Or(1), new Baton(1) },
                                    { null,null,null},
                                    { null, null,null } },new EpeeOr()),
             new Craft(new Item[,]{ { null,null,null},
                                    { new Or(1),new Or(1), new Baton(1) },
                                    { null, null,null } },new EpeeOr()),
             new Craft(new Item[,]{ { null, null,null },
                                    { null,null,null},
                                    { new Or(1),new Or(1), new Baton(1) } },new EpeeOr()),

             new Craft(new Item[,]{ { new Fer(1),new Fer(1), new Baton(1) },
                                    { null,null,null},
                                    { null, null,null } },new EpeeFer()),
             new Craft(new Item[,]{ { null,null,null},
                                    { new Fer(1),new Fer(1), new Baton(1) },
                                    { null, null,null } },new EpeeFer()),
             new Craft(new Item[,]{ { null, null,null },
                                    { null,null,null},
                                    { new Fer(1),new Fer(1), new Baton(1) } },new EpeeFer()),

             new Craft(new Item[,]{ { new Diamant(1),new Diamant(1), new Baton(1) },
                                    { null,null,null},
                                    { null, null,null } },new EpeeDiamant()),
             new Craft(new Item[,]{ { null,null,null},
                                    { new Diamant(1),new Diamant(1), new Baton(1) },
                                    { null, null,null } },new EpeeDiamant()),
             new Craft(new Item[,]{ { null, null,null },
                                    { null,null,null},
                                    { new Diamant(1),new Diamant(1), new Baton(1) } },new EpeeDiamant()),

            new Craft(new Item[,]{ { null,new Cuire(1), new Cuire(1) },
                                    { null,new Cuire(1),null},
                                    { null, new Cuire(1), new Cuire(1) } },new CasqueCuire()),
            new Craft(new Item[,]{ { new Cuire(1), new Cuire(1),null },
                                    { new Cuire(1),null,null},
                                    { new Cuire(1), new Cuire(1),null } },new CasqueCuire()),

            new Craft(new Item[,]{ { null,new Or(1), new Or(1) },
                                    { null,new Or(1),null},
                                    { null, new Or(1), new Or(1) } },new CasqueOr()),
            new Craft(new Item[,]{ { new Or(1), new Or(1),null },
                                    { new Or(1),null,null},
                                    { new Or(1), new Or(1),null } },new CasqueOr()),

            new Craft(new Item[,]{ { null,new Fer(1), new Fer(1) },
                                    { null,new Fer(1),null},
                                    { null, new Fer(1), new Fer(1) } },new CasqueFer()),
            new Craft(new Item[,]{ { new Fer(1), new Fer(1),null },
                                    { new Fer(1),null,null},
                                    { new Fer(1), new Fer(1),null } },new CasqueFer()),

            new Craft(new Item[,]{ { null,new Diamant(1), new Diamant(1) },
                                    { null,new Diamant(1),null},
                                    { null, new Diamant(1), new Diamant(1) } },new CasqueDiamant()),
            new Craft(new Item[,]{ { new Diamant(1), new Diamant(1),null },
                                    { new Diamant(1),null,null},
                                    { new Diamant(1), new Diamant(1),null } },new CasqueDiamant()),

            new Craft(new Item[,]{ { new Cuire(1),new Cuire(1), new Cuire(1) },
                                    { null,new Cuire(1),new Cuire(1)},
                                    { new Cuire(1), new Cuire(1), new Cuire(1) } },new PlastronCuire()),

            new Craft(new Item[,]{ { new Or(1),new Or(1), new Or(1) },
                                    { null,new Or(1),new Or(1)},
                                    { new Or(1), new Or(1), new Or(1) } },new PlastronOr()),

            new Craft(new Item[,]{ { new Fer(1),new Fer(1), new Fer(1) },
                                    { null,new Fer(1),new Fer(1)},
                                    { new Fer(1), new Fer(1), new Fer(1) } },new PlastronFer()),

            new Craft(new Item[,]{ { new Diamant(1),new Diamant(1), new Diamant(1) },
                                    { null,new Diamant(1),new Diamant(1)},
                                    { new Diamant(1), new Diamant(1), new Diamant(1) } },new PlastronDiamant()),

            new Craft(new Item[,]{ { new Cuire(1),new Cuire(1), new Cuire(1) },
                                    { new Cuire(1),null,null},
                                    { new Cuire(1), new Cuire(1), new Cuire(1) } },new JambiereCuire()),

            new Craft(new Item[,]{ { new Or(1),new Or(1), new Or(1) },
                                    { new Or(1),null,null},
                                    { new Or(1), new Or(1), new Or(1) } },new JambiereOr()),

            new Craft(new Item[,]{ { new Fer(1),new Fer(1), new Fer(1) },
                                    { new Fer(1),null,null},
                                    { new Fer(1), new Fer(1), new Fer(1) } },new JambiereFer()),

            new Craft(new Item[,]{ { new Diamant(1),new Diamant(1), new Diamant(1) },
                                    { new Diamant(1),null,null},
                                    { new Diamant(1), new Diamant(1), new Diamant(1) } },new JambiereDiamant()),

            new Craft(new Item[,]{ { null,new Cuire(1), new Cuire(1) },
                                    { null,null,null},
                                    { null, new Cuire(1), new Cuire(1) } },new BotteCuire()),
            new Craft(new Item[,]{ { new Cuire(1), new Cuire(1),null },
                                    { null,null,null},
                                    { new Cuire(1), new Cuire(1),null } },new BotteCuire()),

            new Craft(new Item[,]{ { null,new Or(1), new Or(1) },
                                    { null,null,null},
                                    { null, new Or(1), new Or(1) } },new BotteOr()),
            new Craft(new Item[,]{ { new Or(1), new Or(1),null },
                                    { null,null,null},
                                    { new Or(1), new Or(1),null } },new BotteOr()),

            new Craft(new Item[,]{ { null,new Fer(1), new Fer(1) },
                                    { null,null,null},
                                    { null, new Fer(1), new Fer(1) } },new BotteFer()),
            new Craft(new Item[,]{ { new Fer(1), new Fer(1),null },
                                    { null,null,null},
                                    { new Fer(1), new Fer(1),null } },new BotteFer()),

            new Craft(new Item[,]{ { null,new Diamant(1), new Diamant(1) },
                                    { null,null,null},
                                    { null, new Diamant(1), new Diamant(1) } },new BotteDiamant()),
            new Craft(new Item[,]{ { new Diamant(1), new Diamant(1),null },
                                    { null,null,null},
                                    { new Diamant(1), new Diamant(1),null } },new BotteDiamant()),

        };
    }
}
