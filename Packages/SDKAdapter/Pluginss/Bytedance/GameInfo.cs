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
				DevLog.Instance.Log("TT.InitSDK");
				var ts = new TaskCompletionSource<int>();
				if (!WXSDKManagerHandler.InitSDKPrompt())
				{
					TT.InitSDK((code, env) =>
					{
						DevLog.Instance.Log($"TT.InitSDK return code: {code}");
						ts.SetResult(code);
					});
				}
				else
				{
					DevLog.Instance.Log($"TT.InitSDK has been inited");
					ts.SetResult(0);
				}

				return ts.Task;
			}

			private string _userDataPath;

			public override string UserDataPath
			{
				get
				{
					if (_userDataPath != null)
					{
						return _userDataPath;
					}

					_userDataPath = TTAdapter.GDK_Bytedance_GetUserDataPath();
					return _userDataPath;
				}
			}
		}
	}
#endif

#endif