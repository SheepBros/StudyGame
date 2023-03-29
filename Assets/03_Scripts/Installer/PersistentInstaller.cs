using SB;
using TRTS.UI;
using UnityEngine;

namespace TRTS
{
    public class PersistentInstaller : MonoInstaller
    {
        [SerializeField]
        private PopupObjectManager _popupObjectManager;
        
        public override void InstallBindings()
        {
            _container.BindAllInterfacesFrom<PopupObjectManager>(_popupObjectManager);
            
            _container.BindAllInterfaces<GameEventManager>();
            _container.BindAllInterfaces<PopupManager>(args: _popupObjectManager);
        }
    }
}