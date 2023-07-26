/*
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;


    [CustomPropertyDrawer(typeof(UnityEvent), true)]
    public class UnityEventDrawer2 : UnityEventDrawer
    {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        Debug.Log("OnGUI called");
    }

    protected override void OnSelectEvent(ReorderableList list)
        {
        //base.OnSelectEvent(list);
        Debug.Log("OnSelectEvent called");
        }

    protected override void OnReorderEvent(ReorderableList list)
    {
        //base.OnReorderEvent(list);
        Debug.Log("OnSelectEvent called");
    }

    protected override void DrawEvent(Rect rect, int index, bool isActive, bool isFocused)
    {
        Debug.Log("DrawEvent called");
    }
    }

#endif
*/