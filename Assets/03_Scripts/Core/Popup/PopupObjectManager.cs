using System.Collections.Generic;
using SB;
using TRTS.Util;
using UnityEngine;

namespace TRTS.UI
{
    public class PopupObjectManager : MonoBehaviour, IPopupObjectManager
    {
        [SerializeField]
        private PopupConfigList _popupConfigList;
        
        [SerializeField]
        private UICanvases _uiCanvases;

        private DiContainer _container;

        private Dictionary<PopupType, PopupConfig> _configTable = new();

        private Dictionary<PopupType, ObjectInfo> _spawnedPopups = new();
        
        [Inject]
        public void InjectBindings(DiContainer container)
        {
            _container = container;
        }

        private void Awake()
        {
            _configTable = _popupConfigList.ToDictionary();
        }

        public GameObject GetPopup(PopupType popupType)
        {
            if (!_spawnedPopups.TryGetValue(popupType, out ObjectInfo info))
            {
                info = InstantiatePopup(popupType);
            }

            return info.Instance;
        }

        public void ReturnPopup(PopupType popupType)
        {
            if (!_spawnedPopups.TryGetValue(popupType, out ObjectInfo info))
            {
                return;
            }

            if (info.DestroyAfterClose)
            {
                Destroy(info.Instance);
                _spawnedPopups.Remove(popupType);
            }
            else
            {
                info.Instance.SetActive(false);
            }
        }

        private ObjectInfo InstantiatePopup(PopupType popupType)
        {
            if (!_configTable.TryGetValue(popupType, out PopupConfig config))
            {
                Log.DebugError($"There is no a popup config. {popupType}");
                return null;
            }
            
            if (_spawnedPopups.TryGetValue(popupType, out ObjectInfo info))
            {
                Log.DebugWarning($"A popup is already instantiated. {popupType}");
                return info;
            }
            
            GameObject popupObject = Instantiate(config.Prefab, _uiCanvases.GetCanvasTransform(config.CanvasType));
            popupObject.SetActive(false);
            _container.Inject(popupObject);
            info = new ObjectInfo
            {
                Instance = popupObject,
                DestroyAfterClose = config.DestroyAfterClose
            };
            
            popupObject.SetActive(true);
            
            _spawnedPopups.Add(popupType, info);
            return info;
        }

        private class ObjectInfo
        {
            public GameObject Instance;

            public bool DestroyAfterClose;
        }
    }
}