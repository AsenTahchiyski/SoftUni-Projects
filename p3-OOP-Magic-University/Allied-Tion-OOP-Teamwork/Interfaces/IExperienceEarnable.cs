namespace AlliedTionOOP.Interfaces
{
    public interface IExperienceEarnable // used for players. When we earn enough XP we level up.
    {
        int Experience { get; set; }

        int CurrentLevel { get; set; }

        void LevelUp();
    }
}
