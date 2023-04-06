using System.Collections.Generic;
using UnityEngine;

namespace TRTS.Unit
{
    public class UnitCanvas : MonoBehaviour
    {
        [SerializeField]
        private GameObject _unitCanvas;

        private readonly List<GameObject> _referenceList = new();
        
        public void EnableCanvas(bool enable, GameObject referenceObject)
        {
            bool containReference = _referenceList.Contains(referenceObject);
            if (enable && !containReference)
            {
                _referenceList.Add(referenceObject);
            }
            else if (!enable && containReference)
            {
                _referenceList.Remove(referenceObject);
            }
            
            _unitCanvas.SetActive(enable);
        }

        public bool IsActive()
        {
            return _unitCanvas.activeInHierarchy;
        }
    }
}