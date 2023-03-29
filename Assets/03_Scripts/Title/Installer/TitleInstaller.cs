using SB;
using TRTS.UI;
using UnityEngine;

namespace TRTS
{
    public class TitleInstaller : MonoInstaller
    {
        [SerializeField]
        private SceneUI _sceneUI;
        
        public override void InstallBindings()
        {
            _container.BindAllInterfacesFrom<SceneUI>(_sceneUI);
        }
    }
}