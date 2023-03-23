namespace TRTS.Unit
{
    public class WorkerObject : UnitObject
    {
        private WorkerUnit _workerUnit;

        public override void SetUp(IUnit unit)
        {
            base.SetUp(unit);
            
            _transform = transform;
            _workerUnit = Unit as WorkerUnit;
            _workerUnit?.SetObject(this);
        }
    }
}