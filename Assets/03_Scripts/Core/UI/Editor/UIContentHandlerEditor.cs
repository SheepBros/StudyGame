using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TRTS.UI
{
    [CustomEditor(typeof(UIContentHandler))]
    public class UIContentHandlerEditor : Editor
    {
        private UIContentHandler _uiContentHandler;
 
        private void OnEnable()
        {
            if (AssetDatabase.Contains(target))
            {
                _uiContentHandler = null;
            }
            else
            {
                _uiContentHandler = target as UIContentHandler;
            }
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_uiContentHandler == null)
            {
                return;
            }

            if (GUILayout.Button("Collect all UI contents"))
            {
                _uiContentHandler.CollectAllUIContents();
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }
        }
    }
}