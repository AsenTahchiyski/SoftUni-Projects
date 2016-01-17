using Microsoft.Xna.Framework.Graphics;

namespace AlliedTionOOP.Objects
{
    public abstract class Object : Engine.GameEngine
    {
        protected Object(Texture2D image, int topLeftX, int topLeftY)
        {
            this.Image = image;
            this.TopLeftX = topLeftX;
            this.TopLeftY = topLeftY;
        }

        public int TopLeftX { get; set; }

        public int TopLeftY { get; set; }

        public Texture2D Image { get; set; }
    }
}
