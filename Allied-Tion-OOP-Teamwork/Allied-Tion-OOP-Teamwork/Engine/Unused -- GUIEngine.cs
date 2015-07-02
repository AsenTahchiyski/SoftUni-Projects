using System;
using AlliedTionOOP.GUI.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlliedTionOOP.Engine
{
    public class Unused__GUIEngine
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        //private GameEngine engine;

        private MainMenu mainMenu;

        public Unused__GUIEngine()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        protected void Initialize()
        {
            //window settings
            this.graphics.PreferredBackBufferWidth = MainClass.WindowWidth;
            this.graphics.PreferredBackBufferHeight = MainClass.WindowHeight;

            this.Window.Title = MainClass.LauncherWindowTitle;

            this.Window.Position = new Point((MainClass.CurrentScreenWidth - MainClass.WindowWidth) / 2,
                (MainClass.CurrentScreenHeight - MainClass.WindowHeight) / 2);

            this.IsMouseVisible = true;
            
            //this.graphics.ToggleFullScreen();
            //this.Window.IsBorderless = true;

            base.Initialize();
        }

        protected void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            Button playButton = new Button("play", Content.Load<Texture2D>("GUI/MainMenuTextures/PlayBtn"), 700, 30);
            Button howToPlayButton = new Button("howToPlay", Content.Load<Texture2D>("GUI/MainMenuTextures/HowToPlay"), 700, 200);
            Button aboutButton = new Button("about", Content.Load<Texture2D>("GUI/MainMenuTextures/AboutBtn"), 700, 350);
            Button quitButton = new Button("quit", Content.Load<Texture2D>("GUI/MainMenuTextures/Quit"), 700, 500);

            mainMenu = new MainMenu(Content.Load<Texture2D>("GUI/MainMenuTextures/grunge-texture5"));

            playButton.Click += OnClick;
            howToPlayButton.Click += OnClick;
            aboutButton.Click += OnClick;
            quitButton.Click += OnClick;

            mainMenu.Buttons.Add(playButton);
            mainMenu.Buttons.Add(howToPlayButton);
            mainMenu.Buttons.Add(aboutButton);
            mainMenu.Buttons.Add(quitButton);
        }

        protected void Update(GameTime gameTime)
        {
            foreach (var button in mainMenu.Buttons)
            {
                if (button.ButtonRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    button.FireClick();
                }
            }

            base.Update(gameTime);
        }

        protected void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            this.mainMenu.Draw(spriteBatch);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void OnClick(object buttonClicked, EventArgs args)
        {
            Button currentClickedButton = buttonClicked as Button;
            switch (currentClickedButton.ButtonName)
            {
                case "play":
                    using (var engine = new GameEngine())
                    {
                        engine.Run();
                    }
                    break;
                case "howToPlay":
                    currentClickedButton.ButtonTopLeftY++;
                    break;
                case "about":
                    currentClickedButton.ButtonTopLeftX--;
                    break;
                case "quit":
                    this.Exit();
                    break;
            }
        }
    }
}
