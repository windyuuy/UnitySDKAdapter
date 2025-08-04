#if SUPPORT_BYTEDANCE
	using System.Threading.Tasks;
	using GDK;
	using TTSDK;
	using WeChatWASM;

#if SUPPORT_BYTEDANCE
	namespace BytedanceGDK
	{
		public class GameInfo : GDK.GameInfoBase
		{
			public override void Init()
			{
			}

			public override Task InitWithConfig(GDKConfigV2 info)
			{
				devlog.Log("TT.InitSDK");
				var ts = new TaskCompletionSource<int>();
				if (!WXSDKManagerHandler.InitSDKPrompt())
				{
					TT.InitSDK((code, env) =>
					{
						devlog.Log($"TT.InitSDK return code: {code}");
						ts.SetResult(code);
					});
				}
				else
				{
					devlog.Log($"TT.InitSDK has been inited");
					ts.SetResult(0);
				}

				return ts.Task;
			}
		}
	}
#endif

#endif