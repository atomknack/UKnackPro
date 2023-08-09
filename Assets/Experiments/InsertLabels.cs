using System.Collections;
using System.Collections.Generic;
using UKnack.Attributes;
using UnityEngine;
using UnityEngine.UIElements;

public class InsertLabels : MonoBehaviour
{
    [SerializeField]
    [ProvidedComponent]
    private UIDocument _document;

    [SerializeField]
    private string _whereInsert;

    [SerializeField]
    private string _oneField;

    [SerializeField]
    private string[] _arrayOfStringsToInsert;

    private void OnEnable()
    {
        _document = GetComponentInParent<UIDocument>();
        VisualElement where = _document.rootVisualElement.Q<VisualElement>(_whereInsert);
        where.Add(new Label(_oneField));
        foreach (string str in _arrayOfStringsToInsert)
            where.Add(new Label(str));
    }
}
