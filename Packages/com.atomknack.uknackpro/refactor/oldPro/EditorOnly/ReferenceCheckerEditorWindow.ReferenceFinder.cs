#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UKnack.EditorOnly
{
    public partial class ReferenceCheckerEditorWindow
    {
        public static class ReferenceFinder
        {

            public static Component[] GetComponentsWithPossibleReferences(GameObject go)
            {
                var comps = go.GetComponents(typeof(Component)).Where(x => x is UnityEngine.Transform == false);//(x => x.GetType().ToString())));
                return comps.ToArray();
            }

            public static bool TryGetSomeKindOfReference(SerializedProperty prop, out UnityEngine.Object reference)
            {
                reference = prop.propertyType switch
                {
                    SerializedPropertyType.ObjectReference => prop.objectReferenceValue,
                    //SerializedPropertyType.ManagedReference => prop.managedReferenceValue,
                    SerializedPropertyType.ExposedReference => prop.exposedReferenceValue,
                    _ => null
                };
                return reference != null;
            }

            /*
            private static bool NotASpecial(SerializedProperty prop)
            {
                bool notASpecial = true;
                if (prop.type == "PPtr<MonoScript>" && prop.name == "m_Script")
                    notASpecial = false;
                if (prop.type == "PPtr<PrefabInstance>" && prop.name == "m_PrefabInstance")
                {
                    notASpecial = false;
                    //Debug.Log($"path:{prop.propertyPath} name:{prop.name} displayName:{prop.displayName} {prop.isInstantiatedPrefab} {prop.prefabOverride} type:{prop.type} isNull:{prop.objectReferenceValue == null} |end|");
                    //Debug.Log($"typeof: {prop.objectReferenceValue.GetType().ToString()}");
                }

                if (prop.type == "PPtr<EditorExtension>" && prop.name == "m_CorrespondingSourceObject")
                    notASpecial = false;
                return notASpecial;
            }

            public static string PropertyRefAsString(SerializedProperty prop)
            {
                string refType = prop.propertyType switch
                {
                    SerializedPropertyType.ObjectReference => "ObjRef",
                    SerializedPropertyType.ManagedReference => "ManagedRef",
                    SerializedPropertyType.ExposedReference => "ExposedRef",
                    _ => "notARef"
                };
                return refType + ": " + prop.propertyPath + " type:" + prop.type + " name:" + prop.name;
            }

            public static FoundReference[] GetReferences(GameObject go)
            {
                //Debug.Log(ReferenceFinder.ComponentsArrayToString(
                //    ReferenceFinder.GetComponentsWithPossibleReferences(go)));
                List<FoundReference> references = new();
                List<GameObject> childrenGameobjects = new List<GameObject>();

                if (go.transform.childCount > 0)
                {
                    references.Add(new(null, $"{go.name} has children", go));
                    for (int i = 0; i < go.transform.childCount; ++i)
                    {
                        childrenGameobjects.Add(go.transform.GetChild(i).gameObject);
                    }
                }
                else { references.Add(new(null, $"{go.name} has NO children", go)); };

                foreach (var comp in ReferenceFinder.GetComponentsWithPossibleReferences(go))
                {
                    ReferenceFinder.AddReferencesToList(comp, references);
                }
                //ReferenceFinder.AddReferencesToList(go, references);

                foreach (var childGO in childrenGameobjects)
                {
                    var childGetReferences = GetReferences(childGO);
                    foreach (var s in childGetReferences)
                        references.Add(s);
                }

                return references.ToArray();
            }

            public static void AddReferencesToList(UnityEngine.Component c, List<FoundReference> output)
            {
                var iterator = new SerializedObject(c).GetIterator();
                AddOnlyIfReference(iterator, output, c.gameObject);
                while (iterator.Next(true))
                {
                    AddOnlyIfReference(iterator, output, c.gameObject);
                }
            }

            private static void AddOnlyIfReference(SerializedProperty prop, List<FoundReference> output, GameObject wasIn)
            {
                if (TryGetSomeKindOfReference(prop, out var reference))
                {
                    if (NotASpecial(prop))
                        output.Add(new FoundReference(reference, PropertyRefAsString(prop), wasIn));
                }
            }

            public static string ComponentsArrayToString(Component[] comps) =>
                String.Join(", ", comps.Select(x => x.GetType().ToString()));

            public static void AddReferencesToList(UnityEngine.Object o, List<FoundReference> output, GameObject wasIn)
            {
                var iterator = new SerializedObject(o).GetIterator();
                AddOnlyIfReference(iterator, output, wasIn);
                while (iterator.Next(true))
                {
                    AddOnlyIfReference(iterator, output, wasIn);
                }
            }

            private static void RunForNonStandartComponentProp(SerializedProperty prop, Action act)
            {
                var path = prop.propertyPath;
                if (path != "m_CorrespondingSourceObject" &&
                    path != "m_PrefabInstance" &&
                    path != "m_PrefabAsset" &&
                    path != "m_GameObject" &&
                    path != "m_Script")
                    act();
            }

            public static FoundReference[] FilterOutNullReferences(FoundReference[] refs) =>
                refs.Where(x => x.reference != null).ToArray();
            public static FoundReference[] FilterOutType<T>(FoundReference[] refs) =>
                refs.Where(x => x.reference is T == false).ToArray();

            public static FoundReference[] ThrowOutChildren(FoundReference[] refs, GameObject maybeParent)
            {
                var parentTransform = maybeParent.transform;
                return refs.
                    Where(x => (x.reference is GameObject o) ?
                o.transform.IsChildOf(parentTransform) == false :
                true).
                Where(x => (x.reference is GameObject o) ?
                o.transform != parentTransform : true).ToArray();
            }
            public static FoundReference[] ReplaceComponentReferencesToGameobjects(FoundReference[] refs)
            {
                return refs.Select(x => (x.reference is Component comp) ?
                new FoundReference(comp.gameObject, x.referenceStringRepresentation, x.wasReferencecBy) :
                x).ToArray();
            }

            public static (FoundReference[] scriptableObjects, FoundReference[] notScriptableObjects)
                SortOutScriptableObjects(FoundReference[] refs)
            {
                return (refs.Where(x => x.reference is ScriptableObject).ToArray(),
                    refs.Where(x => x.reference is ScriptableObject == false).ToArray());
            }

            private static void AddOnlyIfReference(SerializedProperty prop, List<string> output)
            {
                if (prop.propertyType == SerializedPropertyType.ObjectReference ||
                    prop.propertyType == SerializedPropertyType.ManagedReference ||
                    prop.propertyType == SerializedPropertyType.ExposedReference)
                    output.Add(PropertyRefAsString(prop));
            }

            public static string GameobjectRefAsString(GameObject go) => go == null ? "null" : go.name;
            */
        }
    }
}
#endif