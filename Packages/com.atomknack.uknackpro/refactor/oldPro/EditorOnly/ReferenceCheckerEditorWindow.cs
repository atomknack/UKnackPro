#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.EditorOnly
{

    public partial class ReferenceCheckerEditorWindow : EditorWindow
    {
        private VisualElement selectionNotNull;
        private Label objectType;
        private Label objectName;
        private Label prefabStage;

        private Foldout prefabsFoldout;
        private VisualElement contextPrefabs;

        private Foldout scriptableObjectsFoldout;
        private VisualElement contextScriptableObjects;

        private Foldout gameobjectsFoldout;
        private VisualElement contextGameobjects;

        private Foldout materialsFoldout;
        private VisualElement contextMaterials;

        private Foldout meshesFoldout;
        private VisualElement contextMeshes;

        private Foldout unsortedFoldout;
        private VisualElement contextUnsorted;

        //private VisualElement referencesPlaceholder;

        [MenuItem("Window/UKnack/Object outside references")]
        public static void ShowMyEditor()
        {
            // This method is called when the user selects the menu item in the Editor
            EditorWindow wnd = GetWindow<ReferenceCheckerEditorWindow>();
            wnd.titleContent = new GUIContent("Outside references for selected object (gameObject, scriptableObject)");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            ScrollView scroller = new ScrollView();
            selectionNotNull = new VisualElement();
            scroller.Add(selectionNotNull);
            root.Add(scroller);

            objectName = new Label();
            selectionNotNull.Add(objectName);
            objectType = new Label();
            selectionNotNull.Add(objectType);

            prefabStage = new Label();
            selectionNotNull.Add(prefabStage);

            //referencesPlaceholder = new VisualElement();
            //selectionNotNull.Add(referencesPlaceholder);

            contextPrefabs = new VisualElement();
            prefabsFoldout = new Foldout();
            prefabsFoldout.text = "Prefabs";
            prefabsFoldout.style.color = new Color(0.5f, 0.5f, 1);
            prefabsFoldout.Add(contextPrefabs);
            selectionNotNull.Add(prefabsFoldout);

            (scriptableObjectsFoldout, contextScriptableObjects) = CreateListItem("ScriptableObjects", new Color(0.8f, 0.8f, 0), selectionNotNull);
            (gameobjectsFoldout, contextGameobjects) = CreateListItem("Gameobjects And Monobehaviours", new Color(0.3f, 1, 0.4f), selectionNotNull);
            (materialsFoldout, contextMaterials) = CreateListItem("Materials", new Color(0.9f, 0.3f, 0.8f), selectionNotNull);
            (meshesFoldout, contextMeshes) = CreateListItem("Meshes", new Color(0.4f, 0.9f, 0.8f), selectionNotNull);
            (unsortedFoldout, contextUnsorted) = CreateListItem("Unsorted", new Color(0.7f, 1, 0.8f), selectionNotNull);


            OnSelectionChange();
            (Foldout foldout, VisualElement context) CreateListItem(string name, Color color, VisualElement parent)
            {
                var context = new VisualElement();
                var foldout = new Foldout();
                foldout.value = false;
                foldout.text = name;
                foldout.style.color = color;
                foldout.Add(context);
                parent.Add(foldout);
                return (foldout, context);
            }
        }

        private void OnFocus() => OnSelectionChange();

        private void OnSelectionChange()
        {
            if (rootVisualElement == null || selectionNotNull == null)
                return;
            var selection = Selection.activeObject;
            if (selection == null)
            {
                selectionNotNull.style.display = DisplayStyle.None;
                return;
            }

            selectionNotNull.style.display = DisplayStyle.Flex;

            objectName.text = "Currently selected: " + selection.name;

            objectType.text = $"Selected object type: {selection.GetType().ToString()}";

            prefabStage.style.display = InPrefabStageMode(out string prefabStageRepresentation) ? DisplayStyle.Flex : DisplayStyle.None;
            prefabStage.text = prefabStageRepresentation;


            contextPrefabs.Clear();// RemoveAllChildren();
            contextScriptableObjects.Clear();// RemoveAllChildren();
            contextGameobjects.Clear();// RemoveAllChildren();
            contextMaterials.Clear();// RemoveAllChildren();
            contextMeshes.Clear();// RemoveAllChildren();
            contextUnsorted.Clear();// RemoveAllChildren();

            prefabsFoldout.style.display = DisplayStyle.None;
            scriptableObjectsFoldout.style.display = DisplayStyle.None;
            gameobjectsFoldout.style.display = DisplayStyle.None;
            materialsFoldout.style.display = DisplayStyle.None;
            meshesFoldout.style.display = DisplayStyle.None;
            unsortedFoldout.style.display = DisplayStyle.None;

            if (selection is GameObject go)
            {
                var sorted = SortedReferences.Create(go);

                var prefabs = sorted.Prefabs;
                foreach (var prefabPath in prefabs)
                {
                    var label = new Label($"{prefabPath}");
                    label.tooltip = "Prefab references are not tracked by this tool";
                    contextPrefabs.Add(label);
                }
                prefabsFoldout.style.display = prefabs.Length > 0 ? DisplayStyle.Flex : DisplayStyle.None;

                AddReferencesToContext(contextScriptableObjects, scriptableObjectsFoldout, sorted.ScriptableObjects);
                AddReferencesToContext(contextGameobjects, gameobjectsFoldout, sorted.GameObjects);
                AddReferencesToContext(contextMaterials, materialsFoldout, sorted.Materials);
                AddReferencesToContext(contextMeshes, meshesFoldout, sorted.Meshes);
                AddReferencesToContext(contextUnsorted, unsortedFoldout, sorted.Unsorted);
            }
        }

        private void AddReferencesToContext(VisualElement context, Foldout foldout, RepresentativeReference[] references)
        {
            foreach (var reference in references)
            {
                var label = new Label($"{reference.referencePath}");
                label.tooltip = "Was referenced by: " + string.Join(", ", reference.wasReferencedBy);
                label.RegisterCallback<ClickEvent>(ev => SetActiveContextOnClick(ev, reference.obj));
                context.Add(label);
            }
            if (foldout != null && references.Length > 0)
                foldout.style.display = DisplayStyle.Flex;
        }

        private void SetActiveContextOnClick(ClickEvent ev, UnityEngine.Object context)
        {
            if (ev.button == 0)
            {
                Selection.SetActiveObjectWithContext(context, Selection.activeObject);
                //if selected object incorrect check:       public static RepresentativeReference[] CreateArray(List<FoundReference> references)
            }
        }

        bool InPrefabStageMode(out string textRepresentation)
        {
            var stage = PrefabStageUtility.GetCurrentPrefabStage();

            textRepresentation = stage switch
            {
                null => "Not in prefab mode",
                var x when x.mode == PrefabStage.Mode.InContext => $"Prefab Mode: ({stage.assetPath}) in context",
                var x when x.mode == PrefabStage.Mode.InIsolation => $"Prefab Mode: ({stage.assetPath}) in isolation",
                _ => "Mode: prefab stage not Null, but unknown, probably bug"
            };
            return stage != null;

        }

        /*

        private void OnSelectionChange()
        {
            var selection = Selection.activeObject;
            if (selection == null)
            {
                selectionNotNull.style.display = DisplayStyle.None;
                return;
            }

            selectionNotNull.style.display = DisplayStyle.Flex;

            objectName.text = selection.name;

            UpdateObjectTypeLabel(selection.GetType().ToString());

            prefabStage.text = GetPrefabStageTextRepresentation();


            RemoveAllChildren(referencesPlaceholder);
            if (selection is GameObject go)
            {
                FoundReference[] refs = ReferenceFinder.GetReferences(go);
                (FoundReference[] scriptable, FoundReference[] notScriptable) =
                    ReferenceFinder.SortOutScriptableObjects(refs);

                VisualElement notScriptablePlaceholder = new VisualElement();
                notScriptablePlaceholder.style.color = new Color(1, 1, 0.8f);

                //notScriptable = ReferenceFinder.ThrowOutType<EditorExtension>(notScriptable, go);
                notScriptable = ReferenceFinder.ReplaceComponentReferencesToGameobjects(notScriptable);
                notScriptable = ReferenceFinder.ThrowOutChildren(notScriptable, go);
                notScriptable = ReferenceFinder.FilterOutNullReferences(notScriptable);

                foreach (var reference in notScriptable)
                {
                    if (reference.reference == null)
                    {
                        notScriptablePlaceholder.Add
                            (
                            new Label($"nullReference {reference.referenceStringRepresentation} WasIn: {ReferenceFinder.GameobjectRefAsString(reference.wasReferencecBy)}")
                            );

                    }
                    else
                    {
                        Debug.Log($"{reference.reference.GetType().ToString()} {reference.reference.name} {reference.referenceStringRepresentation}");
                        notScriptablePlaceholder.Add(
        new Label($"{(reference.reference is GameObject o ? ReferenceFinder.GetTransPath(o.transform) : "")} {reference.referenceStringRepresentation} WasIn: {ReferenceFinder.GameobjectRefAsString(reference.wasReferencecBy)}"));
                    }
                }
                referencesPlaceholder.Add(notScriptablePlaceholder);

                VisualElement scriptablePlaceholder = new VisualElement();
                scriptablePlaceholder.style.color = Color.yellow;
                foreach (var reference in scriptable)
                {
                    scriptablePlaceholder.Add(
    new Label($"{reference.referenceStringRepresentation} WasIn: {ReferenceFinder.GameobjectRefAsString(reference.wasReferencecBy)}"));
                }
                referencesPlaceholder.Add(scriptablePlaceholder);


            }

            if (PrefabUtility.IsPartOfAnyPrefab(selection))
            {
                prefabPath.text = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(selection);
                prefabSelectionSection.style.display = DisplayStyle.Flex;
                if (PrefabUtility.IsPartOfPrefabAsset(selection))
                    Debug.Log("part of asset");
            }
            else
            {
                prefabSelectionSection.style.display = DisplayStyle.None;
            }
        }
    */


    }

}

#endif