using System.Collections.Generic;
using UnityEngine;

namespace TRTS.UI
{
    public class UIContentHandler : MonoBehaviour
    {
        public List<UIContent> UIContents => _uiContents;
        
        [SerializeField]
        private List<UIContent> _uiContents;

        private readonly List<IUIContentLifeCycle> _uiViewLifeCycles = new ();

        private void Awake()
        {
            CollectUIViews();
            AwakeUIViews();
        }

        private void OnDestroy()
        {
            DestroyUIViews();
        }

#if UNITY_EDITOR
        public void CollectAllUIContents()
        {
            _uiContents.Clear();

            GameObject[] rootGameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            for (int i = 0; i < rootGameObjects.Length; ++i)
            {
                SearchAndAddUIContents(rootGameObjects[i]);
            }
        }
#endif

        private void SearchAndAddUIContents(GameObject sceneObject)
        {
            UIContent uiContent = sceneObject.GetComponent<UIContent>();
            if (uiContent != null)
            {
                _uiContents.Add(uiContent);
            }

            Transform rootTransform = sceneObject.transform;
            int childCount = rootTransform.childCount;
            for (int i = 0; i < childCount; ++i)
            {
                Transform childTransform = rootTransform.GetChild(i);
                SearchAndAddUIContents(childTransform.gameObject);
            }
        }

        private void CollectUIViews()
        {
            for (int i = 0; i < _uiContents.Count; ++i)
            {
                UIContent uiContent = _uiContents[i];
                IUIContentLifeCycle[] lifeCycles = uiContent.GetComponents<IUIContentLifeCycle>();
                _uiViewLifeCycles.AddRange(lifeCycles);
            }
        }

        private void AwakeUIViews()
        {
            for (int i = 0; i < _uiViewLifeCycles.Count; ++i)
            {
                IUIContentLifeCycle lifeCycle = _uiViewLifeCycles[i];
                lifeCycle.ViewAwake();
            }
        }

        private void DestroyUIViews()
        {
            for (int i = 0; i < _uiViewLifeCycles.Count; ++i)
            {
                IUIContentLifeCycle lifeCycle = _uiViewLifeCycles[i];
                lifeCycle.ViewDestroy();
            }
        }
    }
}