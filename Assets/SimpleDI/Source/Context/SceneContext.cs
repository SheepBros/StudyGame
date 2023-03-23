﻿using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    /// <summary>
    /// SceneContext is a context in a certain scene.
    /// It's usually used to bind an instance that is used only in a scene.
    /// </summary>
    [ExecutionOrder(-9000)]
    public class SceneContext : Context
    {
        public override DiContainer Container { get; protected set; }

        [SerializeField]
        public List<MonoInstaller> _monoInstallers;

        private void Awake()
        {
            PersistentContext.Instance.MakeSureItsReady();
            Container = new DiContainer(PersistentContext.Instance.Container);

            Install();
        }

        protected override void InstallInternal()
        {
            foreach (MonoInstaller installer in _monoInstallers)
            {
                installer.Initialize(Container);
                installer.InstallBindings();
            }
        }

        protected override void InjectInternal()
        {
            InjectMonoBehaviours();
        }

        private void InjectMonoBehaviours()
        {
            GameObject[] rootGameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject gameObject in rootGameObjects)
            {
                Container.Inject(gameObject);
            }
        }
    }
}