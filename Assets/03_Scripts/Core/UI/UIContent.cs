using SB;
using UnityEngine;

namespace TRTS.UI
{
    public class UIContent : MonoBehaviour, IInjectable
    {
        public string UIName => _uiName;
        
        [SerializeField]
        private string _uiName;

        [SerializeField]
        private bool _autoRegister;

        private ISceneUI _sceneUI;

        [Inject]
        public void InjectBindings(ISceneUI sceneUI)
        {
            _sceneUI = sceneUI;
        }

        private void Awake()
        {
            if (_autoRegister)
            {
                _sceneUI.RegisterUI(_uiName, gameObject);
            }
        }

        private void OnDestroy()
        {
            _sceneUI?.UnregisterUI(_uiName);
        }
    }
}