using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlliedTionOOP.GUI.Menus
{
    public class MainMenu
    {
        public MainMenu(Texture2D background)
        {
            this.Background = background;
            this.Buttons = new List<Button>();
        }

        public List<Button> Buttons { get; set; }

        public Texture2D Background { get; set; }

        public void Draw(SpriteBatch spriteBatch, ContentManager content)
        {
            spriteBatch.Draw(this.Background, new Vector2(0, 0));
            foreach (var button in this.Buttons)
            {
                spriteBatch.Draw(button.ButtonTexture, new Vector2(button.ButtonTopLeftX, button.ButtonTopLeftY));
            }
            DrawElement(spriteBatch, content);
        }

        public void DrawElement(SpriteBatch spriteBatch, ContentManager content)
        {
            spriteBatch.Draw(content.Load<Texture2D>("GUI/MainMenuTextures/LogoGameGreen"), new Vector2(150, 250));
        }
    }
}