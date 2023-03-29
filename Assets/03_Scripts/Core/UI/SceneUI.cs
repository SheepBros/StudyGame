using System;
using System.Collections.Generic;
using TRTS.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace TRTS.UI
{
    public class SceneUI : MonoBehaviour, ISceneUI
    {
        [FormerlySerializedAs("_uiContentCollector")]
        [SerializeField]
        private UIContentHandler _uiContentHandler;

        private Dictionary<string, GameObject> _uiTable = new ();

        private Dictionary<string, Dictionary<Type, MonoBehaviour>> _uiComponentTable = new ();

        private void Awake()
        {
            if (_uiContentHandler != null)
            {
                for (int i = 0; i < _uiContentHandler.UIContents.Count; ++i)
                {
                    UIContent uiContent = _uiContentHandler.UIContents[i];
                    RegisterUI(uiContent.UIName, uiContent.gameObject);
                }
            }
        }

        public void RegisterUI(string uiName, GameObject uiObject)
        {
            if (_uiTable.ContainsKey(uiName))
            {
                Log.DebugWarning($"{name}.SceneUI.RegisterUI: Duplicate register. {uiName}");
                return;
            }

            _uiTable[uiName] = uiObject;
        }

        public void UnregisterUI(string uiName)
        {
            if (!_uiTable.ContainsKey(uiName))
            {
                Log.DebugWarning($"{name}.SceneUI.UnregisterUI: No an object not registered. {uiName}");
                return;
            }

            _uiTable.Remove(uiName);

            if (_uiComponentTable.ContainsKey(uiName))
            {
                _uiComponentTable.Remove(uiName);
            }
        }

        public GameObject GetUIObject(string uiName)
        {
            _uiTable.TryGetValue(uiName, out GameObject uiObject);
            return uiObject;
        }

        public T GetUIComponent<T>(string uiName) where T : MonoBehaviour
        {
            Type type = typeof(T);
            if (_uiComponentTable.TryGetValue(uiName, out Dictionary<Type, MonoBehaviour> componentTable))
            {
                componentTable.TryGetValue(type, out MonoBehaviour monoBehaviour);
                return monoBehaviour as T;
            }
            
            _uiTable.TryGetValue(uiName, out GameObject uiObject);
            if (uiObject == null)
            {
                return null;
            }

            componentTable = new Dictionary<Type, MonoBehaviour>();
            _uiComponentTable.Add(uiName, componentTable);

            T component = uiObject.GetComponent<T>();
            componentTable[type] = component;
            return component;
        }
    }
}