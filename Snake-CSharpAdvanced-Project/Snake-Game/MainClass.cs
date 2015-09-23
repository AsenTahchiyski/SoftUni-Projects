using System;
using MenuNamespace;
using WelcomeIntroNamespace;

namespace MainNamespace
{
    internal class MainClass
    {
        public const int windowHeight = 30;
        public const int windowWidth = 70;

        private static void Main()
        {
            Console.CursorVisible = false;
            Console.Title = "SNAKE GAME by Team \"ALLIED TION\"";

            Console.ForegroundColor = ConsoleColor.Gray;
            IntroEndNamespace.IntroAndEndAnimation.IntroAnimation();

            Console.WindowHeight = windowHeight;
            Console.BufferHeight = Console.WindowHeight;
            Console.WindowWidth = windowWidth;
            Console.BufferWidth = Console.WindowWidth;

            WelcomeIntro.Intro(4);
            MenuClass.Menu();
        }
    }
}