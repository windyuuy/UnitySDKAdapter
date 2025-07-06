using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WebGLSupport
{
    public enum ContentType
    {
        Standard = 0,
        Autocorrected = 1,
        IntegerNumber = 2,
        DecimalNumber = 3,
        Alphanumeric = 4,
        Name = 5,
        EmailAddress = 6,
        Password = 7,
        Pin = 8,
        Custom = 9
    }
    public enum LineType
    {
        SingleLine = 0,
        MultiLineSubmit = 1,
        MultiLineNewline = 2
    }
    public interface IInputField
    {
        ContentType contentType { get; }
        LineType lineType { get; }
        int fontSize { get; }
        string text { get; set; }
        string placeholder { get; }
        int characterLimit { get; }
        int caretPosition { get; }
        bool isFocused { get; }
        int selectionFocusPosition { get;}
        int selectionAnchorPosition { get;}
        void SetSelection(int start, int end);
        bool ReadOnly { get; }
        bool OnFocusSelectAll { get; }
        bool EnableMobileSupport { get; }

        RectTransform RectTransform();
        Rect GetBounds();
        void ActivateInputField();
        void DeactivateInputField();
        void Rebuild();

        /// <summary>
        /// 不支持 IPointerDownHandler 生效则需要的自己实现
        /// </summary>
        /// <param name="eventHandler"></param>
        void RegisterTouchBegin(IPointerDownHandler eventHandler);
    }
}
