using System.Collections.Generic;
using TRTS.Util;
using UnityEngine;

namespace TRTS.UI
{
    [CreateAssetMenu(fileName = "NewPopupConfigList", menuName = "TRTS/PopupConfigList")]
    public class PopupConfigList : ScriptableObject
    {
        public List<PopupConfig> ConfigList => _configList;

        [SerializeField]
        private List<PopupConfig> _configList;

        public Dictionary<PopupType, PopupConfig> ToDictionary()
        {
            Dictionary<PopupType, PopupConfig> dictionary = new Dictionary<PopupType, PopupConfig>();
            for (int i = 0; i < _configList.Count; ++i)
            {
                PopupConfig config = _configList[i];
                if (dictionary.ContainsKey(config.PopupType))
                {
                    Log.DebugError($"There is a duplicate popup config. {config.PopupType}");
                    continue;
                }
                
                dictionary.Add(_configList[i].PopupType, _configList[i]);
            }

            return dictionary;
        }
    }
}