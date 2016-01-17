namespace AlliedTionOOP.Objects.PlayerTypes
{
    public class NormalStudent : Player
    {
        private const int NormalFocus = 100;
        private const int NormalEnergy = 100;
        private const int NormalSpeed = 3;

        public NormalStudent()
            : base(PlayerNormalSkin, NormalEnergy, NormalFocus, NormalSpeed)
        {
        }


        //private const int focus = 100;
        //private const int energy = 100;
        //public NormalStudent(Position position, string image, string name, int focus, int energy)
        //    : base(position, image, name, focus, energy)
        //{
        //    this.Focus = focus;
        //    this.Focus = focus;
        //}
        //public int Focus { get; set; }
        //public int energy { get; set; }

    }
}
