using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TRTS.UI
{
    public class ButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private GameObject _highlightObject;

        private void Awake()
        {
            _highlightObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _highlightObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _highlightObject.SetActive(false);
        }
    }
}