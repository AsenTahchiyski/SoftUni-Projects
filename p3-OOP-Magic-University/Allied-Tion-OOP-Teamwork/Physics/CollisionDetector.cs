using AlliedTionOOP.MapNamespace;
using AlliedTionOOP.Objects.PlayerTypes;
using Microsoft.Xna.Framework;

namespace AlliedTionOOP.Physics
{
    public static class CollisionDetector
    {
        public static bool HasCollisionWithObject(Player player, int targetPositionX, int targetPositionY, Map map, Vector2 mapPosition)
        {
            Rectangle playerRect = new Rectangle(targetPositionX, targetPositionY, player.Image.Width,
                player.Image.Height);

            foreach(var mapElement in map.MapElements)
            {
                var mapElementRect = new Rectangle(mapElement.TopLeftX + (int) mapPosition.X,
                                                   mapElement.TopLeftY + (int) mapPosition.Y, 
                                                   mapElement.Image.Width,
                                                   mapElement.Image.Height);

                if (playerRect.Intersects(mapElementRect))
                {
                    return true;
                }
            }

            foreach (var mapCreature in map.MapCreatures)
            {
                var mapCreatureRect = new Rectangle(mapCreature.TopLeftX + (int)mapPosition.X,
                                                   mapCreature.TopLeftY + (int)mapPosition.Y,
                                                   mapCreature.Image.Width,
                                                   mapCreature.Image.Height);

                if (playerRect.Intersects(mapCreatureRect))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasCollisionWithItem(Player player, Map map, Vector2 mapPosition, out int hashcodeOfCollidedItem)
        {
            Rectangle playerRect = new Rectangle(player.TopLeftX, player.TopLeftY, player.Image.Width, player.Image.Height);

            foreach (var mapItem in map.MapItems)
            {
                var mapItemRect = new Rectangle(mapItem.TopLeftX + (int) mapPosition.X,
                                                mapItem.TopLeftY + (int) mapPosition.Y, 
                                                mapItem.Image.Width,
                                                mapItem.Image.Height);

                if (playerRect.Intersects(mapItemRect))
                {
                    hashcodeOfCollidedItem = mapItem.GetHashCode();
                    return true;
                }
            }

            hashcodeOfCollidedItem = 0;
            return false;
        }

        public static bool HasCollisionWithEnemy(Player player, Map map, Vector2 mapPosition, out int hashcodeOfCollidedEnemy)
        {
            Rectangle playerRect = new Rectangle(player.TopLeftX, player.TopLeftY, player.Image.Width, player.Image.Height);

            foreach (var creature in map.MapCreatures)
            {
                bool intersects =
                    playerRect.Intersects(new Rectangle(creature.TopLeftX + (int)mapPosition.X, creature.TopLeftY + (int)mapPosition.Y, creature.Image.Width,
                        creature.Image.Height));
                if (intersects)
                {
                    hashcodeOfCollidedEnemy = creature.GetHashCode();
                    return true;
                }
            }

            hashcodeOfCollidedEnemy = 0;
            return false;
        }
    }
}
