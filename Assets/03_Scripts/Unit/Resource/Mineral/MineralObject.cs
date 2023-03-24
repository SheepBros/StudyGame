namespace TRTS.Unit
{
    public class MineralObject : UnitObject
    {
        private MineralUnit _mineralUnit;

        public override void SetUp(IUnit unit)
        {
            base.SetUp(unit);
            
            _mineralUnit = unit as MineralUnit;
            _mineralUnit?.SetObject(this);
        }
    }
}