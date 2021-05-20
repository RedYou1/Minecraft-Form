using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    /// <summary>
    /// Public classe TableDeCraft_Item qui hérite de la classe Item_Block et de l'interface Brulable
    /// </summary>
    public class TableDeCraft_Item : Item_Block, Brulable
    {
        public TableDeCraft_Item(int quantite) : base(new TableDeCraft(), quantite)
        {

        }
        
        /// <summary>
        /// Méthode ProduitTemperature qui donne une température à un item à laquelle il va cuire
        /// </summary>
        /// <returns>La température de la table de craft</returns>
        public int ProduitTemperature()
        {
            return 15;
        }

        public override string id()
        {
            return "TableDeCraft";
        }

        public override Item Clone()
        {
            return new TableDeCraft_Item(quantite);
        }

        public override void Sauvegarder(string path)
        {
            System.IO.File.WriteAllLines(path + "\\info.txt", new string[] { "Quantite:" + quantite });
        }

        public override Item Charger(string path)
        {
            return new TableDeCraft_Item(int.Parse(System.IO.File.ReadAllLines(path + "\\info.txt")[0].Split(':')[1]));
        }
    }
}
