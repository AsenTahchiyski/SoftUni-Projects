using Microsoft.Xna.Framework.Graphics;

namespace AlliedTionOOP.Objects.Items
{
    public class Item : Object
    {
        public Item(Texture2D image, int topLeftX, int topLeftY)
            : base(image, topLeftX,topLeftY)
        {
        }

        public override int GetHashCode()
        {
            return this.Image.GetHashCode() ^ this.TopLeftX.GetHashCode() ^ this.TopLeftY.GetHashCode();
        }
    }
}
