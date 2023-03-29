using System;
using UnityEngine;

namespace TRTS.UI
{
    public class PopupAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private string _openTrigger;

        [SerializeField]
        private string _closeTrigger;

        private Action _openCallback;

        private Action _closeCallback;

        public void OpenAnimation(Action callback)
        {
            _openCallback = callback;
            _animator.SetTrigger(_openTrigger);
        }

        public void CloseAnimation(Action callback)
        {
            _closeCallback = callback;
            _animator.SetTrigger(_closeTrigger);
        }

        public void OnOpenFinished()
        {
            _openCallback?.Invoke();
        }

        public void OnCloseFinished()
        {
            _closeCallback?.Invoke();
        }
    }
}