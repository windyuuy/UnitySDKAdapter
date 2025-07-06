
#if UNITY_WEBGL && !SUPPORT_WECHATGAME && SUPPORT_FGUI
using FairyGUI;
#endif

namespace UnityEngine.WebGLExt
{
    public class WebGLInputFix
    {
        /// <summary>
        /// 适配微信小游戏输入键盘
        /// </summary>
        public static void FixInput()
        {
#if UNITY_WEBGL && !SUPPORT_WECHATGAME && SUPPORT_FGUI
            // InputField.OnCreateEx = (inputTextField) =>
            // {
            //     var ext = inputTextField.gameObject.GetOrAddComponent<WebGLSupport.WebGLInput>();
            //     ext.showHtmlElement = true;
            // };
            InputTextField.OnCreateEx = (inputTextField) =>
            {
                var go = inputTextField.gameObject;
                var ext = go.GetComponent<WebGLSupport.WebGLInput>();
                if(ext==null){
                    ext = go.AddComponent<WebGLSupport.WebGLInput>();
                }
                ext.showHtmlElement = true;
            };
#endif
        }
    }
}
