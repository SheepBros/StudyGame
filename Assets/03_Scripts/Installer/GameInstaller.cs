using SB;
using UnityEngine;

namespace TRTS
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private ObjectSpawnManager _objectSpawnManager;
        
        public override void InstallBindings()
        {
            Debug.LogError($"GameInstaller InstallBindings");
            
            _container.BindAllInterfaces<GameManager>();
            _container.BindAllInterfaces<GameEventManager>();
            _container.BindAllInterfacesFrom<ObjectSpawnManager>(_objectSpawnManager);
        }
    }
}