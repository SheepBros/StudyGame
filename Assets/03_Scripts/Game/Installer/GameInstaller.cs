using SB;
using UnityEngine;

namespace TRTS
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private ObjectSpawnManager _objectSpawnManager;
        
        [SerializeField]
        private AbilityComponentManager _abilityComponentManager;

        [SerializeField]
        private PredefinedLevel _predefinedLevel;

        public override void InstallBindings()
        {
            _container.BindAllInterfaces<GameManager>();
            _container.BindAllInterfacesFromAndSelf<ObjectSpawnManager>(_objectSpawnManager);
            _container.BindAllInterfacesFromAndSelf<AbilityComponentManager>(_abilityComponentManager);
            _container.BindAllInterfacesFromAndSelf<PredefinedLevel>(_predefinedLevel);
        }
    }
}