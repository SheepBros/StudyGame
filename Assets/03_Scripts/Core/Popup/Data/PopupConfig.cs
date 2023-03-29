using UnityEngine;

namespace TRTS.UI
{
    [CreateAssetMenu(fileName = "NewPopupConfig", menuName = "TRTS/PopupConfig")]
    public class PopupConfig : ScriptableObject
    {
        public PopupType PopupType => _popupType;

        public GameObject Prefab => _prefab;

        public CanvasType CanvasType => _canvasType;

        public bool DestroyAfterClose => _destroyAfterClose;
        
        [SerializeField]
        private PopupType _popupType;

        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private CanvasType _canvasType;

        [SerializeField]
        private bool _destroyAfterClose;
    }
}