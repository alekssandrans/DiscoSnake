using System;

namespace DiscoSnake
{
    public struct Block
    {
        public int X;
        public int Y;
        public ConsoleColor Color;

        public Block(int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public static Block GenerateRandomCoordinates()
        {
            Random randomCoordinate = new Random();

            int x = randomCoordinate.Next(0, Console.WindowWidth);
            int y = randomCoordinate.Next(0, Console.WindowHeight);

            return new Block(x, y);
        }

        public static ConsoleColor GenerateRandomColor()
        {
            ConsoleColor randomColor = default;
            Random random = new Random();
            int number = random.Next(1, 4);
            switch (number)
            {
                case 1: randomColor = ConsoleColor.Red; break;
                case 2: randomColor = ConsoleColor.Green; break;
                case 3: randomColor = ConsoleColor.Blue; break;
                case 4: randomColor = ConsoleColor.Magenta; break;
            }

            return randomColor;
        }

        public void Draw(char symbol)
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(X, Y);
            Console.Write(symbol);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}