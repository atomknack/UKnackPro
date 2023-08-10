using System.Collections;
using System.Collections.Generic;
using UKnack.Attributes;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UIElements;

public class TextDemonstrator : MonoBehaviour
{
    [System.Serializable]
    struct Article
    {
        public string title; 
        public TextAsset text;
    }

    [SerializeField]
    [ProvidedComponent]
    private VisualElement _documentRoot;

    [SerializeField]
    private string _headerId;
    
    private Label _header;

    [SerializeField]
    private string _textFieldId;

    private TextField _textField;

    [SerializeField]
    private string _buttonId;

    private Button _button;

    [SerializeField]
    private Article[] _articles;

    private int _current = 0;

    private bool _initialized = false;

    private void ButtonClicked()
    {
        _current++;
        if (_current >= _articles.Length)
            _current = 0;

        UpdateCurrentUI();
    }

    public void UpdateCurrentUI()
    {
        if (_initialized == false)
            return;

        var article = _articles[_current];
        _header.text = article.title;
        _textField.SetValueWithoutNotify(article.text.text);
    }

    private void OnEnable()
    {
        _documentRoot = GetComponentInParent<UIDocument>().rootVisualElement;

        _header = GetOrThrow<Label>(_documentRoot, _headerId);
        _textField = GetOrThrow<TextField>(_documentRoot, _textFieldId);
        _button = GetOrThrow<Button>(_documentRoot, _buttonId);
        if (_articles.Length == 0)
            throw new System.Exception("should be at least one article to work");

        _button.clicked += ButtonClicked;
        _initialized = true;
        UpdateCurrentUI();


    }
    private void OnDisable()
    {
        _button.clicked -= ButtonClicked;
        _initialized = false;
    }

    private static TElement GetOrThrow<TElement>(VisualElement from, string what) where TElement : VisualElement
    {
        if (string.IsNullOrWhiteSpace(what))
            throw new System.Exception($"acquaring element's id should not be empty");
        var result = from.Q<TElement>(what);
        if (result == null)
            throw new System.Exception($"cannot find {nameof(TElement)} with id {what} from: {from.name} of {from.GetType().Name}");
        return result;
    }
}
