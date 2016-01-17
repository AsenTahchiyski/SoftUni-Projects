using System.Collections.Generic;
using AlliedTionOOP.Objects.Items;

namespace AlliedTionOOP.Interfaces
{
    public interface ICollect
    {
        IEnumerable<Item> Inventory { get; }

        void AddItemToInventory(Item item);
    }
}
