using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public abstract class Schematique
    {
        protected Dictionary<string, Block> blocks = new Dictionary<string, Block>();
        protected List<Entite> entites = new List<Entite>();

        /// <summary>
        /// met le schema dans le monde</br>
        /// cree accessoirement les chunk non cree en mode non generer
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="monde"></param>
        public void AppliquerSchema(int x, int y, Monde monde)
        {
            foreach (string block in blocks.Keys)
            {
                string[] s = block.Split('/');
                monde.SetBlock(x + int.Parse(s[0]), y + int.Parse(s[1]), blocks[block]);
            }
            foreach (Entite ent in entites)
            {
                Entite t = ent.Clone();
                t.Tp(t.X+x,t.Y+y);
                monde.Entites.Add(t);
            }
        }

        /// <summary>
        /// recupere un block du schema
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Block GetBlock(int x, int y)
        {
            if (blocks.ContainsKey(x + "/" + y))
            {
                return blocks[x + "/" + y];
            }
            else
            {
                return null;
            }
        }

        public Dictionary<string, Block> Blocks { get => blocks; }
    }
}
