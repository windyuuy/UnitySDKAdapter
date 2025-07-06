using FairyGUI;
using UnityEngine;
using UnityEngine.EventSystems;
using WebGLSupport.Detail;

namespace WebGLSupport
{
    public class WrappedFGUIInputField:IInputField
    {
        private InputTextField input;
        // RebuildChecker checker;
        // private Vector2 cachedDesignSize;

        public WrappedFGUIInputField(DisplayObjectInfo obj)
        {
            Debug.Log($"Add-WrappedFGUIInputField-for-{obj.name}");
            this.input = obj.displayObject as InputTextField;
            Debug.Log($"Add-WrappedFGUIInputField-for-{obj.name}, {input!=null}, done");
            // var scaler = input.stage.gameObject.GetComponent<UIContentScaler>();
            // cachedDesignSize = new Vector2(scaler.designResolutionX, scaler.designResolutionY);
            // checker = new RebuildChecker(this);
        }

        public bool ReadOnly { get { return !input.editable; } }

        public string text
        {
            get { return input.text; }
            set { input.text = value; }
        }

        public string placeholder
        {
            get
            {
                return input.promptText;
            }
        }

        public int fontSize
        {
            get { return input.textFormat.size; }
        }

        public ContentType contentType
        {
            get
            {
                if (input.keyboardType == (int)TouchScreenKeyboardType.NumberPad)
                {
                    return ContentType.IntegerNumber;
                }
                else if (input.keyboardType == (int)TouchScreenKeyboardType.DecimalPad)
                {
                    return ContentType.DecimalNumber;
                }
                else if (input.keyboardType == (int)TouchScreenKeyboardType.EmailAddress)
                {
                    return ContentType.EmailAddress;
                }
                else if (input.keyboardType == (int)TouchScreenKeyboardType.ASCIICapable)
                {
                    return ContentType.Alphanumeric;
                }
                else
                {
                    return ContentType.Standard;
                }
            }
        }

        public LineType lineType
        {
            get
            {
                if (input.textField.singleLine)
                {
                    return LineType.SingleLine;
                }
                else
                {
                    return LineType.MultiLineNewline;
                }
            }
        }

        public int characterLimit
        {
            get { return input.maxLength; }
        }

        public int caretPosition
        {
            get { return input.caretPosition; }
        }

        public bool isFocused
        {
            get { return input.focused; }
        }

        public int selectionFocusPosition
        {
            get { return input.selectionEndIndex; }
        }

        public int selectionAnchorPosition
        {
            get { return input.selectionBeginIndex; }
        }

        public void SetSelection(int start, int end)
        {
            input.SetSelection(start, end - start);
        }

        public bool OnFocusSelectAll
        {
            get { return true; }
        }

        public bool EnableMobileSupport
        {
            get
            {
                return true;
            }
        }

        public RectTransform RectTransform()
        {
            return input.cachedTransform as RectTransform;
        }

        public Rect GetBounds()
        {
            var rect = input.GetBounds(input.stage);
            // var x = rect.x - cachedDesignSize.x / 2;
            // var y = cachedDesignSize.y / 2 - rect.yMax;
            // rect.x = x;
            // rect.y = y;
            return rect;
        }

        public void ActivateInputField()
        {
            input.disableIME = false;
        }

        public void DeactivateInputField()
        {
            input.disableIME = true;
        }

        public void Rebuild()
        {
            // if (checker.NeedRebuild())
            // {
            // }
        }

        private IPointerDownHandler eventHandler;
        protected void __touchBegin(EventContext context)
        {
            if (eventHandler != null)
            {
                eventHandler.OnPointerDown(null);
            }
        }
        public void RegisterTouchBegin(IPointerDownHandler eventHandler0)
        {
            eventHandler = eventHandler0;
            input.onTouchBegin.AddCapture(__touchBegin);
        }
    }
}