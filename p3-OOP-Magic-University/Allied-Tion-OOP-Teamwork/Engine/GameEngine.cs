using System;
using System.Linq;
using System.Threading;
using AlliedTionOOP.GUI;
using AlliedTionOOP.GUI.IngameGraphics;
using AlliedTionOOP.GUI.Menus;
using AlliedTionOOP.MapNamespace;
using AlliedTionOOP.Objects.Creatures;
using AlliedTionOOP.Objects.Items;
using AlliedTionOOP.Objects.PlayerTypes;
using AlliedTionOOP.Physics;
using AlliedTionOOP.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlliedTionOOP.Engine
{
    public class GameEngine : Game
    {
        protected static Texture2D BugImage;
        protected static Texture2D ExceptionImage;
        protected static Texture2D ExamBossImage;

        protected static Texture2D PlayerNerdSkin;
        protected static Texture2D PlayerNormalSkin;
        protected static Texture2D PlayerPartySkin;

        protected static Texture2D ProcessorUpgradeImage;
        protected static Texture2D MemoryUpgradeImage;
        protected static Texture2D DiskUpgradeImage;
        protected static Texture2D NakovBookImage;
        protected static Texture2D ResharperImage;
        protected static Texture2D BeerImage;
        protected static Texture2D RedBullImage;

        private Button playButton;
        private Button howToPlayButton;
        private Button aboutButton;
        private Button quitButton;

        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //private SpriteFont spriteFont;
        //private string noItemMessage = "";
        //private SpriteFont noItemMessageFont;

        private MainMenu mainMenu;

        private Sound getItemSound;
        private Sound musicTheme;
        private Sound killEnemy;

        private Map map;
        private Player player;
        private Vector2 mapPosition;

        private Creature closestCreature;

        private bool isKeyDownBeer = false;
        private bool isKeyDownRedBull = false;
        private bool isKeyDownAttack = false;

        private bool isKeyDownEscape = false;

        private bool isInMainMenu = false;

        public GameEngine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //window settings
            this.graphics.PreferredBackBufferWidth = MainClass.WindowWidth;
            this.graphics.PreferredBackBufferHeight = MainClass.WindowHeight;

            this.Window.Title = MainClass.GameWindowTitle;

            this.Window.Position = new Point((MainClass.CurrentScreenWidth - MainClass.WindowWidth) / 2,
                (MainClass.CurrentScreenHeight - MainClass.WindowHeight) / 2);

            this.IsMouseVisible = true;

            //this.graphics.ToggleFullScreen();
            //this.Window.IsBorderless = true;

            this.LoadImages();

            this.mainMenu = new MainMenu(Content.Load<Texture2D>("GUI/MainMenuTextures/background"));

            // TODO: Add your initialization logic here

            this.getItemSound = new Sound(MainClass.GotItem);
            this.musicTheme = new Sound(MainClass.Music);
            this.killEnemy = new Sound(MainClass.KillEnemy);

            this.map = new Map();
            
            this.musicTheme.Play(true);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            this.playButton = new Button("play", Content.Load<Texture2D>("GUI/MainMenuTextures/play"), 700, 50);
            this.howToPlayButton = new Button("howToPlay", Content.Load<Texture2D>("GUI/MainMenuTextures/howToPlay"), 700, 200);
            this.aboutButton = new Button("about", Content.Load<Texture2D>("GUI/MainMenuTextures/about"), 700, 350);
            this.quitButton = new Button("quit", Content.Load<Texture2D>("GUI/MainMenuTextures/quit"), 700, 500);

            this.playButton.Click += OnClick;
            this.howToPlayButton.Click += OnClick;
            this.aboutButton.Click += OnClick;
            this.quitButton.Click += OnClick;

            this.mainMenu.Buttons.Add(playButton);
            this.mainMenu.Buttons.Add(howToPlayButton);
            this.mainMenu.Buttons.Add(aboutButton);
            this.mainMenu.Buttons.Add(quitButton);

            this.map.SetMapBackground(Content.Load<Texture2D>("MapElementsTextures/map"));
            MapFactory.LoadMapObjectsFromTextFile(map, MainClass.MapCoordinates, this.Content);

            this.mapPosition = new Vector2(0, 0);

            this.player = new NormalStudent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (this.isInMainMenu)
            {
                foreach (var button in mainMenu.Buttons)
                {
                    if (button.ButtonRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) &&
                        Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        button.FireClick();
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    isKeyDownEscape = true;
                }

                if (Keyboard.GetState().IsKeyUp(Keys.Escape) && isKeyDownEscape)
                {
                    isInMainMenu = false;

                    isKeyDownEscape = false;
                }
            }
            else
            {
                if (player.IsAlive && map.MapCreatures.Any(cr => cr is ExamBoss))
                {
                    //TODO: Player gets stuck with objects in some pixels?
                    this.CheckForPlayerMovementInput();

                    #region CheckForCollisionWithItem

                    int hashcodeOfCollidedItem;
                    bool hasCollisionWithItem = CollisionDetector.HasCollisionWithItem(player, map, mapPosition,
                        out hashcodeOfCollidedItem);

                    if (hasCollisionWithItem)
                    {
                        Item collidedItem = map.MapItems.Single(x => x.GetHashCode() == hashcodeOfCollidedItem);
                        player.AddItemToInventory(collidedItem);

                        this.map.RemoveMapItemByHashCode(hashcodeOfCollidedItem);

                        new Thread(() => getItemSound.Play()).Start();
                    }

                    #endregion

                    this.closestCreature = DistanceCalculator.GetClosestCreature(map, player, mapPosition);
                
                    this.CheckForPlayerAttack();
                    
                    this.CheckForItemShortcutPressed();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    this.isKeyDownEscape = true;
                }

                if (Keyboard.GetState().IsKeyUp(Keys.Escape) && isKeyDownEscape)
                {
                    this.isInMainMenu = true;

                    this.isKeyDownEscape = false;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (this.isInMainMenu)
            {
                GraphicsDevice.Clear(Color.Gray);
                spriteBatch.Begin();

                this.mainMenu.Draw(spriteBatch,Content);

                spriteBatch.End();
            }
            else if(!isInMainMenu)
            {
                GraphicsDevice.Clear(Color.Gray);
                spriteBatch.Begin();

                DrawEnvironment.DrawMap(this.map, this.mapPosition, this.spriteBatch, this.Content); // draw map with all its elements

                Target.DrawTarget(this.closestCreature, this.spriteBatch, this.Content, this.mapPosition);

                DrawEnvironment.DrawPlayer(this.player, this.spriteBatch, this.Content); // draw player with his stat bars

                InventoryBar.DrawInventory(this.player, this.spriteBatch, this.Content);
                InventoryBar.DrawPlayerLevel(this.player, this.spriteBatch, this.Content);

                if (!this.player.IsAlive)
                {
                    GameOver.DrawGameOverLose(this.spriteBatch, this.Content);
                }

                if (!this.map.MapCreatures.Any(cr => cr is ExamBoss))
                {
                    GameOver.DrawGameOverWin(this.spriteBatch, this.Content);
                }

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        private void LoadImages()
        {
            BugImage = this.Content.Load<Texture2D>("CharacterTextures/bug");
            ExceptionImage = this.Content.Load<Texture2D>("CharacterTextures/exception");
            ExamBossImage = this.Content.Load<Texture2D>("CharacterTextures/exam");

            PlayerNerdSkin = this.Content.Load<Texture2D>("CharacterTextures/wizzard");
            PlayerNormalSkin = this.Content.Load<Texture2D>("CharacterTextures/wizzard");
            PlayerPartySkin = this.Content.Load<Texture2D>("CharacterTextures/wizzard");

            ProcessorUpgradeImage = this.Content.Load<Texture2D>("ItemsTextures/cpu-x35");
            MemoryUpgradeImage = this.Content.Load<Texture2D>("ItemsTextures/ram");
            DiskUpgradeImage = this.Content.Load<Texture2D>("ItemsTextures/hdd");
            NakovBookImage = this.Content.Load<Texture2D>("ItemsTextures/book");
            ResharperImage = this.Content.Load<Texture2D>("ItemsTextures/RSharper");
            BeerImage = this.Content.Load<Texture2D>("ItemsTextures/beerx32");
            RedBullImage = this.Content.Load<Texture2D>("ItemsTextures/redbull");
        }

        public void CheckForPlayerMovementInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (player.TopLeftX < MainClass.WindowWidth / 2
                    || mapPosition.X + map.Background.Width < MainClass.WindowWidth)
                {
                    if (player.TopLeftX < MainClass.WindowWidth - player.Image.Width
                      && !CollisionDetector.HasCollisionWithObject(player, (player.TopLeftX + player.Speed.X), player.TopLeftY, map, mapPosition))
                    {
                        player.TopLeftX += player.Speed.X;
                    }
                }
                else
                {
                    mapPosition.X -= player.Speed.X;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (player.TopLeftX >= MainClass.WindowWidth / 2
                    || mapPosition.X >= map.Background.Bounds.Left)
                {
                    if (player.TopLeftX > 0
                        && !CollisionDetector.HasCollisionWithObject(player, (int)(player.TopLeftX - player.Speed.X), (int)player.TopLeftY, map, mapPosition))
                    {
                        player.TopLeftX -= player.Speed.X;
                    }
                }
                else
                {
                    mapPosition.X += player.Speed.X;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (player.TopLeftY < MainClass.WindowHeight / 2
                    || mapPosition.Y + map.Background.Height < MainClass.WindowHeight)
                {
                    if (player.TopLeftY < MainClass.WindowHeight - player.Image.Height
                        && !CollisionDetector.HasCollisionWithObject(player, (player.TopLeftX), (player.TopLeftY + player.Speed.Y), map, mapPosition))
                    {
                        player.TopLeftY += player.Speed.Y;
                    }
                }
                else
                {
                    mapPosition.Y -= player.Speed.Y;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (player.TopLeftY >= MainClass.WindowHeight / 2
                    || mapPosition.Y >= map.Background.Bounds.Top)
                {
                    if (player.TopLeftY > 0
                        && !CollisionDetector.HasCollisionWithObject(player, (int)(player.TopLeftX), (int)(player.TopLeftY - player.Speed.Y), map, mapPosition))
                    {
                        player.TopLeftY -= player.Speed.Y;
                    }
                }
                else
                {
                    mapPosition.Y += player.Speed.Y;
                }
            }
        }

        public void CheckForItemShortcutPressed()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                this.isKeyDownBeer = true;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Z) && isKeyDownBeer)
            {
                Beer beerToUse = player.Inventory.FirstOrDefault(b => b is Beer) as Beer;

                if (beerToUse != null)
                {
                    this.player.GetFocus(beerToUse);
                }

                this.isKeyDownBeer = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                isKeyDownRedBull = true;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.X) && isKeyDownRedBull)
            {
                RedBull redbullToUse = this.player.Inventory.FirstOrDefault(b => b is RedBull) as RedBull;

                if (redbullToUse != null)
                {
                    this.player.GetEnergy(redbullToUse);
                }

                this.isKeyDownRedBull = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                MemoryUpgrade memoryToUse = this.player.Inventory.FirstOrDefault(m => m is MemoryUpgrade) as MemoryUpgrade;

                if (memoryToUse != null)
                {
                    this.player.MemoryUpgrade(memoryToUse);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.V))
            {
                DiskUpgrade diskToUse = this.player.Inventory.FirstOrDefault(d => d is DiskUpgrade) as DiskUpgrade;

                if (diskToUse != null)
                {
                    this.player.DiskUpgrade(diskToUse);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {
                ProcessorUpgrade processorToUse = this.player.Inventory.FirstOrDefault(p => p is ProcessorUpgrade) as ProcessorUpgrade;

                if (processorToUse != null)
                {
                    this.player.ProcessorUpgrade(processorToUse);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.N))
            {
                NakovBook bookToUse = this.player.Inventory.FirstOrDefault(b => b is NakovBook) as NakovBook;

                if (bookToUse != null)
                {
                    this.player.NakovBook(bookToUse);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                Resharper resharperToUse = this.player.Inventory.FirstOrDefault(r => r is Resharper) as Resharper;
                if (resharperToUse != null)
                {
                    this.player.Resharper(resharperToUse);
                }
            }
        }

        private void CheckForPlayerAttack()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                this.isKeyDownAttack = true;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.R) && isKeyDownAttack)
            {
                if (closestCreature != null &&
                    DistanceCalculator.GetDistanceBetweenObjects(player, closestCreature, mapPosition) < 80)
                {
                    this.closestCreature.Attack(this.player);
                    this.player.Attack(this.closestCreature);

                    this.CheckIfCreatureIsAlive(this.closestCreature);
                }

                this.isKeyDownAttack = false;
            }
        }

        private void CheckIfCreatureIsAlive(Creature creature)
        {
            if (!creature.IsAlive)
            {
                new Thread(() => killEnemy.Play()).Start();

                int hashcodeOfKilledCreature = creature.GetHashCode();
                this.map.RemoveMapCreatureByHashCode(hashcodeOfKilledCreature);
            }
        }

        protected void OnClick(object buttonClicked, EventArgs args)
        {
            Button currentClickedButton = buttonClicked as Button;
            switch (currentClickedButton.ButtonName)
            {
                case "play":
                    isInMainMenu = false;
                    break;
                case "howToPlay":
                    currentClickedButton.ButtonTopLeftY++;
                    break;
                case "about":
                    currentClickedButton.ButtonTopLeftX--;
                    break;
                case "quit":
                    base.Exit();
                    break;
            }
        }
    }
}


