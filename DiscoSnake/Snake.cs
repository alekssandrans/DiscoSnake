using System.Collections.Generic;
using System.Linq;

namespace DiscoSnake
{
    public class Snake
    {
        private const char SnakeBodySymbol = 'o';

        public Snake()
        {
            Blocks.Add(new Block(0));
            Blocks.Add(new Block(1));
        }

        public readonly List<Block> Blocks = new List<Block>();

        public Block Head => Blocks.Last();

        public char BodySymbol => SnakeBodySymbol;

        public void Draw()
        {
            Blocks.ForEach(b => b.Draw(SnakeBodySymbol));
        }

    }
}
