using System.Collections.Generic;
using UnityEngine;

namespace TRTS.UI
{
    public class UICanvases : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _overlayCanvas;

        [SerializeField]
        private RectTransform _cameraCanvas;

        private readonly Dictionary<CanvasType, RectTransform> _canvasTransforms = new();

        private RectTransform _defaultTransform;

        private void Awake()
        {
            _defaultTransform = _overlayCanvas;
            
            _canvasTransforms.Add(CanvasType.Overlay, _overlayCanvas);
            _canvasTransforms.Add(CanvasType.Camera, _cameraCanvas);
        }

        public RectTransform GetCanvasTransform(CanvasType canvasType)
        {
            if (!_canvasTransforms.TryGetValue(canvasType, out RectTransform canvasTransform))
            {
                return _defaultTransform;
            }

            return canvasTransform;
        }
    }
}