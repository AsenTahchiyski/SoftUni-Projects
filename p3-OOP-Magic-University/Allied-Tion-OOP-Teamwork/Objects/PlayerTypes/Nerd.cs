namespace AlliedTionOOP.Objects.PlayerTypes
{
    public class Nerd : Player
    {
        private const int NerdFocus = 140;
        private const int NerdEnergy = 60;
        private const int NerdSpeed = 3;

        public Nerd()
            : base(PlayerNerdSkin, NerdEnergy, NerdFocus, NerdSpeed)
        {
        }

        //private const int focus = 140;
        //private const int energy = 60;
        //public Nerd(Position position, string image, string name, int focus, int energy)
        //    : base(position, image, name, focus, energy)
        //{
        //    this.Focus = focus;
        //    this.Focus = focus;
        //}
        //public int Focus { get; set; }
        //public int energy { get; set; }
    }
}
