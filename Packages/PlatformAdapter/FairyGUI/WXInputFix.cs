#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR && SUPPORT_FGUI
using FairyGUI;
#endif

namespace UnityEngine.WXExt
{
    public class WXInputFix
    {
        /// <summary>
        /// 适配微信小游戏输入键盘
        /// </summary>
        public static void FixInputField()
        {
#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR && SUPPORT_FGUI
            // 适配微信小游戏输入键盘
            InputTextField.OnCreateEx = (inputTextField) =>
            {
                Debug.Log("添加自定义微信输入组件");
                var go = inputTextField.gameObject;
                var ext = go.GetComponent<WXFInputField>();
                if (ext == null)
                {
                    ext = go.AddComponent<WXFInputField>();
                }
                ext.Input = inputTextField;
            };
            // if (WechatSDKWrapper.Inst.IsWechatGame)
            // {
            //
            // }
#endif
        }
    }
}