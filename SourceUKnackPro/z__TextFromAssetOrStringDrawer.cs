//__not updated properly if TextAsset _asset set to none in editor  
/*
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack;

//[CustomPropertyDrawer(typeof(TextFromAssetOrString), true)]
public class __TextFromAssetOrStringDrawer : PropertyDrawer
{
    PropertyField AssetField;
    PropertyField StringField;
    private const string AssetPropertyName = "_asset";
    private const string StringPropertyName = "_string";

    VisualElement container;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        container = new VisualElement();
        var foldout = new Foldout();
        foldout.text = property.displayName;
        foldout.SetValueWithoutNotify(true);

        var assetProperty = property.FindPropertyRelative(AssetPropertyName);
        AssetField = new PropertyField(assetProperty);
        //AssetField.RegisterValueChangeCallback(ev => OnChangeProperty(ev.changedProperty));
        AssetField.TrackPropertyValue(property, OnChangeProperty);
        StringField = new PropertyField(property.FindPropertyRelative(StringPropertyName));

        foldout.Add(AssetField);
        foldout.Add(StringField);
        container.Add(foldout);

        OnChangeProperty(property);
        return container;
    }

    private void OnChangeProperty(SerializedProperty prop)
    {

        Debug.Log("OnChangeProperty called");
        var asset = prop.FindPropertyRelative(AssetPropertyName);

        if (asset.objectReferenceValue!= null)
        {
            StringField.SetEnabled(false);
            var stringProp = prop.FindPropertyRelative(StringPropertyName);
            string current = stringProp.stringValue;
            Debug.Log(current);
            if (string.IsNullOrEmpty(current) == false)
            {
                stringProp.stringValue = string.Empty;
                AssetField.MarkDirtyRepaint();
            }
        }
        else
        {
            StringField.SetEnabled(true);
            AssetField.MarkDirtyRepaint();
        }

    }
}

#endif
*/