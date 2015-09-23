using AlliedTionOOP.Interfaces;

namespace AlliedTionOOP.Objects.Items
{
    public class NakovBook : Item, IFocusIncreasable
    {
        private const int FocusIncreaseValue = 50;

        public NakovBook(int topLeftX, int topLeftY)
            : base(NakovBookImage, topLeftX, topLeftY)
        {
            this.FocusIncrease = FocusIncreaseValue;
        }

        public int FocusIncrease { get; private set; }

        //private int maxFocusBonus = 50;
        //private bool isCollected = false;

        //public NakovBook(Position position, string image)
        //    : base(position, image)
        //{
        //    this.MaxFocusBonus = maxFocusBonus;
        //    this.IsCollected = isCollected;
        //}

        //public int MaxFocusBonus { get; private set; }
        //public bool IsCollected { get; set; }
       
    }
}
