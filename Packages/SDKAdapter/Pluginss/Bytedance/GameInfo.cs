#if SUPPORT_BYTEDANCE
using System.Threading.Tasks;
using GDK;
using TTSDK;

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
			TT.InitSDK((code, env) =>
			{
				DevLog.Instance.Log($"TT.InitSDK return code: {code}, {env?.GameAppId}, {env?.m_HostEnum}, {env?.m_LaunchFromEnum}");
				ts.SetResult(code);
			});

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
				DevLog.Instance.Log($"UserDataPath: {_userDataPath}");
				return _userDataPath;
			}
		}
	}
}

#endif