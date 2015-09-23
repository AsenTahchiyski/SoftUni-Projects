namespace AlliedTionOOP.Objects.Creatures
{
    public class Exception : Creature
    {
        private const int ExcEnergy = 30;
        private const int ExcFocus = 30;
        private const int ExcExpToGive = 400;

        public Exception(int topLeftX, int topLeftY)
            : base(ExceptionImage, topLeftX, topLeftY, ExcEnergy, ExcFocus, ExcExpToGive)
        {
        }


        //internal const int ExcFocus = 30;
        //private const int ExcEnergy = 30;
        //private const char ExcSym = 'E';

        //public Exception(Position position, string name)
        //    : base(position, ExcSym, name, ExcFocus, ExcEnergy)
        //{
        //}

        
    }
}