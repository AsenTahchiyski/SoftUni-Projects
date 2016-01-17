using AlliedTionOOP.Interfaces;

namespace AlliedTionOOP.Objects.Items
{
    public class ProcessorUpgrade : Item, IEnergyIncreasable
    {
        private const int EnergyIncreaseValue = 20;

        public ProcessorUpgrade(int topLeftX, int topLeftY)
            : base(ProcessorUpgradeImage, topLeftX, topLeftY)
        {
            this.EnergyIncrease = EnergyIncreaseValue;
        }

        public int EnergyIncrease { get; private set; }

        //private int maxEnergyBonus = 20;
        //private bool isCollected = false;

        //public ProcessorUpgrade(Position position, string image)
        //    : base(position, image)
        //{
        //    this.MaxFocusBonus = maxEnergyBonus;
        //    this.IsCollected = isCollected;
        //}

        //public int MaxFocusBonus { get; private set; }
        //public bool IsCollected { get; set; }

    }
}
