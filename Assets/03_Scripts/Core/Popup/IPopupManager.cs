using UnityEngine;

namespace TRTS.UI
{
    public interface IPopupManager
    {
        void Open(PopupType popupType, IPopupData data = null);

        void Close(PopupType popupType);

        T GetActivePopup<T>(PopupType popupType) where T : MonoBehaviour, IPopup;
    }
}