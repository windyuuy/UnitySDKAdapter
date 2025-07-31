using System;
using UnityEngine;
using System.Collections;
#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR
using WeChatWASM;
#endif
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UnityEngine.WXExt
{
    /// <summary>
    /// fix UGUI
    /// </summary>
    public class WXInputField : MonoBehaviour
// #if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR
        , IPointerClickHandler, IPointerExitHandler
// #endif
    {
        public InputField input;

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
#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR
            Debug.Log("OnPointerClick");
            ShowKeyboard();
#endif
        }

        public void OnPointerExit(PointerEventData eventData)
        {
#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR
            Debug.Log("OnPointerExit");
            if (!input.isFocused)
            {
                HideKeyboard();
            }
#endif
        }

#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR
        public void OnInput(OnKeyboardInputListenerResult v)
        {
            Debug.Log("onInput");
            Debug.Log(v.value);
            if (input.isFocused)
            {
                input.text = v.value;
            }
        }

        public void OnConfirm(OnKeyboardInputListenerResult v)
        {
            // 输入法confirm回调
            Debug.Log("onConfirm");
            Debug.Log(v.value);
            HideKeyboard();
        }

        public void OnComplete(OnKeyboardInputListenerResult v)
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
                var showOptions = new ShowKeyboardOption
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
                WX.ShowKeyboard(showOptions);

                //绑定回调
                WX.OnKeyboardConfirm(OnConfirm);
                WX.OnKeyboardComplete(OnComplete);
                WX.OnKeyboardInput(OnInput);
                _isShowKeyboard = true;
            }
        }

        private void HideKeyboard()
        {
            if (_isShowKeyboard)
            {
                WX.HideKeyboard(new HideKeyboardOption());
                //删除掉相关事件监听
                WX.OffKeyboardInput(OnInput);
                WX.OffKeyboardConfirm(OnConfirm);
                WX.OffKeyboardComplete(OnComplete);
                _isShowKeyboard = false;
            }
        }
#endif
    }
}
