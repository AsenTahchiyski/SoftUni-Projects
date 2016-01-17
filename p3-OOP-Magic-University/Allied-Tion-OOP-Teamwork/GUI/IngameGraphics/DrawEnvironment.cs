using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlliedTionOOP.MapNamespace;
using AlliedTionOOP.Objects.PlayerTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlliedTionOOP.GUI.IngameGraphics
{
    public static class DrawEnvironment
    {
        public static void DrawPlayer(Player player,SpriteBatch spriteBatch, ContentManager content)
        {
            spriteBatch.Draw(player.Image, new Vector2(player.TopLeftX, player.TopLeftY)); // draw player

            StatBar.DrawEnergyBar(player, 10, spriteBatch, content, Vector2.Zero);
            StatBar.DrawFocusBar(player, 13, spriteBatch, content, Vector2.Zero);
            StatBar.DrawExperienceBar(player, 16, spriteBatch, content, Vector2.Zero);
        }

        public static void DrawMap(Map map, Vector2 mapPosition, SpriteBatch spriteBatch, ContentManager content)
        {
            spriteBatch.Draw(map.Background, mapPosition);

            foreach (var mapElement in map.MapElements)
            {
                spriteBatch.Draw(mapElement.Image, new Vector2(mapElement.TopLeftX + mapPosition.X, mapElement.TopLeftY + mapPosition.Y));
            }

            foreach (var mapCreature in map.MapCreatures)
            {
                if (mapCreature.IsAlive)
                {
                    spriteBatch.Draw(mapCreature.Image, new Vector2(mapCreature.TopLeftX + mapPosition.X, mapCreature.TopLeftY + mapPosition.Y));
                    StatBar.DrawEnergyBar(mapCreature, 10, spriteBatch, content, mapPosition);
                    StatBar.DrawFocusBar(mapCreature, 16, spriteBatch, content, mapPosition);
                }
            }

            foreach (var mapItem in map.MapItems)
            {
                spriteBatch.Draw(mapItem.Image, new Vector2(mapItem.TopLeftX + mapPosition.X, mapItem.TopLeftY + mapPosition.Y));
            }
        }
    }
}
