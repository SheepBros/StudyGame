using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TRTS.UI
{
    [InitializeOnLoad]
    public class UIContentAutoCollectEditor
    {
        private const bool AutoRegister = true;

        static UIContentAutoCollectEditor()
        {
            if (!AutoRegister)
            {
                return;
            }
            
            if (EditorApplication.isCompiling || EditorApplication.isPaused || EditorApplication.isPlaying)
            {
                return;
            }

            EditorSceneManager.sceneSaving -= OnSceneSaving;
            EditorSceneManager.sceneSaving += OnSceneSaving;
        }

        private static void OnSceneSaving(Scene scene, string path)
        {
            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject rootObject in rootObjects)
            {
                UIContentHandler uiContentHandler = rootObject.GetComponentInChildren<UIContentHandler>(true);
                if (uiContentHandler != null)
                {
                    uiContentHandler.CollectAllUIContents();

                    if (PrefabUtility.IsPartOfAnyPrefab(uiContentHandler))
                    {
                        PrefabUtility.RecordPrefabInstancePropertyModifications(uiContentHandler);
                    }
                }
            }
        }
    }
}