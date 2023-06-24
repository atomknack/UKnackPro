#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.EditorOnly
{


    public class RelativeScriptsFinder : EditorWindow
    {
        private VisualElement _rootInsideScroller;

        private Foldout _parentScriptsFoldout;
        private VisualElement _parentScriptsContext;

        private ObjectField _searcheable;

        private Foldout _childScriptsFoldout;
        private VisualElement _childScriptsContext;

        private readonly Color _parItemColor = new(0.6f, 1f, 0.6f);
        private readonly Color _chiItemColor = new(0.6f, 0.6f, 1);

        //private VisualElement referencesPlaceholder;

        [MenuItem("Window/UKnack/Relative Scripts Finder")]
        public static void ShowMyEditor()
        {
            // This method is called when the user selects the menu item in the Editor
            EditorWindow wnd = GetWindow<RelativeScriptsFinder>();
            wnd.titleContent = new GUIContent("Script Relatives Finder");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;


            ScrollView scroller = new ScrollView();
            _rootInsideScroller = new VisualElement();
            scroller.Add(_rootInsideScroller);
            root.Add(scroller);

            _parentScriptsContext = new VisualElement();
            _parentScriptsFoldout = new Foldout();
            _parentScriptsFoldout.text = "Parent types:";
            _parentScriptsFoldout.style.color = _parItemColor;
            _parentScriptsFoldout.Add(_parentScriptsContext);
            bool foldoutOpen = true;
            _parentScriptsFoldout.value = foldoutOpen;

            _rootInsideScroller.Add(_parentScriptsFoldout);

            //_searcheable = new ObjectField();
            //_searcheable.objectType = typeof(MonoScript);
            _searcheable = new ObjectField { allowSceneObjects = false, objectType = typeof(MonoScript) };
            _searcheable.RegisterCallback<ChangeEvent<UnityEngine.Object>>(_ => ClearFound());
            _rootInsideScroller.Add(_searcheable);

            Button findIn = new Button(OnFindRelatives);
            findIn.text = "Find relatives";
            _rootInsideScroller.Add(findIn);

            _childScriptsContext = new VisualElement();
            _childScriptsFoldout = new Foldout();
            _childScriptsFoldout.text = "Child types:";
            _childScriptsFoldout.style.color = _chiItemColor;
            _childScriptsFoldout.Add(_childScriptsContext);
            _childScriptsFoldout.value = foldoutOpen;
            _rootInsideScroller.Add(_childScriptsFoldout);
        }

        private void OnFindRelatives()
        {
            MonoScript searheableMonoScript = (MonoScript)_searcheable.value;
            if (searheableMonoScript == null)
            {
                Debug.LogError("searcheable is empty or not Monoscript");
                //Debug.LogError("searcheable is empty or not Monoscript, if Picker throws Exception (because U***y) use drag and drop to assign Monoscript");
                //Picker probably not likes perfectly fine objectfield created like this: 
                //_searcheable = new ObjectField();
                //_searcheable.objectType = typeof(MonoScript);
                return;
            }
            Type searcheable = searheableMonoScript.GetClass();
            //Debug.Log($"{searcheable}");

            ClearFound();
            //string[] guids = AssetDatabase.FindAssets("t:MonoScript");
            //Debug.Log($"found guids count: {guids.Length}");
            int countParents = 0;
            int countChildren = 0;

            //List<Type> notRelated = new List<Type>();

            /*
            Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                           .SelectMany(t => t.GetTypes())
                           .Where(t => t.IsClass).ToArray();
            foreach (var found in types)
            */
            foreach (var found in AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes()))
            {
                /*
                var path = AssetDatabase.GUIDToAssetPath(guid);
                MonoScript ms = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                if (ms == null)
                {
                    Debug.Log($"{path} for {guid} was found, but MonoScript is null");
                    continue;
                }
                Type found = ms.GetClass();*/
                if (found == null)
                {
                    //Debug.Log($"{path} for {guid} was found, MonoScript looks fine, but GetClass returns null");
                    continue;
                }


                if (searcheable.IsSubclassOf(found))
                {
                    countParents++;
                    WriteItem(_parentScriptsContext, $"{found}");//, at {path}");
                }
                else
                if (found.IsSubclassOf(searcheable))
                {
                    countChildren++;
                    WriteItem(_childScriptsContext, $"{found}");//, at {path}");
                } //else
                  //notRelated.Add(found);

            }
            if (countChildren == 0)
                WriteItem(_childScriptsContext, "No types found");
            if (countParents == 0)
                WriteItem(_parentScriptsContext, "No types found");

            //Debug.Log(String.Join(", ", notRelated.Select(x=>x.ToString())));
        }

        private void WriteItem(VisualElement vE, string text, Color? color = null)
        {
            Label label = new Label(text);
            if (color != null)
                label.style.color = color.Value;
            vE.Add(label);
            //Debug.Log(text);
        }

        private void ClearFound()
        {
            if (_parentScriptsContext == null)
                return;
            if (_childScriptsContext == null)
                return;
            _parentScriptsContext.Clear();// RemoveAllChildren();
            _childScriptsContext.Clear();// RemoveAllChildren();
        }


    }

}
#endif