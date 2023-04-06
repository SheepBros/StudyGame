using SB;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TRTS.UI
{
    public class SceneSelectScrollItem : MonoBehaviour, IInjectable
    {
        [SerializeField]
        private Button _selectButton;

        private IPopupManager _popupManager;
        
        private string _sceneName;

        [Inject]
        public void InjectBindings(IPopupManager popupManager)
        {
            _popupManager = popupManager;
        }

        private void Awake()
        {
            _selectButton.onClick.AddListener(OnSelectButtonClicked);
        }

        public void Initialize(string sceneName)
        {
            _sceneName = sceneName;
        }

        private void OnSelectButtonClicked()
        {
            _popupManager.Open(PopupType.Common, new CommonPopup.Data(ChangeScene, null, "Do you want to start the game?"));
        }

        private void ChangeScene()
        {
            SceneManager.LoadSceneAsync(_sceneName);
        }
    }
}