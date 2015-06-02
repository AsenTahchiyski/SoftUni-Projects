using System;
using CreditsNamespace;
using HighscoreNamespace;
using MainNamespace;
using SnakeNamespace;
using WelcomeIntroNamespace;

namespace MenuNamespace
{
    internal class MenuClass
    {
        public static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            string[] menuItems =
            {
                "Play",
                "Highscore",
                "Credits",
                "Exit"
            };

            var menu = new Item[menuItems.Length];

            for (var i = 0; i < menuItems.Length; i++)
            {
                menu[i] = new Item(
                    MainClass.windowWidth/2 - menuItems[i].Length/2,
                    MainClass.windowHeight/2 + i*2,
                    menuItems[i],
                    i
                    );

                if (i == 0)
                {
                    menu[i].isSelected = true;
                }
                else
                {
                    menu[i].isSelected = false;
                }

                menu[i].DrawItem();

                Console.WriteLine();
            }
            var currentSelected = 0;
            while (true)
            {   
                Console.ForegroundColor=ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                WelcomeIntro.PrintWelcome();
                var pressedKey = Console.ReadKey();
                if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    if (currentSelected < menu.Length - 1)
                    {
                        menu[currentSelected].isSelected = false;
                        currentSelected++;
                        menu[currentSelected].isSelected = true;
                    }
                }

                else if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    if (currentSelected > 0)
                    {
                        menu[currentSelected].isSelected = false;
                        currentSelected--;
                        menu[currentSelected].isSelected = true;
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                WelcomeIntro.PrintWelcome();
                WelcomeIntro.Intro(0);
                for (var i = 0; i < menuItems.Length; i++)
                {
                    menu[i].DrawItem();
                    Console.WriteLine();
                }
                if (pressedKey.Key == ConsoleKey.Enter)
                {
                    if (currentSelected == 0)
                    {
                        Console.Clear();
                        Snake.SnakeGame();
                    }
                    else if (currentSelected == 1)
                    {
                        Console.Clear();
                        Highscore.ShowHighScores();
                    }
                    else if (currentSelected == 2)
                    {
                        Console.Clear();
                        Credits.PrintCredits();
                    }
                    else if (currentSelected == 3)
                    {
                        IntroEndNamespace.IntroAndEndAnimation.EndAnimation();
                        Environment.Exit(0);
                    }
                }
            }
        }
    }

    internal class Item
    {
        public int id;
        public bool isSelected;
        public string itemText;
        public int x;
        public int y;

        public Item(int x, int y, string itemText, int id)
        {
            this.x = x;
            this.y = y;
            this.itemText = itemText;
            this.id = id;
            isSelected = false;
        }

        public void DrawItem()
        {
            switch (isSelected)
            {
                case false:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case true:
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(x, y);
            Console.Write(itemText);
        }
    }
}