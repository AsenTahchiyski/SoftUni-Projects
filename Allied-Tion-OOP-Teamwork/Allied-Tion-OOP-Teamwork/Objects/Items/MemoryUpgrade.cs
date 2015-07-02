using AlliedTionOOP.Interfaces;

namespace AlliedTionOOP.Objects.Items
{
    public class MemoryUpgrade : Item, IFocusIncreasable
    {
        private const int FocusIncreaseValue = 20;

        public MemoryUpgrade(int topLeftX, int topLeftY)
            : base(MemoryUpgradeImage, topLeftX, topLeftY)
        {
            this.FocusIncrease = FocusIncreaseValue;
        }

        public int FocusIncrease { get; private set; }


        //private int maxFocusBonus = 20;
        //private bool isCollected = false;

        //public MemoryUpgrade(Position position, string image)
        //    : base(position, image)
        //{
        //    this.MaxFocusBonus = maxFocusBonus;
        //    this.IsCollected = isCollected;
        //}

        //public int MaxFocusBonus { get; private set; }
        //public bool IsCollected { get; set; }
        
    }
}
