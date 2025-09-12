using GDK;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UnityEngine.WXExt
{
    /// <summary>
    /// fix UGUI
    /// </summary>
    public class WXInputField : MonoBehaviour
        , IPointerClickHandler, IPointerExitHandler
    {
        public InputField input;

        private void Reset()
        {
            input = this.GetComponent<InputField>();
        }

        private void Start()
        {
            if (input == null)
            {
                input = GetComponent<InputField>();
            }
        }

        private bool _isShowKeyboard = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("OnPointerClick");
            ShowKeyboard();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnPointerExit");
            if (!input.isFocused)
            {
                HideKeyboard();
            }
        }

        public void OnInput(OnKeyboardInputResult v)
        {
            Debug.Log("onInput");
            Debug.Log(v.value);
            if (input.isFocused)
            {
                input.text = v.value;
            }
        }

        public void OnConfirm(OnKeyboardConfirmResult v)
        {
            // 输入法confirm回调
            Debug.Log("onConfirm");
            Debug.Log(v.value);
            HideKeyboard();
        }

        public void OnComplete(OnKeyboardCompleteResult v)
        {
            // 输入法complete回调
            Debug.Log("OnComplete");
            Debug.Log(v.value);
            HideKeyboard();
        }

        private void ShowKeyboard()
        {
            if (!_isShowKeyboard)
            {
                var showOptions = new ShowKeyboardOptions
                {
                    confirmHold = true,
                    defaultValue = input.text,
                    multiple = !input.multiLine,
                    confirmType = "done",
                };
                if (input.characterLimit > 0)
                {
                    showOptions.maxLength = input.characterLimit;
                }
                else
                {
                    Debug.Log($"maxlenU: {showOptions.maxLength}");
                    showOptions.maxLength = 10000;
                }
                UserAPI.Instance.Widgets.keyboard.ShowKeyboard(showOptions);

                //绑定回调
                UserAPI.Instance.Widgets.keyboard.OnKeyboardConfirm(OnConfirm);
                UserAPI.Instance.Widgets.keyboard.OnKeyboardComplete(OnComplete);
                UserAPI.Instance.Widgets.keyboard.OnKeyboardInputEvent(OnInput);
                _isShowKeyboard = true;
            }
        }

        private void HideKeyboard()
        {
            if (_isShowKeyboard)
            {
                UserAPI.Instance.Widgets.keyboard.HideKeyboard();
                //删除掉相关事件监听
                UserAPI.Instance.Widgets.keyboard.OffKeyboardInputEvent(OnInput);
                UserAPI.Instance.Widgets.keyboard.OffKeyboardConfirm(OnConfirm);
                UserAPI.Instance.Widgets.keyboard.OffKeyboardComplete(OnComplete);
                _isShowKeyboard = false;
            }
        }
    }
}
