using System;

namespace TRTS.Utils
{
    [Serializable]
    public struct Cooldown
    {
        public float Time;

        public float _currentTime;

        public bool Update()
        {
            _currentTime += UnityEngine.Time.deltaTime;
            return _currentTime > Time;
        }

        public bool IsReady()
        {
            return _currentTime > Time;
        }

        public void HardReset()
        {
            _currentTime = 0;
        }

        public void SoftReset()
        {
            _currentTime -= Time;
        }
    }
}