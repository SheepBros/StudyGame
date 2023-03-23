﻿using System.Collections.Generic;
using SB.Util;
using UnityEngine;

namespace SB
{
    /// <summary>
    /// PersistentContext is a context that remains persistent until the game ends.
    /// Usually, it's used to keep an instance that is used for the whole time.
    /// </summary>
    [ExecutionOrder(-9999)]
    public class PersistentContext : Context
    {
        private static PersistentContext _instance;
        public static PersistentContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    Instatiate();
                }

                return _instance;
            }
        }

        public override DiContainer Container { get; protected set; }

        [SerializeField]
        public List<MonoInstaller> _monoInstallers;

        private static void Instatiate()
        {
            GameObject prefab = Resources.Load<GameObject>("PersistentContext");
            if (prefab == null)
            {
                GameObject instance = new GameObject("PersistentContext");
                _instance = instance.AddComponent<PersistentContext>();
            }
            else
            {
                GameObject instance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                _instance = instance.GetComponent<PersistentContext>();
            }

            DontDestroyOnLoad(_instance);

            _instance.Container = new DiContainer();
            _instance.Install();
        }

        public void MakeSureItsReady() { }

        protected override void InstallInternal()
        {
            MonoLifeCycle lifeCycle = GetComponent<MonoLifeCycle>();
            Container.BindFrom<MonoLifeCycle>(lifeCycle);

            foreach (MonoInstaller installer in _monoInstallers)
            {
                installer.Initialize(Container);
                installer.InstallBindings();
            }

            InjectMonoBehaviours();
        }

        private void InjectMonoBehaviours()
        {
            // Search game objects under DontDestroyOnLoad scene.
            GameObject[] rootGameObjects = this.gameObject.scene.GetRootGameObjects();
            foreach (GameObject gameObject in rootGameObjects)
            {
                MonoBehaviour[] monoBehaviours = gameObject.GetComponentsInChildren<MonoBehaviour>();
                foreach (MonoBehaviour behaviour in monoBehaviours)
                {
                    InjectUtil.InjectWithContainer(Container, behaviour);
                }
            }
        }
    }
}