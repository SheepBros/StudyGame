using System;
using UnityEngine;

namespace TRTS.BehaviourTree
{
    public class BehaviourTreeComponent : MonoBehaviour
    {
        [SerializeField]
        private Root _root;

        private void Update()
        {
            _root.PreUpdate();
            _root.Update();
            _root.PostUpdate();
        }
    }
}