using SB;
using UnityEngine;

namespace TRTS.UI
{
    public class LobbyView : MonoBehaviour, IUIContentLifeCycle, IInjectable
    {
        private ISceneUI _sceneUI;

        private IPopupManager _popupManager;

        [Inject]
        public void InjectBindings(ISceneUI sceneUI, IPopupManager popupManager)
        {
            _sceneUI = sceneUI;
            _popupManager = popupManager;
        }
        
        public void ViewAwake()
        {
        }

        public void ViewDestroy()
        {
        }
    }
}