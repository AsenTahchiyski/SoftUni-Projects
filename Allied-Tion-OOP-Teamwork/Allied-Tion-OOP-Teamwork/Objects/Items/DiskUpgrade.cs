using AlliedTionOOP.Interfaces;

namespace AlliedTionOOP.Objects.Items
{
    public class DiskUpgrade : Item, IFocusIncreasable
    {
        private const int FocusIncreaseValue = 30;

        public DiskUpgrade(int topLeftX, int topLeftY)
            : base(DiskUpgradeImage, topLeftX, topLeftY)
        {
            this.FocusIncrease = FocusIncreaseValue;
        }

        public int FocusIncrease { get; private set; }


        //private int maxFocusBonus = 30;
        //private bool isCollected = false;

        //public DiskUpgrade(Position position, string image)
        //    : base(position, image)
        //{
        //    this.MaxFocusBonus = maxFocusBonus;
        //    this.IsCollected = isCollected;
        //}

        //public int MaxFocusBonus { get; private set; }
        //public bool IsCollected { get; set; }

        
    }
}
