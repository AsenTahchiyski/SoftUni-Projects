using AlliedTionOOP.Interfaces;

namespace AlliedTionOOP.Objects.Items
{
    public class Beer : Item, IFocusRestorable
    {
        private const int FocusRestoreValue = 40;

        public Beer(int topLeftX, int topLeftY)
            : base(BeerImage, topLeftX, topLeftY)
        {
            this.FocusRestore = FocusRestoreValue;
        }

        public int FocusRestore { get; private set; }

    }
}
