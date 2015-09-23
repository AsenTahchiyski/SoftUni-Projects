using System.Collections.Generic;
using System.Linq;
using AlliedTionOOP.GUI;
using AlliedTionOOP.GUI.IngameGraphics;
using AlliedTionOOP.Objects;
using AlliedTionOOP.Objects.Creatures;
using AlliedTionOOP.Objects.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlliedTionOOP.MapNamespace
{
    public class Map
    {
        public Map()
        {
            this.MapElements = new List<MapElement>();
            this.MapCreatures = new List<Creature>();
            this.MapItems = new List<Item>();
        }

        public List<MapElement> MapElements { get; private set; }

        public List<Creature> MapCreatures { get; private set; }

        public List<Item> MapItems { get; private set; }

        public Texture2D Background { get; set; }

        public void SetMapBackground(Texture2D mapBackground)
        {
            this.Background = mapBackground;
        }

        public void AddElement(MapElement mapElementToAdd)
        {
            this.MapElements.Add(mapElementToAdd);
        }

        public void AddCreature(Creature creatureToAdd)
        {
            this.MapCreatures.Add(creatureToAdd);
        }

        public void AddItem(Item itemToAdd)
        {
            this.MapItems.Add(itemToAdd);
        }

        public void RemoveMapCreatureByHashCode(int hashCodeOfCreatureToRemove)
        {
            this.MapCreatures.Remove(this.MapCreatures.Single(cr => cr.GetHashCode() == hashCodeOfCreatureToRemove));
        }

        public void RemoveMapItemByHashCode(int hashCodeOfItemToRemove)
        {
            this.MapItems.Remove(this.MapItems.Single(el => el.GetHashCode() == hashCodeOfItemToRemove));
        }
    }
}
