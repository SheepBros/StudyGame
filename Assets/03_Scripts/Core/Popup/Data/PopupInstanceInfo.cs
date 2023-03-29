using UnityEngine;

namespace TRTS.UI
{
    public class PopupInstanceInfo
    {
        public PopupType PopupType;
        
        public GameObject Instance;
            
        public RectTransform Transform;

        public IPopup Popup;

        public PopupAnimation AnimationComponent;
    }
}