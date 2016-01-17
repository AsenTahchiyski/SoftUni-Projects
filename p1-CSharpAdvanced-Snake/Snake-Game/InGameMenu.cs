using System;
using CreditsNamespace;
using HighscoreNamespace;
using MainNamespace;
using WelcomeIntroNamespace;
using SnakeNamespace;


namespace IngameMenuNamespace
{
    internal class IngameMenuClass
    {
        public static void IngameMenu()
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            string[] menuItems =
            {
                "Submit name and retry",
                "Submit name and return to main menu",
                "Exit Game"
            };

            var menu = new Item[menuItems.Length];

            for (var i = 0; i < menuItems.Length; i++)
            {
                menu[i] = new Item(
                    MainClass.windowWidth / 2 - menuItems[i].Length / 2,
                    MainClass.windowHeight / 2 + i * 2,
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
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
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
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;

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
                        MenuNamespace.MenuClass.Menu();
                    }
                    else if (currentSelected == 2)
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