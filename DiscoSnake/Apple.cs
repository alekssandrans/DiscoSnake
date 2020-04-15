using System;

namespace DiscoSnake
{
    public class Apple
    {
        private const char AppleSymbol = 'o';

        private Apple(int x, int y)
        {
            ConsoleColor randomColor = Block.GenerateRandomColor();
            Position = new Block(x, y, randomColor);
        }

        public Block Position { get; private set; }

        public char Symbol => AppleSymbol;

        public ConsoleColor Color => Position.Color;
       
        public static Apple GenerateRandomApple()
        {
            Block random = Block.GenerateRandomCoordinates();

            return new Apple(random.X, random.Y);
        }

        public void Draw()
        {
            Position.Draw(AppleSymbol);
        }
    }
}