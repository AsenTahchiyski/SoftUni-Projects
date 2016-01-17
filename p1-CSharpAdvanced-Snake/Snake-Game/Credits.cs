using System;
using System.IO;
using System.Security.Principal;
using MainNamespace;


namespace CreditsNamespace
{
    class Credits
    {
        public static void PrintCredits()
        {
            Console.WriteLine("\n\n{0}Credits:", new string(' ', ((Console.WindowWidth/2) - 8)));
            Console.WriteLine("{0}---------\n\n", new string(' ', ((Console.WindowWidth/2) - 8)));

            using (StreamReader reader = new StreamReader("../../Credits.txt"))
            {
                string currentLine = reader.ReadLine();
                while (currentLine != null && currentLine != "")
                {
                    Console.WriteLine("{1}{0}\n", currentLine, new string(' ', Console.WindowWidth/2 - 10));

                    currentLine = reader.ReadLine();
                }
            }
            Console.WriteLine();
            Console.Write(new string(' ',Console.WindowWidth/2 - 32));
            Console.BackgroundColor=ConsoleColor.DarkRed;
            Console.WriteLine("Return to main menu");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\n");
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
