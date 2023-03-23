namespace TRTS.Unit
{
    public class BaseCenterObject : UnitObject
    {
        private BaseCenterUnit _baseCenterUnit;

        public override void SetUp(IUnit unit)
        {
            base.SetUp(unit);
            _baseCenterUnit = unit as BaseCenterUnit;
            _baseCenterUnit?.SetObject(this);
        }
    }
}