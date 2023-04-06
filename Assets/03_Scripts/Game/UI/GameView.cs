using SB;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TRTS.UI
{
    public class GameView : MonoBehaviour, IUIContentLifeCycle, IInjectable
    {
        [SerializeField]
        private Button _backButton;

        [SerializeField]
        private TextMeshProUGUI _mineralAmount;

        private IGameManager _gameManager;

        private IPopupManager _popupManager;

        [Inject]
        public void InjectBindings(IGameManager gameManager, IPopupManager popupManager)
        {
            _gameManager = gameManager;
            _popupManager = popupManager;
        }
        
        public void ViewAwake()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
            _gameManager.Minerals.OnValueChanged += OnMineralChanged;
        }

        public void ViewDestroy()
        {
        }
        
        private void OnBackButtonClicked()
        {
            _popupManager.Open(PopupType.Common, new CommonPopup.Data(MoveToTitleScene, null, "Go to Title?"));
        }

        private void MoveToTitleScene()
        {
            SceneManager.LoadScene("Title");
        }

        private void OnMineralChanged(int amount)
        {
            _mineralAmount.text = amount.ToString();
        }
    }
}