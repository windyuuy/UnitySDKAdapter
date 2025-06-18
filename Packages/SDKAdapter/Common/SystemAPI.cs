
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lang.Loggers;

namespace GDK
{
	public class Clipboard : IClipboard
	{
		public ClipboardData _data = null;
		public virtual async Task<ClipboardData> GetData()
		{
			// return { ...this._data }
			// return {};
			return this._data;
		}
		public virtual async Task SetData(ClipboardData res)
		{
			// this._data = { ...res }
			this._data = new()
			{
				data = res.data,
			};
		}
	}

	/// <summary>
	/// Base class implementing ISystemAPI interface providing system-level functionality.
	/// Includes methods for FPS control, memory warnings, clipboard access, debugging,
	/// app navigation, permissions checking, and app info management.
	/// Also handles system events like show/hide and provides safe area information.
	/// </summary>
	/// <remarks>
	/// This is an abstract base class - most methods throw NotImplementedException by default
	/// and should be overridden by platform-specific implementations.
	/// </remarks>
	public class SystemAPIBase : ISystemAPI
	{
		public virtual void GetFPS(int fps)
		{
			throw new System.Exception("Method not implemented.");
		}
		public virtual void OnMemoryWarning(Action<IOnMemoryWarningResult> call)
		{
			throw new System.Exception("Method not implemented.");
		}

		public string SdkFrameworkVersion
		{
			get
			{
				return "-1.0";
			}
		}
		public virtual void init()
		{
			this._initEvents();

			this.appAutoRestart();
		}

		public int nativeVersion
		{
			get
			{
				return -1;
			}
		}
		double ISystemAPI.NativeVersion => nativeVersion;

		public virtual IClipboard Clipboard { get; set; } = new Clipboard();

        public Logger devlog = new Logger();
		public virtual async Task SetEnableDebug(SetEnableDebugOptions res)
		{
			devlog.Info($"unsupoort action: setEnableDebug -> {res.enableDebug} ");
		}

		public virtual async Task<AppCallUpResult> NavigateToApp(AppCallUpParams paras)
		{
			devlog.Info("打开小程序成功");
			return new AppCallUpResult();
		}
		public virtual async Task ExitProgram()
		{
			devlog.Info("正在退出");
			UnityEngine.Application.Quit();
		}
		public virtual async Task UpdateProgramForce()
		{
			devlog.Info("没有更新");
		}

		public virtual void _initEvents()
		{
			// TODO: 实现系统各种事件回调和api
		}

		public virtual void OnShow(Action<object> callback)
		{
			// TODO: 实现系统各种事件回调和api
		}
		public virtual void OffShow(Action<object> callback)
		{
			// TODO: 实现系统各种事件回调和api
		}
		public virtual void OnHide(Action<object> callback)
		{
			// TODO: 实现系统各种事件回调和api
		}
		public virtual void OffHide(Action<object> callback)
		{
			// TODO: 实现系统各种事件回调和api
		}

		/// <summary>
		/// Gets the safe area of the screen and returns the result via callback.
		/// </summary>
		/// <param name="callback">The callback to receive the safe area result with left, right, top, and bottom values.</param>
		public virtual void GetSafeArea(Action<GetSafeAreaResult> callback)
		{
			callback(new() { left = 0, right = 0, top = 0, bottom = 0 });
		}

		public virtual async Task<IChooseDialogResult> GotoAppSystemSettings(IChooseDialogParams paras)
		{
			IChooseDialogResult result = new ChooseDialogResult()
			{
				action = "cancel",
				crashed = false,
			};
			return result;
		}
		public virtual async Task<ICheckPermissionResult> CheckAppSystemPermissions(ICheckPermissionParams paras)
		{
			ICheckPermissionResult result = new CheckPermissionResult()
			{
				lackedPermissions = Array.Empty<string>(),
				error = { },
			};
			return result;
		}

		public virtual async Task<object> GetSDKMetaInfo(IGetSDKMetaInfo paras)
		{
			return null;
		}

		public readonly Dictionary<string, object> AppInfo = new();

		public virtual void InitAppinfo(AppInfo info)
		{
			if (info != null)
			{
				// 将所有sdk config中的key, 合并到自身同一个appInfo中
				if (info.SdkConfigs != null)
				{
					foreach (var sdkConfig in info.SdkConfigs)
					{
						if (sdkConfig.parameters != null)
						{
							foreach (var (k, _) in sdkConfig.parameters)
							{
								this.AppInfo[$"{sdkConfig.name}.{k}"] = sdkConfig.parameters[k];
							}
						}
					}
				}
				if (info.Parameters != null)
				{
					foreach (var (k, _) in info.Parameters)
					{
						this.AppInfo[k] = info.Parameters[k];
					}
				}
			}
		}

		public virtual void SetAppInfo(string key, string value)
		{
			this.AppInfo[key] = value;
		}

		public virtual void SetAppInfo(string key, double value)
		{
			this.AppInfo[key] = value;
		}

		public virtual void SetAppInfo(string key, bool value)
		{
			this.AppInfo[key] = value;
		}

		public object GetAppInfo(string key)
		{
			return this.AppInfo.GetValueOrDefault(key);
		}

		public virtual bool getAppInfoBoolean(string key, bool def = false)
		{
			var v = this.GetAppInfo(key);
			if (v is bool b)
			{
				return b;
			}
			else if (v is string s)
			{
				return s.ToLower() == "true";
			}
			else
			{
				return def;
			}

		}

		public double GetAppInfoDouble(string key, double def)
		{
			var v = this.GetAppInfo(key);
			if (v is double d)
			{
				return d;
			}
			else if (v is string s && !float.IsNaN(float.Parse(v.ToString())))
			{
				return float.Parse(s);
			}
			else
			{
				return def;
			}
		}

		public string GetAppInfoString(string key, string def)
		{
			var v = this.GetAppInfo(key);
			if (v == null)
			{
				return def;
			}
			else
			{
				return v.ToString();
			}
		}

		/**
		 * app项目，进入后台10分钟以后，重新进入前台，默认重启App，调用cc.restart方法
		 */
		public virtual void appAutoRestart()
		{

		}

		public virtual void SetLoadingProgress(SetLoadingProgressOptions paras)
		{
			throw new NotImplementedException();
		}

		public virtual void OpenURL(string url)
		{
			throw new NotImplementedException();
		}

		public virtual void StartYunkefu(string accessId, string name, string id, object customField, bool native)
		{
			throw new NotImplementedException();
		}

		public virtual bool HasNativeAssistantCenter()
		{
			throw new NotImplementedException();
		}

		public virtual void ShowHackWeb(string url, double duration)
		{
			throw new NotImplementedException();
		}

		public virtual void SetSDKLanguage(string lang)
		{
			throw new NotImplementedException();
		}

		public virtual bool GetAppInfoBool(string key, bool def)
		{
			throw new NotImplementedException();
		}

		public virtual void OffMemoryWarning(Action<IOnMemoryWarningResult> call)
		{
			throw new NotImplementedException();
		}

        public virtual Task<RestartMiniProgramResult> RestartMiniProgram(RestartMiniProgramOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
