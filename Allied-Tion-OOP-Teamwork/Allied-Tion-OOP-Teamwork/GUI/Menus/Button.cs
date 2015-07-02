using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlliedTionOOP.GUI.Menus
{
    public class Button
    {
        public event EventHandler Click;

        public Button(string buttonName, Texture2D buttonTexture, int buttonTopLeftX, int buttonTopLeftY)
        {
            this.ButtonName = buttonName;
            this.ButtonTexture = buttonTexture;
            this.ButtonTopLeftX = buttonTopLeftX;
            this.ButtonTopLeftY = buttonTopLeftY;
            this.ButtonRect = new Rectangle(ButtonTopLeftX, buttonTopLeftY, ButtonTexture.Width, ButtonTexture.Height);
        }

        public string ButtonName { get; set; }

        public Texture2D ButtonTexture { get; set; }

        public int ButtonTopLeftX { get; set; }

        public int ButtonTopLeftY { get; set; }

        public Rectangle ButtonRect { get; set; }
        
        public void FireClick()
        {
            if (this.Click != null)
            {
                this.Click(this, new EventArgs());
            }
        }
    }
}
