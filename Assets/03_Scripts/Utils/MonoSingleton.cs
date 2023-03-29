using UnityEngine;

namespace TRTS
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(T)) as T;
                    
                    if (_isDetroyed)
                    {
                        return null;
                    }

                    if (_instance == null)
                    {
                        _instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    }
                    
                    _isInitialized = true;
                }

                return _instance;
            }
        }

        private static T _instance;

        private static bool _isInitialized;

        private static bool _isDetroyed;

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != this)
            {
                DestroyImmediate(this);
                return;
            }

            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;
        }

        protected void OnDestroy()
        {
            _isDetroyed = true;
            _instance = null;
        }
    }
}