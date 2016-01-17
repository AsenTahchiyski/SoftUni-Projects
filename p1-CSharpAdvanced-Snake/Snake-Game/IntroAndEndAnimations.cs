using System;
using System.IO;
using System.Threading;

namespace IntroEndNamespace
{
    public class IntroAndEndAnimation
    {
        private static void PrintFile(string fileName)
        {
            string line;

            StreamReader file = new StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine("{0}{1}", new string(' ', Console.WindowWidth/2 - line.Length/2), line);
            }
            file.Close();
            Thread.Sleep(1000);
            //  Console.Clear();
        }

        public static void IntroAnimation()
        {
            Console.CursorVisible = false;
            int windowHeight = 33;
            int windowWidth = 70;
            Console.WindowHeight = windowHeight;
            Console.BufferHeight = Console.WindowHeight;
            Console.WindowWidth = windowWidth;
            Console.BufferWidth = Console.WindowWidth;
            PrintFile("../../IntroHungry.txt");
            PrintFile("../../IntroPython.txt");
            PrintFileSnake("../../IntroPythonAnimal.txt");
            Thread.Sleep(3000);
            Console.Clear();
        }

        public static void PrintFileSnake(string FileName)
        {
            //Console.Clear();
            string line;
            StreamReader file = new StreamReader(FileName);
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            file.Close();
        }

        public static void EndAnimation()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            PrintFile("../../BeBlessed.txt");
            Thread.Sleep(1500);
        }
    }
}
