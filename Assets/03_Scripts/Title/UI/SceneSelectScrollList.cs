using SB;
using UnityEngine;
using UnityEngine.UI;

namespace TRTS.UI
{
    public class SceneSelectScrollList : MonoBehaviour, IInjectable
    {
        [SerializeField]
        private ScrollRect _scroll;
        
        [SerializeField]
        private GameObject _itemPrefab;

        [SerializeField]
        private string[] _sceneNames;

        private DiContainer _container;

        [Inject]
        public void InjectBindings(DiContainer container)
        {
            _container = container;
        }

        private void Start()
        {
            foreach (string sceneName in _sceneNames)
            {
                GameObject scrollItem = _container.InstantiatePrefab(_itemPrefab);
                Transform scrollItemTransform = scrollItem.transform;
                scrollItemTransform.SetParent(_scroll.content);
                scrollItemTransform.localScale = Vector3.one;
                
                SceneSelectScrollItem sceneSelectScrollItem = scrollItem.GetComponent<SceneSelectScrollItem>();
                sceneSelectScrollItem.Initialize(sceneName);
            }
        }
    }
}