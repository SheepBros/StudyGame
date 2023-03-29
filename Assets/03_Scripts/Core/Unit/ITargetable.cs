namespace TRTS.Unit
{
    public interface ITargetableUnit
    {
        IUnit Target { get; }

        void SetTarget(IUnit target);
    }
}