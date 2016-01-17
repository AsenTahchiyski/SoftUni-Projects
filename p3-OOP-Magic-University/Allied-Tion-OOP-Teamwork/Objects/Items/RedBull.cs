using AlliedTionOOP.Interfaces;

namespace AlliedTionOOP.Objects.Items
{
    public class RedBull : Item, IEnergyRestorable
    {
        private const int EnergyRestoreValue = 50;

        public RedBull(int topLeftX, int topLeftY)
            : base(RedBullImage, topLeftX, topLeftY)
        {
            this.EnergyRestore = EnergyRestoreValue;
        }

        public int EnergyRestore { get; private set; }

        //private int energyRestore = 40;

        //public RedBull(Position position, string image)
        //    : base(position, image)
        //{
        //    this.EnergyRestore = energyRestore;
        //}

        //public int EnergyRestore { get; private set; }
    }
}
