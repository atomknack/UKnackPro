#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.EditorOnly
{


    public class ScriptReplacer : EditorWindow
    {
        private Label danger;
        private Label selectedType;
        private ObjectField selectedObject;
        private ObjectField replaceWithThisScript;
        private Button replace;

        [MenuItem("Window/UKnack/Script replacer (Dangerous!!!)")]
        public static void ShowMyEditor()
        {
            // This method is called when the user selects the menu item in the Editor
            EditorWindow wnd = GetWindow<ScriptReplacer>();
            wnd.titleContent = new GUIContent("Script replacer - most dangerous tool");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            danger = new Label("Very Dangerous!!! \nDrag&Drop\nChange works for same type:\n 1: monobeh for monobeh \n 2: scriptableObj for scriptableObj\nInto selected field goes object(NOT gameobject), into replace goes Monoscript\n!!!do backup beforehand !!!\nCause it could break whole project");
            danger.style.backgroundColor = new Color(0.4f, 0.3f, 0.1f);
            root.Add(danger);
            selectedType = new Label();
            root.Add(selectedType);
            root.Add(new Label("Selected:"));
            selectedObject = new ObjectField { allowSceneObjects = true }; //, objectType = typeof(UnityEngine.Object) 
            selectedObject.RegisterValueChangedCallback(SelectedObjectChanged);
            root.Add(selectedObject);
            root.Add(new Label("Replace source with:"));
            replaceWithThisScript = new ObjectField { allowSceneObjects = false, objectType = typeof(MonoScript) };
            replaceWithThisScript.RegisterValueChangedCallback(ReplaceWithChanged);
            root.Add(replaceWithThisScript);
            replace = new Button(AttemptReplaceScript);
            replace.text = "Try replace script";
            replace.SetEnabled(false);
            root.Add(replace);
        }

        private void ReplaceWithChanged(ChangeEvent<UnityEngine.Object> e)
        {
            var newValue = e.newValue;
            if (newValue == null)
                return;

            Debug.Log($"{newValue.name}, {newValue.GetType().Name}");

            MonoScript monoScript = (MonoScript)newValue;
            if (monoScript == null)
            {
                NotAccepted(replaceWithThisScript, e);
                return;
            }

            Type t = monoScript.GetClass();
            Debug.Log(t);
            if (t == null)
            {
                Debug.Log("It's wrong type of class you trying to replace with, maybe it unspecified generic, maybe abstract, maybe something else wrong with it");
                NotAccepted(replaceWithThisScript, e);
                return;
            }

            Debug.Log($"{t.Name} gt:{t.IsGenericType} gtd:{t.IsGenericTypeDefinition} gtp:{t.IsGenericTypeParameter} a:{t.IsAbstract} i:{t.IsInterface} s:{t.IsSealed}");


            if (t.IsAbstract)
            {
                Debug.Log("Trying to replace with abstract type can lead to unexpected errors");
                NotAccepted(replaceWithThisScript, e);
                return;
            }
            if (t.IsGenericType)
            {
                Debug.Log("Trying to replace with unspecified generic type can lead to unexpected errors");
                NotAccepted(replaceWithThisScript, e);
                return;
            }

            EnableDisableButton();
        }

        private void SelectedObjectChanged(ChangeEvent<UnityEngine.Object> e)
        {
            if (e.newValue == null)
            {
                NotAccepted(selectedObject, e);
                return;
            }
            Type t = e.newValue.GetType();
            Debug.Log(t);
            if (typeof(MonoBehaviour).IsAssignableFrom(t) || typeof(ScriptableObject).IsAssignableFrom(t))
            {
                EnableDisableButton();
                return;
            }
            Debug.Log($"Drag and drop only some Monobehaviour from Gameobject (Not gameobject itself) or ScriptableObject instance from project assets");
            NotAccepted(selectedObject, e);
            //Debug.Log($"Could not set {e.newValue.name} as reciever, Script can be changed only for concrete Gameobjects.Monobehavour or ScriptableObject");
            //selectedObject.value = null;
        }

        void NotAccepted(ObjectField field, ChangeEvent<UnityEngine.Object> e)
        {
            e.PreventDefault();
            field.SetValueWithoutNotify(null);
            EnableDisableButton();
            return;
        }

        private void EnableDisableButton()
        {
            if (selectedObject.value == null || replaceWithThisScript.value == null)
            {
                replace.SetEnabled(false);
                return;
            }

            Type selectedType = selectedObject.value.GetType();

            MonoScript monos = replaceWithThisScript.value as MonoScript;
            if (monos == null)
            {
                replace.SetEnabled(false);
                return;
            }

            Type replaceType = monos.GetClass();

            if (typeof(MonoBehaviour).IsAssignableFrom(selectedType) && typeof(MonoBehaviour).IsAssignableFrom(replaceType))
            {
                replace.SetEnabled(true);
                return;
            }
            if (typeof(ScriptableObject).IsAssignableFrom(selectedType) && typeof(ScriptableObject).IsAssignableFrom(replaceType))
            {
                replace.SetEnabled(true);
                return;
            }
            //if (selectedObject.value is Monosc)

            replace.SetEnabled(false);
        }

        private void AttemptReplaceScript()
        {
            if (rootVisualElement == null || selectedType == null)
                return;
            if (replaceWithThisScript == null || replaceWithThisScript.value == null)
                return;

            if (Changable(selectedObject.value))
            {
                var toBeChanged = selectedObject.value;
                SerializedObject source = new SerializedObject(toBeChanged);
                source.FindProperty("m_Script").objectReferenceValue = replaceWithThisScript.value;
                source.ApplyModifiedProperties();
                Debug.Log($"toBeChanged: {AssetDatabase.GetAssetPath(toBeChanged)} script was replaced to {replaceWithThisScript.value}");
                selectedObject.value = null;
            }
        }

        //private void OnFocus() => OnSelectionChange();

        private bool Changable(UnityEngine.Object obj) => obj is ScriptableObject || obj is MonoBehaviour;
        private bool TryChange(UnityEngine.Object obj)
        {
            if (Changable(obj))
            {
                selectedObject.value = obj;
                selectedType.text = $"BeforeReplacement type {obj.GetType().Name}";
                return true;
            }
            return false;
        }
        /*
        private void OnSelectionChange()
        {
            if (rootVisualElement == null || selectedType == null)
                return;
            var newSelection = Selection.activeObject;
            int activeId = Selection.activeInstanceID;
            Debug.Log(activeId);
            //Debug.Log(AssetDatabase.GetAssetPath(Selection.activeInstanceID));
            //Debug.Log(AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(Selection.activeInstanceID), typeof(System.Object)).GetType());

            ///// start of totally useless block that does nothing??????? 
            //if (newSelection == null)
            //    if (activeId > 0)
            //        newSelection = EditorUtility.InstanceIDToObject(activeId);

            //if (newSelection == null)
            //{
            //    string assetPath = AssetDatabase.GetAssetPath(activeId);
            //    if (string.IsNullOrWhiteSpace(assetPath) == false)
            //        newSelection = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            //}
            ///// end of totally useless block

            Debug.Log(newSelection.GetType());

            if (Changable(newSelection))
            {
                TryChange(newSelection);
            }


            if (Changable(selectedObject.value))
            {
                replace.SetEnabled(true);
            }
            else
            {
                replace.SetEnabled(false);
            }

        }
            */

    }

}
#endif