using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.UI
{
    public abstract class UIWindowBehaviour_WithText : UIWindowBehaviour
    {
        [SerializeField] private string _headerPlaceholderName;
        [SerializeField] private bool _hideHeaderOnEmptyText = false;
        [SerializeField] private string _longTextPlaceholderName;
        internal string _header = string.Empty;
        internal string _longText = string.Empty;

        protected override void OnLayoutCreatedAndReady(VisualElement layout)
        {
            if (NotEmpty(_header) && Empty(_headerPlaceholderName))
                throw new Exception($"There is text for header, but header placeholder name is empty");
            if (NotEmpty(_headerPlaceholderName)) 
            {
                if (layout.TryFindSomeKindOfTextStorage(_headerPlaceholderName).TryAssignTextWithoutNotification(_header).
                        TryHide(_hideHeaderOnEmptyText && Empty(_header)) == null)
                    throw new Exception($"There is no suitable placeholder with name: {_headerPlaceholderName} for header in layout");
            }
            if (NotEmpty(_longText) && Empty(_longTextPlaceholderName))
                throw new Exception($"There is text for long text, but long text placeholder name is empty");
            if (NotEmpty(_longTextPlaceholderName))
            {
                if (layout.TryFindSomeKindOfTextStorage(_longTextPlaceholderName).TryAssignTextWithoutNotification(_longText) == null)
                    throw new Exception($"There is no suitable placeholder with name: {_longTextPlaceholderName} for text in layout");
            }
        }

        private static bool Empty(string s) => string.IsNullOrEmpty(s);
        private static bool NotEmpty(string s) => ! Empty(s);

    }
}
