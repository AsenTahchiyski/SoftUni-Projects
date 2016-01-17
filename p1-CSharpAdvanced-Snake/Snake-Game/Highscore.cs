using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighscoreNamespace
{
    class Highscore
    {
        public static void EnterHighScores(int newScore, string name)
        {
            if (!File.Exists("../../highscores.txt"))
            {
                File.CreateText("../../highscores.txt");
            }
            File.AppendAllText("../../highscores.txt", newScore.ToString() + " " + name + "\n", System.Text.Encoding.ASCII);
        }

        public static void ShowHighScores()
        {
            List<KeyValuePair<int, string>> scores = new List<KeyValuePair<int, string>>();
            using (StreamReader reader = new StreamReader("../../highscores.txt"))
            {
                string currentLine = reader.ReadLine();
                while (currentLine != null && currentLine != "")
                {
                    string[] parameters = currentLine.Split(' ');
                    scores.Add(new KeyValuePair<int, string>(int.Parse(parameters[0]), parameters[1]));
                    scores.OrderBy(key => key.Key);
                    //scores.Add(int.Parse(parameters[0]), parameters[1]);
                    currentLine = reader.ReadLine();
                }
            }
            Console.Clear();
            Console.WriteLine("\n\n{0}Highest scores:", new string(' ', ((Console.WindowWidth / 2) - 8)));
            Console.WriteLine("{0}---------------\n", new string(' ', ((Console.WindowWidth / 2) - 8)));
            
            int counter = 1;
            foreach (var player in scores.OrderByDescending(x => x.Key))
            {
                Console.WriteLine("{3}{0}. {1} - {2}", counter, player.Value, player.Key,new string(' ' ,Console.WindowWidth/2-13));
                counter++;
                if (counter > 10) break;
            }
            Console.WriteLine();
            Console.Write(new string(' ', Console.WindowWidth / 2 - 32));
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Return to main menu");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
            IntroEndNamespace.IntroAndEndAnimation.PrintFileSnake("../../IntroPythonAnimal.txt");
            while (true)
            {
                var pressed = Console.ReadKey();
                if (pressed.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
            Console.Clear();
            MenuNamespace.MenuClass.Menu();
        }
    }
}
