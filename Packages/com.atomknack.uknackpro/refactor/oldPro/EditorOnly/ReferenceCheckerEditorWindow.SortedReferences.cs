#if UNITY_EDITOR
using System.Collections.Generic;
using UKnack.Common;
using UnityEditor;
using UnityEngine;

namespace UKnack.EditorOnly
{

    public partial class ReferenceCheckerEditorWindow
    {
        private class SortedReferences
        {
            private GameObject root;
            private Transform rootTransform;
            private List<string> _prefabs;
            private List<FoundReference> _gameobjects;
            private List<FoundReference> _scriptableObjects;
            private List<FoundReference> _materials;
            private List<FoundReference> _meshes;
            private List<FoundReference> _unsorted;
            public string[] Prefabs => _prefabs.ToArray();
            public RepresentativeReference[] GameObjects => RepresentativeReference.CreateArray(_gameobjects);
            public RepresentativeReference[] ScriptableObjects => RepresentativeReference.CreateArray(_scriptableObjects);
            public RepresentativeReference[] Materials => RepresentativeReference.CreateArray(_materials);


            public RepresentativeReference[] Meshes => RepresentativeReference.CreateArray(_meshes);




            public RepresentativeReference[] Unsorted => RepresentativeReference.CreateArray(_unsorted);

            public static SortedReferences Create(GameObject go)
            {
                SortedReferences result = new SortedReferences();
                if (go == null)
                    return result;
                result.root = go;
                result.rootTransform = go.transform;
                result.AddRecursive(go);
                return result;
            }

            private void AddRecursive(GameObject go)
            {
                for (int i = 0; i < go.transform.childCount; ++i)
                    AddRecursive(go.transform.GetChild(i).gameObject);
                AddPrefabOf(go);
                AddScriptsOf(go);
                //AddReferences(go,go);
            }

            private void AddScriptsOf(GameObject go)
            {
                var scripts = ReferenceFinder.GetComponentsWithPossibleReferences(go);
                foreach (var script in scripts)
                {
                    AddReferences(script, go);
                }
            }

            public void AddReferences(UnityEngine.Object o, GameObject wasIn)
            {
                if (o == null)
                    return;
                var iterator = new SerializedObject(o).GetIterator();
                AddIfReference(iterator, wasIn);
                while (iterator.Next(true))
                {
                    AddIfReference(iterator, wasIn);
                }

                void AddIfReference(SerializedProperty prop, GameObject wasIn)
                {
                    if (ReferenceFinder.TryGetSomeKindOfReference(prop, out var reference))
                    {
                        SortPropertyAndAddToCorrespondingList(prop, wasIn, reference);
                    }
                }

            }

            private void SortPropertyAndAddToCorrespondingList(SerializedProperty prop, GameObject wasIn, UnityEngine.Object reference)
            {
                string textRepr = prop.propertyPath + " type:" + prop.type + " name:" + prop.name;
                if (reference is ScriptableObject)
                {
                    textRepr = AssetDatabase.GetAssetPath(reference);
                    _scriptableObjects.Add(new FoundReference(reference, textRepr, wasIn));
                    return;
                }
                if (reference is GameObject go)
                {
                    //Debug.Log(textRepr + $" is prebab: {prop.isInstantiatedPrefab}");
                    AddGameObject(go, wasIn);
                    return;
                }
                if (reference is MonoBehaviour monoBehaviour)
                {
                    AddMonoBehaviour(monoBehaviour, wasIn);
                    return;
                }
                if (reference is Material)
                {
                    textRepr = AssetDatabase.GetAssetPath(reference);
                    _materials.Add(new FoundReference(reference, textRepr, wasIn));
                    return;
                }
                if (reference is Mesh)
                {
                    textRepr = AssetDatabase.GetAssetPath(reference);
                    _meshes.Add(new FoundReference(reference, textRepr, wasIn));
                    return;
                }
                //
                //
                //
                //
                //

                _unsorted.Add(new FoundReference(reference, $"({reference.name}, {reference.GetType()}, {reference.GetInstanceID()}) " + textRepr, wasIn));
                return;


                void AddMonoBehaviour(MonoBehaviour reference, GameObject wasIn)
                {
                    AddGameObject(reference.gameObject, wasIn);

                    AddPrefabOf(reference.gameObject);
                    if (IsPartOfAddedPrefab(reference.gameObject))
                        return;
                    if (reference.gameObject == wasIn)
                        return;
                    if (reference.transform.IsChildOf(rootTransform))
                        return;
                    _gameobjects.Add(new FoundReference(reference, CommonStatic.GetFullPath_Recursive(reference.gameObject) + "/MONOBEHAVIOUR:" + reference.GetType().FullName, wasIn));
                }

                void AddGameObject(GameObject reference, GameObject wasIn)
                {
                    //Debug.Log($"{reference.name} was in {wasIn.name}");
                    if (reference == wasIn)
                        return;
                    AddPrefabOf(reference);
                    if (IsPartOfAddedPrefab(reference.gameObject) && reference.scene != wasIn.scene)
                    {
                        //Debug.Log($"{reference.name}, {CommonStatic.GetFullPath_Recursive(reference)} in scene {reference.scene.name}, {}");
                        return;
                    }

                    //if (root == reference || reference.transform.IsChildOf(rootTransform))
                    //    return;
                    if (IsChild(reference))
                        return;
                    _gameobjects.Add(new FoundReference(reference, CommonStatic.GetFullPath_Recursive(reference), wasIn));
                }
            }

            private bool IsChild(GameObject go)
            {
                if (root == go)
                    return true;
                var transform = go.transform;
                //while (transform != null) 
                //{ 
                //    if(transform== rootTransform) 
                //        return true;
                if (transform.IsChildOf(rootTransform))
                    return true;
                //    transform = transform.parent;
                //}
                //Debug.Log(CommonStatic.GetFullPath_Recursive(go));
                //Debug.Log(go.scene.name);
                return false;
            }

            private static bool TryGetPrefabPath(GameObject go, out string path)
            {
                if (PrefabUtility.IsPartOfAnyPrefab(go))
                {
                    path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(go);
                    return true;
                }
                path = string.Empty;
                return false;
            }

            private bool IsPartOfAddedPrefab(GameObject go)
            {
                if (TryGetPrefabPath(go, out var path))
                {
                    foreach (var prefab in _prefabs)
                        if (path == prefab)
                            return true;
                }
                return false;
            }

            private void AddPrefabOf(GameObject go)
            {
                if (TryGetPrefabPath(go, out var path))
                    AddIfNotExists(_prefabs, path);
            }

            private SortedReferences()
            {
                _prefabs = new List<string>();
                _gameobjects = new List<FoundReference>();
                _scriptableObjects = new List<FoundReference>();
                _materials = new List<FoundReference>();
                _meshes = new List<FoundReference>();
                _unsorted = new List<FoundReference>();
            }

            public static bool AddIfNotExists<T>(List<T> list, T value)
            {
                if (!list.Contains(value))
                {
                    list.Add(value);
                    return true;
                }
                return false;
            }
        }
    }

}
#endif