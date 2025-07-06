#if SUPPORT_FGUI
	using FairyGUI;
#endif

#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR
using WeChatWASM;
#endif

	namespace UnityEngine.WXExt
	{
		/// <summary>
		/// fix FGUI
		/// </summary>
		public class WXFInputField : MonoBehaviour
		{
		#if SUPPORT_FGUI
			private InputTextField _input;

			public InputTextField Input
			{
				get => _input;
				set
				{
				#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR
                if (input != null)
                {
                    input.onFocusIn.Remove(__focusIn);
                    input.onFocusOut.Remove(__focusOut);
                    input = null;
                }

                if (value != null)
                {
                    input = value;
                    input.onFocusIn.Add(__focusIn);
                    input.onFocusOut.Add(__focusOut);
                }
				#else
					_input = value;
				#endif
				}
			}
		#endif

		#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR && SUPPORT_FGUI
        private bool _isShowKeyboard = false;

        void __focusIn(EventContext context)
        {
            Debug.Log("OnPointerClick");
            ShowKeyboard();
        }

        void __focusOut(EventContext contxt)
        {
            Debug.Log("OnPointerExit");
            if (!input.focused)
            {
                HideKeyboard();
            }
        }

        public void OnInput(OnKeyboardInputListenerResult v)
        {
            Debug.Log("onInput");
            Debug.Log(v.value);
            if (input.focused)
            {
                input.ReplaceText(v.value);
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
                    multiple = !input.textField.singleLine,
                    confirmType = "done",
                };
                if (input.maxLength > 0)
                {
                    showOptions.maxLength = input.maxLength;
                }
                else
                {
                    Debug.Log($"maxlenF: {showOptions.maxLength}");
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