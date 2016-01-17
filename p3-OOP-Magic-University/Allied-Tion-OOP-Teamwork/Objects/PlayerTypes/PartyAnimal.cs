namespace AlliedTionOOP.Objects.PlayerTypes
{
    public class PartyAnimal : Player
    {
        private const int PartyFocus = 60;
        private const int PartyEnergy = 140;
        private const int PartySpeed = 3;

        public PartyAnimal()
            : base(PlayerPartySkin, PartyEnergy, PartyFocus, PartySpeed)
        {
        }
    }
}
