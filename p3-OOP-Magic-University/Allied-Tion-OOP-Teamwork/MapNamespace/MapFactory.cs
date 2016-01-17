using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlliedTionOOP.Objects;
using AlliedTionOOP.Objects.Creatures;
using AlliedTionOOP.Objects.Items;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlliedTionOOP.MapNamespace
{
    public static class MapFactory
    {
        private static readonly List<string> Commands = new List<string>
        #region FillListWithCommands
        {
            "bug",
            "exception",
            "exam",
            "cpu",
            "ram",
            "hdd",
            "book",
            "rsharper",
            "beer",
            "redbull",
            "big-rock",
            "large-rock",
            "rocks",
            "skulls",
            "stump",
            "bush2",
            "tree",
            "double-bush",
            "bush"
        };
        #endregion

        private static ContentManager Content;
        private static Map Map;

        public static void LoadMapObjectsFromTextFile(Map map, string mapTextFilePath, ContentManager content)
        {
            Map = map;
            Content = content;
            using (var sr = new StreamReader(mapTextFilePath))
            {
                string currentLine = sr.ReadLine();
                string command = "";

                while (!sr.EndOfStream)
                {
                    if (Commands.Contains(currentLine))
                    {
                        command = currentLine;
                        currentLine = sr.ReadLine();
                    }
                    int positionX = int.Parse(currentLine.Split(' ')[0]);
                    int positionY = int.Parse(currentLine.Split(' ')[1]);

                    Execute(command, positionX, positionY);

                    currentLine = sr.ReadLine();
                }
            }
        }

        private static void Execute(string command, int positionX, int positionY)
        {
            #region CheckCommandCase
            switch (command)
            {
                case "bug":
                    Map.AddCreature(new Bug(positionX, positionY));
                    break;
                case "exception":
                    Map.AddCreature(new Exception(positionX, positionY));
                    break;
                case "exam":
                    Map.AddCreature(new ExamBoss(positionX, positionY));
                    break;
                case "cpu":
                    Map.AddItem(new ProcessorUpgrade(positionX, positionY));
                    break;
                case "ram":
                    Map.AddItem(new MemoryUpgrade(positionX, positionY));
                    break;
                case "hdd":
                    Map.AddItem(new DiskUpgrade(positionX, positionY));
                    break;
                case "book":
                    Map.AddItem(new NakovBook(positionX, positionY));
                    break;
                case "rsharper":
                    Map.AddItem(new Resharper(positionX, positionY));
                    break;
                case "beer":
                    Map.AddItem(new Beer(positionX, positionY));
                    break;
                case "redbull":
                    Map.AddItem(new RedBull(positionX, positionY));
                    break;
                case "big-rock":
                    Map.AddElement(new MapElement(Content.Load<Texture2D>("MapElementsTextures/big-rock"), positionX, positionY));
                    break;
                case "large-rock":
                    Map.AddElement(new MapElement(Content.Load<Texture2D>("MapElementsTextures/large-rock"), positionX, positionY));
                    break;
                case "rocks":
                    Map.AddElement(new MapElement(Content.Load<Texture2D>("MapElementsTextures/rocks"), positionX, positionY));
                    break;
                case "skulls":
                    Map.AddElement(new MapElement(Content.Load<Texture2D>("MapElementsTextures/skulls"), positionX, positionY));
                    break;
                case "stump":
                    Map.AddElement(new MapElement(Content.Load<Texture2D>("MapElementsTextures/stump"), positionX, positionY));
                    break;
                case "bush2":
                    Map.AddElement(new MapElement(Content.Load<Texture2D>("MapElementsTextures/bush2"), positionX, positionY));
                    break;
                case "tree":
                    Map.AddElement(new MapElement(Content.Load<Texture2D>("MapElementsTextures/tree"), positionX, positionY));
                    break;
                case "double-bush":
                    Map.AddElement(new MapElement(Content.Load<Texture2D>("MapElementsTextures/double-bush"), positionX, positionY));
                    break;
                case "bush":
                    Map.AddElement(new MapElement(Content.Load<Texture2D>("MapElementsTextures/bush"), positionX, positionY));
                    break;
            }
            #endregion
        }
    }
}
