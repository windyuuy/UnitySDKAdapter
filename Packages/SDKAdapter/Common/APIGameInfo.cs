
using System;
using System.Threading.Tasks;

namespace GDK
{

	public interface AppInfoKeys
	{
		/**
		 * AppId
		 */
		public static string appid = "glee.appid";

		/**
		 * 当前是否为测试模式，弃用mode
		 */
		public static string test = "glee.test";

	}

	public abstract class GameInfoBase : IGameInfo
	{
		public IModuleMap api { get; set; }

		public string mode
		{
			get
			{
				// return this.api.gameInfo.getAppInfoBoolean(AppInfoKeys.test, false) == true ? "develop" : "release";
				throw new NotImplementedException();
			}
		}

		public string appId
		{
			get
			{
				// return this.api.getAppInfoString(AppInfoKeys.appid, "-1");
				throw new NotImplementedException();
			}
		}

		public string gameVersion { get; set; } = "1.0.0";

		public abstract void init();

		public async Task initWithConfig(GDKConfigV2 info)
		{
			info = info ?? new GDKConfigV2();
			if (!string.IsNullOrEmpty(info.gameVersion))
			{
				this.gameVersion = info.gameVersion;
			}
			// this.api.initAppinfo(info.appInfo);

			// CommonServer.getServerTime = info.getServerTime
			// CommonServer.httpClient = info.httpClient
		}

	}
}
