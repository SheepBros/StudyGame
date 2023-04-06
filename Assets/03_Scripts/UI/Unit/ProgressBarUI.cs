using UnityEngine;
using UnityEngine.UI;

namespace TRTS.Unit
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField]
        private UnitCanvas _unitCanvas;
        
        [SerializeField]
        private GameObject _uiObject;

        [SerializeField]
        private Image _progressBarImage;

        public bool IsActive()
        {
            return _uiObject.activeInHierarchy;
        }

        public void SetEnable(bool enable)
        {
            _unitCanvas.EnableCanvas(enable, gameObject);
            _uiObject.SetActive(enable);
        }

        public void SetProgress(float progress)
        {
            _progressBarImage.fillAmount = progress;
        }
    }
}