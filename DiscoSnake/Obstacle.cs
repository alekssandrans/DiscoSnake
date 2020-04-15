using System;
using System.Collections.Generic;
using System.Text;

namespace DiscoSnake
{
    public class Obstacle
    {
        private const char ObstacleSymbol = '■';

        private Obstacle(int x, int y)
        {
            ConsoleColor randomColor = Block.GenerateRandomColor();
            Position = new Block(x, y, randomColor);
        }

        public Block Position { get; private set; }

        public char Symbol => ObstacleSymbol;

        public ConsoleColor Color => Position.Color;

        public static Obstacle GenerateRandomObstacle()
        {
            Block random = Block.GenerateRandomCoordinates();

            return new Obstacle(random.X, random.Y);
        }

        public void Draw()
        {
            Position.Draw(ObstacleSymbol);
        }
    }
}
