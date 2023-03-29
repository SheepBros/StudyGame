using SB;
using UnityEngine;
using UnityEngine.UI;

namespace TRTS.UI
{
    public class TitleView : MonoBehaviour, IUIContentLifeCycle, IInjectable
    {
        [SerializeField]
        private Button _startButton;

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
            _startButton.onClick.AddListener(OnStartButtonClicked);
        }

        public void ViewDestroy()
        {
        }
        
        private void OnStartButtonClicked()
        {
            _popupManager.Open(PopupType.Common, new CommonPopup.Data(MoveToLobbyView, null, "Go to Lobby?"));
        }

        private void MoveToLobbyView()
        {
            _sceneUI.GetUIObject("TitleView").SetActive(false);
            _sceneUI.GetUIObject("LobbyView").SetActive(true);
        }
    }
}