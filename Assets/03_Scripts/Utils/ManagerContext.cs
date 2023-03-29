using UnityEngine;

namespace TRTS
{
    public class ManagerContext : MonoBehaviour
    {
        private static GameObject _managersObject;

        private void Awake()
        {
            CheckManagers();
        }
        
        private void CheckManagers()
        {
            if (_managersObject != null)
            {
                return;
            }

            GameObject prefab = Resources.Load<GameObject>("Managers");
            _managersObject = Instantiate(prefab);
            DontDestroyOnLoad(_managersObject);
        }
    }
} 