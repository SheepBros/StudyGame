using System;
using System.Collections.Generic;
using TRTS.Util;
using UnityEngine;

namespace TRTS.UI
{
    public class PopupManager : IPopupManager
    {
        private IPopupObjectManager _popupObjectManager;

        private readonly Dictionary<PopupType, PopupInstanceInfo> _popupList = new();

        public PopupManager(IPopupObjectManager popupObjectManager)
        {
            _popupObjectManager = popupObjectManager;
        }
        
        public void Open(PopupType popupType, IPopupData data = null)
        {
            if (!GetPopupInfo(popupType, out PopupInstanceInfo info))
            {
                return;
            }
            
            info.Instance.SetActive(true);
            if (info.AnimationComponent != null)
            {
                info.AnimationComponent.OpenAnimation(() =>
                {
                    info.Popup?.OnOpened(data);
                });
            }
            else
            {
                info.Popup?.OnOpened(data);
            }
        }

        public void Close(PopupType popupType)
        {
            if (!_popupList.TryGetValue(popupType, out PopupInstanceInfo info))
            {
                return;
            }

            if (info.AnimationComponent != null)
            {
                info.AnimationComponent.CloseAnimation(() =>
                {
                    info.Popup?.OnClosed();
                    _popupObjectManager.ReturnPopup(popupType);
                });
            }
            else
            {
                info.Popup?.OnClosed();
                _popupObjectManager.ReturnPopup(popupType);
            }

            _popupList.Remove(popupType);
        }

        public T GetActivePopup<T>(PopupType popupType) where T : MonoBehaviour, IPopup
        {
            if (!_popupList.TryGetValue(popupType, out PopupInstanceInfo info))
            {
                return null;
            }

            return info.Popup as T;
        }

        private bool GetPopupInfo(PopupType popupType, out PopupInstanceInfo info)
        {
            if (_popupList.TryGetValue(popupType, out info))
            {
                return true;
            }

            try
            {
                GameObject popupObject = _popupObjectManager.GetPopup(popupType);
                info = new PopupInstanceInfo
                {
                    Instance =  popupObject,
                    Transform = popupObject.transform as RectTransform,
                    Popup = popupObject.GetComponent<IPopup>(),
                    AnimationComponent = popupObject.GetComponent<PopupAnimation>(),
                };

                if (info.Popup == null)
                {
                    Log.DebugWarning($"A component doesn't have IPopup. PopupType: {popupType}");
                }
            }
            catch (Exception exception)
            {
                Log.DebugError(exception);
                return false;
            }
            
            _popupList.Add(popupType, info);
            return true;
        }
    }
}