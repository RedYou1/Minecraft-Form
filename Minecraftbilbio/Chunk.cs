using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public class Chunk
    {
        private Block[,] blocks;
        private bool generer = false;

        public Chunk(Block[,] blocks)
        {
            if (blocks.GetLength(0) == 16 && blocks.GetLength(1) == 16)
            {
                this.blocks = blocks;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Accept seulement des chunk de 16x16");
            }
        }

        public Chunk(Block[,] blocks, bool generer) : this(blocks)
        {
            this.generer = generer;
        }

        public bool Equals(Chunk chunk)
        {
            if (chunk == null)
            {
                return false;
            }

            if (generer != chunk.generer)
            {
                return false;
            }

            for (int x = 0; x < 16; x++)
                for (int y = 0; y < 16; y++)
                {
                    if ((blocks[x, y] == null) != (chunk.blocks[x, y] == null))
                    {
                        return false;
                    }
                    if (blocks[x, y] == null && chunk.blocks[x, y] == null)
                    {
                        continue;
                    }
                    if (!blocks[x, y].Equals(chunk.blocks[x, y]))
                    {
                        return false;
                    }
                }
            return true;
        }

        public Chunk Clone()
        {
            Block[,] b = new Block[16, 16];
            for (int x = 0; x < 16; x++)
                for (int y = 0; y < 16; y++)
                {
                    if (blocks[x, y] != null)
                    {
                        b[x, y] = blocks[x, y].Clone();
                    }
                }
            return new Chunk(b,generer);
        }

        public Block GetBlock(int x, int y)
        {
            return blocks[x, y];
        }

        public void SetBlock(int x, int y, Block block)
        {
            blocks[x, y] = block;
        }

        public Block[,] Blocks { get => blocks; }
        public bool Generer { get => generer; set => generer = value; }
    }
}
