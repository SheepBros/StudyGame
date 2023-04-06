using System;
using SB;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TRTS.UI
{
    public class CommonPopup : MonoBehaviour, IPopup
    {
        [SerializeField]
        private TextMeshProUGUI _descriptionText;
        
        [SerializeField]
        private Button _okButton;

        [SerializeField]
        private Button _cancelButton;
        
        private IPopupManager _popupManager;

        private Action _yesButtonClickedCallback;

        private Action _cancelButtonClickedCallback;

        [Inject]
        public void InjectBindings(IPopupManager popupManager)
        {
            _popupManager = popupManager;
        }

        private void Awake()
        {
            _okButton.onClick.AddListener(OnYesButtonClicked);
            _cancelButton.onClick.AddListener(OnNoButtonClicked);
        }
        
        public void OnOpened(IPopupData data)
        {
            if (data == null)
            {
                return;
            }

            if (data is not Data popupData)
            {
                return;
            }

            if (popupData.OkButtonClickedCallback != null)
            {
                _yesButtonClickedCallback = popupData.OkButtonClickedCallback;
            }

            if (popupData.CancelButtonClickedCallback != null)
            {
                _cancelButtonClickedCallback = popupData.CancelButtonClickedCallback;
            }

            _descriptionText.text = popupData.DescriptionText;
        }

        public void OnClosed()
        {
            _yesButtonClickedCallback = null;
            _cancelButtonClickedCallback = null;
        }

        private void OnYesButtonClicked()
        {
            _yesButtonClickedCallback?.Invoke();
            Close();
        }

        private void OnNoButtonClicked()
        {
            _cancelButtonClickedCallback?.Invoke();
            Close();
        }

        private void Close()
        {
            _popupManager.Close(PopupType.Common);
        }

        public class Data : IPopupData
        {
            public Action OkButtonClickedCallback;

            public Action CancelButtonClickedCallback;

            public string DescriptionText;

            public Data(Action okButtonClickedCallback, Action cancelButtonClickedCallback, string descriptionText)
            {
                DescriptionText = descriptionText;
                OkButtonClickedCallback = okButtonClickedCallback;
                CancelButtonClickedCallback = cancelButtonClickedCallback;
            }
        }
    }
}