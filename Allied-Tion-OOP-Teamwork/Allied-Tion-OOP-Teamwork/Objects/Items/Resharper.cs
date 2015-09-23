using AlliedTionOOP.Interfaces;

namespace AlliedTionOOP.Objects.Items
{
    public class Resharper : Item, IEnergyIncreasable
    {
        private const int EnergyIncreaseValue = 50;

        public Resharper(int topLeftX, int topLeftY)
            : base(ResharperImage, topLeftX, topLeftY)
        {
            this.EnergyIncrease = EnergyIncreaseValue;
        }

        public int EnergyIncrease { get; private set; }

        //private int maxEnergyBonus = 50;
        //private bool isCollected = false;

        //public Resharper(Position position, string image)
        //    : base(position, image)
        //{
        //    this.MaxFocusBonus = maxEnergyBonus;
        //    this.IsCollected = isCollected;
        //}

        //public int MaxFocusBonus { get; private set; }
        //public bool IsCollected { get; set; }
        
    }
}
