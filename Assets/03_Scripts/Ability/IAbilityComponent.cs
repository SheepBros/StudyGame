using TRTS.Unit;

namespace TRTS.Ability
{
    public interface IAbilityComponent
    {
        bool SetUp(UnitObject unitObject, IAbility ability);
    }
}