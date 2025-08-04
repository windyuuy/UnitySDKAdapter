using System.Threading.Tasks;
using GDK;
using WeChatWASM;

#if SUPPORT_WECHATGAME
namespace WechatGDK
{

    public class GameInfo : GDK.GameInfoBase
    {
        public override void Init()
        {
        }

        public override Task InitWithConfig(GDKConfigV2 info)
        {
            devlog.Log("WX.InitSDK");
            var ts = new TaskCompletionSource<int>();
            if (!WXSDKManagerHandler.InitSDKPrompt())
            {
                WX.InitSDK((code) =>
                {
                    devlog.Log($"WX.InitSDK return code: {code}");
                    ts.SetResult(code);
                });
            }
            else
            {
                devlog.Log($"WX.InitSDK has been inited");
                ts.SetResult(0);
            }

            return ts.Task;
        }


    }
}
#endif
