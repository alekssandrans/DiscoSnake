using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DiscoSnake
{
    public enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }
    public class GameEngine
    {
        private Snake _snake;
        private List<Obstacle> _obstacles;

        private GameEngine()
        {

        }

        private static void SetConsoleSettings()
        {
            Console.BufferHeight = Console.WindowHeight = 15;
            Console.BufferWidth = Console.WindowWidth = 50;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void InitializeGame()
        {
            Console.WriteLine("Welcome to DISCO SNAKE");
            Console.WriteLine("Press any key to begin..");
            Console.ReadKey();
            Console.Clear();
        }

        private void EndCurrentGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            string gameOver = "Game over";
            Console.SetCursorPosition((Console.WindowWidth - gameOver.Length) / 2, Console.WindowHeight / 2 - 1);
            Console.WriteLine(gameOver.ToUpper());
            string scoreInfo = $"Score : {_snake.Blocks.Count}";
            Console.SetCursorPosition((Console.WindowWidth - scoreInfo.Length) / 2, Console.WindowHeight / 2);
            Console.Write(scoreInfo);
            Console.SetCursorPosition(0, Console.WindowHeight / 2 + 2);
            Console.Write("Press enter to start over or press escape to exit: ");
            Console.CursorVisible = true;
            Console.SetCursorPosition((Console.WindowWidth + gameOver.Length) / 2, Console.WindowHeight / 2 - 1);

            Console.BackgroundColor = ConsoleColor.Black;
            ConsoleKeyInfo keyInput = Console.ReadKey(true);
            if (keyInput.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                Environment.Exit(0);
            }
            else if (keyInput.Key == ConsoleKey.Enter)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                StartOver();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                Environment.Exit(0);
            }
        }

        private bool IsOnSnake(Block block)
        {
            foreach (Block snakeBlock in _snake.Blocks)
            {
                if (snakeBlock.X == block.X && snakeBlock.Y == block.Y)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsOnObstacles(Block block)
        {
            foreach (Obstacle obstacle in _obstacles)
            {
                if (obstacle.Position.X == block.X && obstacle.Position.Y == block.Y)
                {
                    return true;
                }
            }
            return false;
        }

        private Obstacle CreateValidObstacle()
        {
            Obstacle obstacle = Obstacle.GenerateRandomObstacle();
            while(IsOnSnake(obstacle.Position))
            {
                obstacle = Obstacle.GenerateRandomObstacle();
            }

            return obstacle;
        }

        private void GenerateAllObstacles(int count)
        {
            _obstacles = new List<Obstacle>();
            for (int i = 0; i < count; i++)
            {
                _obstacles.Add(CreateValidObstacle());
            }
            _obstacles.ForEach(o => o.Draw());
        }

        private Apple CreateValidApple()
        {
            Apple apple = Apple.GenerateRandomApple();
            while (IsOnSnake(apple.Position) || IsOnObstacles(apple.Position))
            {
                apple = Apple.GenerateRandomApple();
            }

            return apple;
        }

        private ConsoleColor GetColorOfObstacleByCoordinates(int x, int y)
        {
            foreach(Obstacle obstacle in _obstacles)
            {
                if(obstacle.Position.X == x && obstacle.Position.Y == y)
                {
                    return obstacle.Color;
                }
            }

            return ConsoleColor.Black;
        }

        private void StartOver()
        {
            Console.CursorVisible = false;

            // draws initial snake
            _snake = new Snake();
            _snake.Draw();

            Direction direction = Direction.Right;

            //draws random obstacles
            GenerateAllObstacles(20);

            // draws first apple
            Apple apple = CreateValidApple();
            apple.Draw();

            //determines direction of snake
            ConsoleKeyInfo keyInput = Console.ReadKey(true);
            Block headOffset = new Block();
            bool isKeyPressed = false;
            while (keyInput.Key != ConsoleKey.Escape)
            {
                if (Console.KeyAvailable)
                {
                    isKeyPressed = true;
                    keyInput = Console.ReadKey(true);
                    switch (keyInput.Key)
                    {
                        case ConsoleKey.RightArrow:
                            headOffset = new Block(1, 0);
                            direction = Direction.Right;
                            break;
                        case ConsoleKey.LeftArrow:
                            headOffset = new Block(-1, 0);
                            direction = Direction.Left;
                            break;
                        case ConsoleKey.UpArrow:
                            headOffset = new Block(0, -1);
                            direction = Direction.Up;
                            break;
                        case ConsoleKey.DownArrow:
                            headOffset = new Block(0, 1);
                            direction = Direction.Down;
                            break;
                    }
                }
                else
                {
                    isKeyPressed = false;
                    switch (direction)
                    {
                        case Direction.Right: headOffset = new Block(1, 0); break;
                        case Direction.Left: headOffset = new Block(-1, 0); break;
                        case Direction.Up: headOffset = new Block(0, -1); break;
                        case Direction.Down: headOffset = new Block(0, 1); break;
                    }
                }
                var projectedHead = new Block(_snake.Head.X + headOffset.X, _snake.Head.Y + headOffset.Y);

                if (IsOnSnake(projectedHead))
                {
                    break;
                }

                if (IsOnObstacles(projectedHead))
                {
                    Console.BackgroundColor = GetColorOfObstacleByCoordinates(projectedHead.X, projectedHead.Y);
                    break;
                }
                // transforms snake
                try
                {
                    if (projectedHead.X == apple.Position.X && projectedHead.Y == apple.Position.Y)
                    {
                        projectedHead.Color = apple.Color;
                        _snake.Blocks.Add(projectedHead);
                        projectedHead.Draw(_snake.BodySymbol);
                        apple = CreateValidApple();
                        apple.Draw();
                    }
                    else
                    {
                        var first = _snake.Blocks.First();
                        first.Draw(' ');
                        _snake.Blocks.Remove(first);
                        projectedHead.Color = _snake.Head.Color;
                        _snake.Blocks.Add(projectedHead);
                        projectedHead.Draw(_snake.BodySymbol);
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    break;
                }

                if (!isKeyPressed)
                {
                    Thread.Sleep(300);
                }
            }
            EndCurrentGame();
        }

        public static void Start()
        {
            SetConsoleSettings();
            InitializeGame();
            GameEngine game = new GameEngine();
            game.StartOver();
        }
    }
}

