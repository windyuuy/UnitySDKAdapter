
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lang.Loggers;

namespace GDK
{
	public class Clipboard : IClipboard
	{
		public ClipboardData _data = null;
		public virtual async Task<ClipboardData> getData()
		{
			// return { ...this._data }
			// return {};
			return this._data;
		}
		public virtual async Task setData(ClipboardData res)
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
		public virtual void setFPS(int fps)
		{
			throw new System.Exception("Method not implemented.");
		}
		public virtual void onMemoryWarning(Action<IOnMemoryWarningResult> call)
		{
			throw new System.Exception("Method not implemented.");
		}

		public string sdkFrameworkVersion
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
		double ISystemAPI.nativeVersion => nativeVersion;

		public virtual IClipboard clipboard { get; set; } = new Clipboard();

        public Logger devlog = new Logger();
		public virtual async Task setEnableDebug(SetEnableDebugOptions res)
		{
			devlog.Info($"unsupoort action: setEnableDebug -> {res.enableDebug} ");
		}

		public virtual async Task<AppCallUpResult> navigateToApp(AppCallUpParams paras)
		{
			devlog.Info("打开小程序成功");
			return new AppCallUpResult();
		}
		public virtual async Task exitProgram()
		{
			devlog.Info("正在退出");
			UnityEngine.Application.Quit();
		}
		public virtual async Task updateProgramForce()
		{
			devlog.Info("没有更新");
		}

		public virtual void _initEvents()
		{
			// TODO: 实现系统各种事件回调和api
		}

		public virtual void onShow(Action<object> callback)
		{
			// TODO: 实现系统各种事件回调和api
		}
		public virtual void offShow(Action<object> callback)
		{
			// TODO: 实现系统各种事件回调和api
		}
		public virtual void onHide(Action<object> callback)
		{
			// TODO: 实现系统各种事件回调和api
		}
		public virtual void offHide(Action<object> callback)
		{
			// TODO: 实现系统各种事件回调和api
		}

		/// <summary>
		/// Gets the safe area of the screen and returns the result via callback.
		/// </summary>
		/// <param name="callback">The callback to receive the safe area result with left, right, top, and bottom values.</param>
		public virtual void getSafeArea(Action<GetSafeAreaResult> callback)
		{
			callback(new() { left = 0, right = 0, top = 0, bottom = 0 });
		}

		public virtual async Task<IChooseDialogResult> gotoAppSystemSettings(IChooseDialogParams paras)
		{
			IChooseDialogResult result = new ChooseDialogResult()
			{
				action = "cancel",
				crashed = false,
			};
			return result;
		}
		public virtual async Task<ICheckPermissionResult> checkAppSystemPermissions(ICheckPermissionParams paras)
		{
			ICheckPermissionResult result = new CheckPermissionResult()
			{
				lackedPermissions = Array.Empty<string>(),
				error = { },
			};
			return result;
		}

		public virtual async Task<object> getSDKMetaInfo(IGetSDKMetaInfo paras)
		{
			return null;
		}

		public Dictionary<string, object> appInfo = new();

		public virtual void initAppinfo(AppInfo info)
		{
			if (info != null)
			{
				// 将所有sdk config中的key, 合并到自身同一个appInfo中
				if (info.sdkConfigs != null)
				{
					foreach (var sdkConfig in info.sdkConfigs)
					{
						if (sdkConfig.parameters != null)
						{
							foreach (var (k, _) in sdkConfig.parameters)
							{
								this.appInfo[$"{sdkConfig.name}.{k}"] = sdkConfig.parameters[k];
							}
						}
					}
				}
				if (info.parameters != null)
				{
					foreach (var (k, _) in info.parameters)
					{
						this.appInfo[k] = info.parameters[k];
					}
				}
			}
		}

		public virtual void setAppInfo(string key, string value)
		{
			this.appInfo[key] = value;
		}

		public virtual void setAppInfo(string key, double value)
		{
			this.appInfo[key] = value;
		}

		public virtual void setAppInfo(string key, bool value)
		{
			this.appInfo[key] = value;
		}

		public object getAppInfo(string key)
		{
			return this.appInfo[key];
		}

		public virtual bool getAppInfoBoolean(string key, bool def = false)
		{
			var v = this.getAppInfo(key);
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

		public double getAppInfodouble(string key, double def)
		{
			var v = this.getAppInfo(key);
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

		public string getAppInfoString(string key, string def)
		{
			var v = this.getAppInfo(key);
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

		public virtual void setLoadingProgress(SetLoadingProgressOptions paras)
		{
			throw new NotImplementedException();
		}

		public virtual void openURL(string url)
		{
			throw new NotImplementedException();
		}

		public virtual void startYunkefu(string accessId, string name, string id, object customField, bool native)
		{
			throw new NotImplementedException();
		}

		public virtual bool hasNativeAssistantCenter()
		{
			throw new NotImplementedException();
		}

		public virtual void showHackWeb(string url, double duration)
		{
			throw new NotImplementedException();
		}

		public virtual void setSDKLanguage(string lang)
		{
			throw new NotImplementedException();
		}

		public virtual bool getAppInfobool(string key, bool def)
		{
			throw new NotImplementedException();
		}

		public virtual void offMemoryWarning(Action<IOnMemoryWarningResult> call)
		{
			throw new NotImplementedException();
		}

	}
}
