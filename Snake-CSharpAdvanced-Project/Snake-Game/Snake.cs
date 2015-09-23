using System;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Collections.Generic;
using HighscoreNamespace;

namespace SnakeNamespace
{
    internal struct Position
    {
        public int row;
        public int col;

        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }

    internal class Snake
    {
        public static void SnakeGame()
        {
            Console.CursorVisible = false;
            // play music
            using (SoundPlayer player = new SoundPlayer("../../ibasi_zmiqta.wav"))
            {
                player.PlayLooping();
            }
            //---------GAME-START-------------------------------------------------
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;
            int lastFoodTime = 0;
            int foodDisappearTime = 8000;
            int negativePoints = 0;
            Position[] directions = new Position[]
            {
                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0) // up
            };
            double sleepTime = 100;
            int direction = right;
            Random randomNumbersGenerator = new Random();
            Console.BufferHeight = Console.WindowHeight;
            lastFoodTime = Environment.TickCount;
            List<Position> obstacles = new List<Position>()
            {
                new Position(2, 42),
                new Position(14, 31),
                new Position(15, 60),
                new Position(19, 19),
                new Position(22, 14)
            };
            foreach (var obstacle in obstacles)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(obstacle.col, obstacle.row);
                Console.Write("#");
            }
            Queue<Position> snakeElements = new Queue<Position>();
            for (int i = 0; i <= 5; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }
            Position food;
            do
            {
                food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                    randomNumbersGenerator.Next(0, Console.WindowWidth));
            } while (snakeElements.Contains(food) || obstacles.Contains(food));
            Console.SetCursorPosition(food.col, food.row);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("@");
            foreach (Position position in snakeElements)
            {
                Console.SetCursorPosition(position.col, position.row);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("*");
            }
            while (true)
            {
                negativePoints++;
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    switch (userInput.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            if (direction != right) direction = left;
                            break;
                        case ConsoleKey.RightArrow:
                            if (direction != left) direction = right;
                            break;
                        case ConsoleKey.UpArrow:
                            if (direction != down) direction = up;
                            break;
                        case ConsoleKey.DownArrow:
                            if (direction != up) direction = down;
                            break;
                    }
                }
                Position snakeHead = snakeElements.Last();
                Position nextDirection = directions[direction];
                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row,
                    snakeHead.col + nextDirection.col);

                if (snakeNewHead.col < 0) snakeNewHead.col = Console.WindowWidth - 1;
                if (snakeNewHead.row < 0) snakeNewHead.row = Console.WindowHeight - 1;
                if (snakeNewHead.col >= Console.WindowWidth) snakeNewHead.col = 0;
                if (snakeNewHead.row >= Console.WindowHeight) snakeNewHead.row = 0;
                if (snakeElements.Contains(snakeNewHead) || obstacles.Contains(snakeHead))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    Console.WriteLine("{0}", new string('\n', Console.WindowHeight / 2 - 7));
                    Console.WriteLine("{1}{0}", "GAME OVER!", new string(' ', Console.WindowWidth / 2 - "GAME OVER!".Length / 2));
                    int userPoints = (snakeElements.Count - 6) * 100 - negativePoints;
                    userPoints = Math.Max(userPoints, 0);
                    Console.WriteLine("{2}{0}{1}", "Your points are: ", userPoints, new string(' ', Console.WindowWidth / 2 - "Your points are: ".Length / 2));
                    Console.Write("{1}{0}", "Enter your name: ", new string(' ', Console.WindowWidth / 2 - 10));
                    string name = Console.ReadLine();

                    IngameMenuNamespace.IngameMenuClass.IngameMenu();
                    Highscore.EnterHighScores(userPoints, name);
                    return;
                }
                Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("*");
                snakeElements.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Green;
                if (direction == right) Console.Write(">");
                if (direction == left) Console.Write("<");
                if (direction == up) Console.Write("^");
                if (direction == down) Console.Write("v");
                if (snakeNewHead.col == food.col && snakeNewHead.row == food.row)
                {
                    // feeding the snake
                    do
                    {
                        food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight - 1),
                            randomNumbersGenerator.Next(0, Console.WindowWidth - 1));
                    } while (snakeElements.Contains(food) || obstacles.Contains(food));
                    lastFoodTime = Environment.TickCount;
                    Console.SetCursorPosition(food.col, food.row);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("@");
                    sleepTime--;
                    Position obstacle;
                    do
                    {
                        obstacle = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth));
                    } while (snakeElements.Contains(obstacle) || obstacles.Contains(obstacle) ||
                             (food.col != obstacle.col && food.row != obstacle.row));
                    obstacles.Add(obstacle);
                    Console.SetCursorPosition(obstacle.col, obstacle.row);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("#");
                }
                else
                {
                    // just movement
                    Position last = snakeElements.Dequeue();
                    Console.SetCursorPosition(last.col, last.row);
                    Console.Write(" ");
                }
                if (Environment.TickCount - lastFoodTime >= foodDisappearTime)
                {
                    negativePoints += 50;
                    Console.SetCursorPosition(food.col, food.row);
                    Console.Write(" ");
                    do
                    {
                        food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth));
                    } while (snakeElements.Contains(food) || obstacles.Contains(food));
                    lastFoodTime = Environment.TickCount;
                }
                Console.SetCursorPosition(food.col, food.row);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("@");
                sleepTime -= 0.01;
                Thread.Sleep((int) sleepTime);
            }
        }
    }
}