using System;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Lang.Loggers.Logger;

namespace GDK
{
	public interface AppInfoKeys
	{
		/**
		 * AppId
		 */
		public static string appid = "gdk.appid";

		/**
		 * 当前是否为测试模式，弃用mode
		 */
		public static string test = "gdk.test";
	}

	public abstract class GameInfoBase : IGameInfo
	{
		public IModuleMap Api { get; set; }

		public string Mode
		{
			get
			{
				// return this.api.gameInfo.getAppInfoBoolean(AppInfoKeys.test, false) == true ? "develop" : "release";
				throw new NotImplementedException();
			}
		}

		public string AppId
		{
			get
			{
				// return this.api.getAppInfoString(AppInfoKeys.appid, "-1");
				throw new NotImplementedException();
			}
		}

		public string GameVersion { get; set; } = "1.0.0";
		public virtual string UserDataPath => Application.persistentDataPath;

		public abstract void Init();

		public virtual async Task InitWithConfig(GDKConfigV2 info)
		{
			info = info ?? new GDKConfigV2();
			if (!string.IsNullOrEmpty(info.GameVersion))
			{
				this.GameVersion = info.GameVersion;
			}
			// this.api.initAppinfo(info.appInfo);

			// CommonServer.getServerTime = info.getServerTime
			// CommonServer.httpClient = info.httpClient
		}

		public Logger devlog = new Logger();
	}
}