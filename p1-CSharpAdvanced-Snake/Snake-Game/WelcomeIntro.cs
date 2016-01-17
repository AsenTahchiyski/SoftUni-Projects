using System;
using System.Threading;
using MainNamespace;

namespace WelcomeIntroNamespace
{
    internal class WelcomeIntro
    {
        public static string[] template =
        {
            "###############################################",
            "#                                             #",
            "#                   WELCOME                   #",
            "#                   TO THE                    #",
            "#      SNAKE GAME - Hungry Python Edition     #",
            "#                                             #",
            "###############################################",
            "              presented by \"Allied Tion\" team"
        };

        public static void Intro(int seconds)
        {
            var countSeconds = 0;


            while (countSeconds < seconds)
            {
                Console.Clear();
                switch (countSeconds)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

                PrintWelcome();

                Thread.Sleep(1000);
                countSeconds++;
            }
        }

        public static void PrintWelcome()
        {
            Console.SetCursorPosition(0, MainClass.windowHeight/10);
            for (var i = 0; i < template.Length; i++)
            {
                Console.WriteLine(template[i].PadLeft(template[i].Length/2 + MainClass.windowWidth/2));
            }
        }
    }
}