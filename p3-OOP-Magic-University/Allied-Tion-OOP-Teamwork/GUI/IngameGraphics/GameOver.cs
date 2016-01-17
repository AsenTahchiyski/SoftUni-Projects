using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlliedTionOOP.GUI.IngameGraphics
{
    public static class GameOver
    {
        public static void DrawGameOverWin(SpriteBatch spriteBatch, ContentManager content)
        {
            spriteBatch.Draw(content.Load<Texture2D>("GUI/GameOver/GameOverWon"), Vector2.Zero);
        }

        public static void DrawGameOverLose(SpriteBatch spriteBatch, ContentManager content)
        {
            spriteBatch.Draw(content.Load<Texture2D>("GUI/GameOver/GameOver"), Vector2.Zero);
        }
    }
}
