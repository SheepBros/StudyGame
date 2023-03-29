using UnityEngine;

namespace TRTS.UI
{
    public interface IPopupObjectManager
    {
        GameObject GetPopup(PopupType popupType);

        void ReturnPopup(PopupType popupType);
    }
}