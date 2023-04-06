using SB;
using TRTS.Unit;
using UnityEngine;

namespace TRTS
{
    public class PredefinedLevel : MonoBehaviour, IInitializable
    {
        [SerializeField]
        private UnitObject[] _units;

        private ObjectSpawnManager _objectSpawnManager;

        private IGameManager _gameManager;

        [Inject]
        public void InjectBindings(IGameManager gameManager, ObjectSpawnManager objectSpawnManager)
        {
            _gameManager = gameManager;
            _objectSpawnManager = objectSpawnManager;
        }

        public void Initialize()
        {
            foreach (UnitObject unitObject in _units)
            {
                if (unitObject is WorkerObject)
                {
                    IUnit unit = _gameManager.CreateWorker();
                    _objectSpawnManager.SetUpUnit(unitObject, unit);
                }
                
                if (unitObject is MineralObject)
                {
                    IUnit unit = _gameManager.CreateMineral();
                    _objectSpawnManager.SetUpUnit(unitObject, unit);
                }
                
                if (unitObject is BaseCenterObject)
                {
                    IUnit unit = _gameManager.CreateBaseCenter();
                    _objectSpawnManager.SetUpUnit(unitObject, unit);
                }
            }
        }
    }
}