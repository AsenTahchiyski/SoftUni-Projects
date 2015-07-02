using System;
using AlliedTionOOP.MapNamespace;
using AlliedTionOOP.Objects.Creatures;
using AlliedTionOOP.Objects.PlayerTypes;
using Microsoft.Xna.Framework;

namespace AlliedTionOOP.Physics
{
    public static class DistanceCalculator
    {
        public static Creature GetClosestCreature(Map map, Player player, Vector2 mapPostion)
        {
            double closestCreatureDistance = Double.MaxValue;
            Creature closestCreature = null;

            foreach (var mapCreature in map.MapCreatures)
            {
                double currentDistance = GetDistanceBetweenObjects(player, mapCreature,mapPostion);
                if (currentDistance < closestCreatureDistance)
                {
                    closestCreature = mapCreature;
                    closestCreatureDistance = currentDistance;
                }
            }

            return closestCreature;
        }

        public static double GetDistanceBetweenObjects(Player player, Creature creature, Vector2 mapPosition)
        {
            Vector2 playerPoint = GetCenterOfRectangle(player);
            Vector2 creaturePoint = GetCenterOfRectangle(creature);

            double a = (double)((creaturePoint.X + mapPosition.X/2) - playerPoint.X);
            double b = (double)((creaturePoint.Y + mapPosition.Y/2) - playerPoint.Y);

            return Math.Sqrt(a * a + b * b);
        }

        private static Vector2 GetCenterOfRectangle(Creature creature)
        {
            float x = (creature.TopLeftX + creature.Image.Width) / 2;
            float y = (creature.TopLeftY + creature.Image.Height) / 2;

            return new Vector2(x, y);
        }

    }
}
