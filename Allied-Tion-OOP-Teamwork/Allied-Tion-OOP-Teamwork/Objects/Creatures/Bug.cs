namespace AlliedTionOOP.Objects.Creatures
{
    public class Bug : Creature
    {
        private const int BugEnergy = 30;
        private const int BugFocus = 30;
        private const int BugExpToGive = 80;

        public Bug(int topLeftX, int topLeftY)
            : base(BugImage, topLeftX, topLeftY, BugEnergy, BugFocus, BugExpToGive)
        {
        }


        //public Bug(Position position, string name)
        //    : base(position, Bygimage,name, BugFocus, BugEnergy)
        //{
        //}
    }
}
