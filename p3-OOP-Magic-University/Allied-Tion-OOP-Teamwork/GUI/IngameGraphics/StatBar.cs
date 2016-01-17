using AlliedTionOOP.Engine;
using AlliedTionOOP.Objects.Creatures;
using AlliedTionOOP.Objects.PlayerTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlliedTionOOP.GUI.IngameGraphics
{
    public static class StatBar
    {
        private const int FullBarWidth = 40;

        public static void DrawEnergyBar(Creature creature, int barOffSet, SpriteBatch spriteBatch, ContentManager content, Vector2 mapPosition)
        {
            spriteBatch.Draw(content.Load<Texture2D>("GUI/statbar"), new Vector2(creature.TopLeftX + mapPosition.X, creature.TopLeftY - barOffSet + mapPosition.Y));

            double percentageFull = ((double)creature.CurrentEnergy / creature.TotalEnergy) * FullBarWidth;

            for (int i = 0; i < percentageFull; i++)
            {
                spriteBatch.Draw(content.Load<Texture2D>("GUI/energy-filler"),
                    new Vector2((creature.TopLeftX + 1 + i) + mapPosition.X,
                                (creature.TopLeftY - (barOffSet - 1)) + mapPosition.Y));
            }
        }

        public static void DrawFocusBar(Creature creature, int barOffSet, SpriteBatch spriteBatch, ContentManager content, Vector2 mapPosition)
        {
            spriteBatch.Draw(content.Load<Texture2D>("GUI/statbar"), new Vector2(creature.TopLeftX + mapPosition.X, creature.TopLeftY - barOffSet + mapPosition.Y));

            double percentageFull = ((double)creature.CurrentFocus / creature.TotalFocus) * FullBarWidth;

            for (int i = 0; i < percentageFull; i++)
            {
                spriteBatch.Draw(content.Load<Texture2D>("GUI/focus-filler"),
                    new Vector2((creature.TopLeftX + 1 + i) + mapPosition.X,
                                (creature.TopLeftY - (barOffSet - 1)) + mapPosition.Y));
            }
        }

        public static void DrawExperienceBar(Player player, int barOffSet, SpriteBatch spriteBatch, ContentManager content, Vector2 mapPosition)
        {
            spriteBatch.Draw(content.Load<Texture2D>("GUI/statbar"), new Vector2(player.TopLeftX + mapPosition.X, player.TopLeftY - barOffSet + mapPosition.Y));

            double percentageFull = ((double)player.Experience / player.LevelUpExperience) * FullBarWidth;

            for (int i = 0; i < percentageFull; i++)
            {
                spriteBatch.Draw(content.Load<Texture2D>("GUI/experience-filler"),
                    new Vector2((player.TopLeftX + 1 + i) + mapPosition.X,
                                (player.TopLeftY - (barOffSet - 1)) + mapPosition.Y));
            }
        }
    }
}
