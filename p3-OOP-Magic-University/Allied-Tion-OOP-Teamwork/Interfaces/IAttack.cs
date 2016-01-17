using AlliedTionOOP.Objects.Creatures;

namespace AlliedTionOOP.Interfaces
{
    public interface IAttack
    {
        void Attack(Creature enemy);

        int CurrentEnergy { get; }
    }
}
