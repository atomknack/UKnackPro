#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UKnack.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.EditorOnly
{

    public class BrokenScriptFinder : EditorWindow
    {
        private VisualElement _rootInsideScroller;

        private Foldout _brokenScriptsFoldout;
        private VisualElement _brokenScriptsContext;

        private readonly Color _defItemColor = new(0.6f, 0.6f, 1);

        //private VisualElement referencesPlaceholder;

        [MenuItem("Window/UKnack/Broken Script Finder")]
        public static void ShowMyEditor()
        {
            // This method is called when the user selects the menu item in the Editor
            EditorWindow wnd = GetWindow<BrokenScriptFinder>();
            wnd.titleContent = new GUIContent("Broken Script Finder");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            Button findBrokenInScene = new Button(OnFindBrokenInScene);
            findBrokenInScene.text = "Find Broken In Scene";
            root.Add(findBrokenInScene);
            Button findBrokenInAssets = new Button(OnFindBrokenInAssets);
            findBrokenInAssets.text = "Find Broken In Assets";
            root.Add(findBrokenInAssets);

            ScrollView scroller = new ScrollView();
            _rootInsideScroller = new VisualElement();
            scroller.Add(_rootInsideScroller);
            root.Add(scroller);

            _brokenScriptsContext = new VisualElement();
            _brokenScriptsFoldout = new Foldout();
            _brokenScriptsFoldout.text = "Gameobjects with null reference behaviour:";
            _brokenScriptsFoldout.style.color = _defItemColor;
            _brokenScriptsFoldout.Add(_brokenScriptsContext);
            bool foldoutOpen = true;
            _brokenScriptsFoldout.value = foldoutOpen;

            _rootInsideScroller.Add(_brokenScriptsFoldout);
        }
        private void OnFindBrokenInScene()
        {
            ClearFound();
            GameObject[] inScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            int count = 0;
            foreach (var found in FindBrokenInSelected(inScene))
            {
                count++;
                WriteItem(_brokenScriptsContext, found);
            }
            if (count == 0)
                WriteItem(_brokenScriptsContext, "No objects with null behaviours found");
        }

        private void OnFindBrokenInAssets()
        {
            ClearFound();
            string[] guids = AssetDatabase.FindAssets("t:Prefab");

            int count = 0;

            List<string> brokenObjects = new();
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                brokenObjects.Clear();
                if (go != null)
                    brokenObjects.AddRange(FindBrokenIn(go));
                if (brokenObjects.Count > 0)
                {
                    count += brokenObjects.Count;

                    WriteItem(_brokenScriptsContext, $"{path} have {brokenObjects.Count} broken: ", Color.white);

                    int localCount = 0;
                    foreach (string found in brokenObjects)
                    {
                        WriteItem(_brokenScriptsContext, $"{localCount}) {found}");
                        localCount++;
                    }
                }
            }
            if (count == 0)
                WriteItem(_brokenScriptsContext, "No objects with null behaviours found");

        }

        private void WriteItem(VisualElement vE, string text, Color? color = null)
        {
            Label label = new Label(text);
            if (color != null)
                label.style.color = color.Value;
            vE.Add(label);
            Debug.Log(text);
        }

        private IEnumerable<string> FindBrokenInSelected(IEnumerable<GameObject> allSelected)
        {
            foreach (var go in allSelected)
                foreach (var goResult in FindBrokenIn(go))
                    yield return goResult;
        }

        private IEnumerable<string> FindBrokenIn(GameObject go)
        {
            if (go == null)
                yield break;
            if (GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go) == 0)
                yield break;
            if (IsGameObjectHasNullComponent(go))
                yield return CommonStatic.GetFullPath_Recursive(go);
            Transform selectedTransform = go.transform;
            if (selectedTransform == null)
                yield break;
            for (int i = 0; i < selectedTransform.childCount; ++i)
            {
                foreach (var childResult in FindBrokenIn(selectedTransform.GetChild(i).gameObject))
                    yield return childResult;
            }
        }

        private void ClearFound()
        {
            if (_brokenScriptsContext == null)
                return;
            _brokenScriptsContext.Clear();// RemoveAllChildren();
        }



        private static bool IsGameObjectHasNullComponent(GameObject g)
        {
            var components = g.GetComponents<Component>();
            for (var i = 0; i < components.Length; i++)
            {
                if (components[i] != null)
                    return true;
            }
            return false;
        }

    }
}
#endif