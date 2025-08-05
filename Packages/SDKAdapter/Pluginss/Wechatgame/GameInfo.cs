#if SUPPORT_WECHATGAME
using System.Threading.Tasks;
using GDK;
using WeChatWASM;

namespace WechatGDK
{

    public class GameInfo : GDK.GameInfoBase
    {
        public override void Init()
        {
        }

        public override Task InitWithConfig(GDKConfigV2 info)
        {
            DevLog.Instance.Log("WX.InitSDK");
            var ts = new TaskCompletionSource<int>();
            if (!WXSDKManagerHandler.InitSDKPrompt())
            {
                WX.InitSDK((code) =>
                {
                    DevLog.Instance.Log($"WX.InitSDK return code: {code}");
                    ts.SetResult(code);
                });
            }
            else
            {
                DevLog.Instance.Log($"WX.InitSDK has been inited");
                ts.SetResult(0);
            }

            return ts.Task;
        }

        public override string UserDataPath => WX.env.USER_DATA_PATH;
    }
}
#endif
