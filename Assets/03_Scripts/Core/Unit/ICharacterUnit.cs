using TRTS.BT;

namespace TRTS.Unit
{
    public interface ICharacterUnit : IUnit, IAbilityUnit, ITargetableUnit
    {
        void SetAI(BehaviourTree behaviourTree);
    }
}