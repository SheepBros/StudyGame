using UnityEngine;

namespace TRTS.UI
{
    public interface ISceneUI
    {
        void RegisterUI(string uiName, GameObject uiObject);

        void UnregisterUI(string uiName);

        GameObject GetUIObject(string uiName);

        T GetUIComponent<T>(string uiName) where T : MonoBehaviour;
    }
}