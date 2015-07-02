namespace AlliedTionOOP.Interfaces
{
    public interface IDestroyable
    {
        int TotalFocus { get; set; }

        int CurrentFocus { get; set; }

        int TotalEnergy { get; set; }

        int CurrentEnergy { get; set; }

        bool IsAlive { get; set; }
    }
}
