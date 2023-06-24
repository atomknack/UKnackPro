#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Linq;
using UnityEditor.Search;
using System.Collections.Generic;

namespace UKnack.Assorted;

[CustomEditor(typeof(NamedSingleton))]
public class NamedSingletonEditor : Editor
{
    private VisualElement warningNameLength;
    private Label warningHaveDuplicateNameInScene;
    private Label warningHaveDuplicateNameInAssets;
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new();

        // Create FloatFields for serialized properties.
        var singletonName = new TextField("Unique SingletonName") { bindingPath = "uniqueSingletonName" };
        singletonName.tooltip = "Unique name of in game singleton, for every name could be only one singleton with that name at any moment in game";
        root.Add(singletonName);

        warningNameLength = new VisualElement();
        warningNameLength.style.color = Color.red;
        //warningL.style.backgroundColor = Color.black;
        //warningNameLength.style.unityFontStyleAndWeight = FontStyle.Bold;
        Label warningNameLengthLabel = new("Singleton name Length should be > 0");
        warningNameLengthLabel.style.backgroundColor = Color.black;
        warningNameLength.Add(warningNameLengthLabel);
        warningNameLength.Add(new Label(" "));
        root.Add(warningNameLength);

        var warningScene = new VisualElement();
        warningScene.style.color = Color.red;
        //warningScene.style.unityFontStyleAndWeight = FontStyle.Bold;
        warningHaveDuplicateNameInScene = new("");
        warningScene.Add(warningHaveDuplicateNameInScene);
        root.Add(warningScene);


        var warningPrefabs = new VisualElement();
        warningPrefabs.style.color = Color.yellow;
        //warningPrefabs.style.unityFontStyleAndWeight = FontStyle.Bold;
        warningHaveDuplicateNameInAssets = new("");
        warningPrefabs.Add(warningHaveDuplicateNameInAssets);
        root.Add(warningPrefabs);

        // Determine whether to show the warnings at the start.
        CheckForWarnings(serializedObject);

        // Whenever any serialized property on this serialized object changes its value, call CheckForWarnings.
        root.TrackSerializedObjectValue(serializedObject, CheckForWarnings);

        return root;
    }

    void CheckForWarnings(SerializedObject serializedObject)
    {

        var namedSingleton = serializedObject.targetObject as NamedSingleton;
        string namedInstanceName = namedSingleton.GetSingletonInstanceName();
        if (namedInstanceName == null)
        {
            return;
        }

        warningNameLength.style.display = namedInstanceName.Length==0 ? DisplayStyle.Flex : DisplayStyle.None;

        var inScene = OthersWithSameNameInScene(namedSingleton);
        if (inScene.Length > 0)
        {
            string text = "Found same name in scene gameobjects:\n";
            text += string.Join("\n", inScene.Select(x => GameobjectPathOrInactive(x.gameObject)));
            warningHaveDuplicateNameInScene.text = text + "\n";
            warningHaveDuplicateNameInScene.style.display = DisplayStyle.Flex;
           
        }
        else 
            warningHaveDuplicateNameInScene.style.display = DisplayStyle.None;

        var inPrefabs = OthersWithSameNameInResources(namedSingleton);
        if (inPrefabs.Length > 0)
        {
            string text = "Found same name in Prefabs:\n";
            text += string.Join("\n", inPrefabs.Select(x => x.path));
            warningHaveDuplicateNameInAssets.text = text + "\n";
            warningHaveDuplicateNameInAssets.style.display = DisplayStyle.Flex;
            warningHaveDuplicateNameInAssets.style.display = DisplayStyle.Flex;

        }
        else
            warningHaveDuplicateNameInAssets.style.display = DisplayStyle.None;

        string GameobjectPathOrInactive(GameObject o)
        {
            var path = SearchUtils.GetHierarchyPath(o, true);
            return path == "" ? $"in inactive Gameobject {o.name}" : path;
        }
    }

    private static NamedSingleton[] OthersWithSameNameInScene(NamedSingleton named)
    {
        string nameOfNamed = named.GetSingletonInstanceName();
        var found = UnityEngine.Object.FindObjectsOfType<NamedSingleton>(includeInactive:true);
        //var found = Resources.FindObjectsOfTypeAll<T>();
        return found.Where(x=>x.GetSingletonInstanceName() == nameOfNamed && x!=named).ToArray();
    }

    private static (NamedSingleton script, string path)[] OthersWithSameNameInResources(NamedSingleton named)
    {
        string nameOfNamed = named.GetSingletonInstanceName();
        string path = "";
        if (PrefabUtility.IsPartOfAnyPrefab(named))
        {
            path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(named);
            //Debug.Log(path);
        }
        var found = GetAllPrefabs<NamedSingleton>();
        return found.Where(x => x.script.GetSingletonInstanceName() == nameOfNamed && x.path != path).ToArray();
    }

    private static (T script, string path)[] GetAllPrefabs<T>()
    {
        var guids = AssetDatabase.FindAssets("t:Prefab");
        var toSelect = new List<(T script, string path)>();
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var toCheck = AssetDatabase.LoadAllAssetsAtPath(path);
            foreach (var obj in toCheck)
            {
                var go = obj as GameObject;
                if (go == null)
                {
                    continue;
                }

                var comp = go.GetComponent<T>();
                if (comp != null)
                {
                    toSelect.Add((comp, path));
                }
                else
                {
                    var comps = go.GetComponentsInChildren<T>();
                    if (comps.Length > 0)
                    {
                        toSelect.Add((comps[0], path));
                    }
                }
            }
        }
        return toSelect.ToArray();
    }

    /* wrong, maybe will work fork for ScriptableObject
public static (T script, string path)[] GetAllInstances<T>() where T : MonoBehaviour //ScriptableObject
{
    Resources.FindObjectsOfTypeAll<T>();
    string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
    Debug.Log(guids.Length);
    (T script, string path)[] a = new (T script, string path)[guids.Length];
    for (int i = 0; i < guids.Length; i++)        
    {
        string path = AssetDatabase.GUIDToAssetPath(guids[i]);
        a[i] = (AssetDatabase.LoadAssetAtPath<T>(path), path);
    }

    return a;
}*/

}
#endif