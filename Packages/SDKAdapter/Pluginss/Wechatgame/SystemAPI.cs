#if SUPPORT_WECHATGAME
using System;
using System.Threading.Tasks;
using WeChatWASM;
using GDK;

namespace WechatGDK
{
	public static class DefaultErrorHandler
	{
		public static Action<GeneralCallbackResult> GetErrorHandler<T>(this TaskCompletionSource<T> ts)
		{
			return (GeneralCallbackResult resp2) =>
			{
				ts.SetException(new Exception(resp2.errMsg));
			};
		}
	}
	public class Clipboard : GDK.IClipboard
	{
		public Task<GDK.ClipboardData> GetData()
		{
			var ts = new TaskCompletionSource<GDK.ClipboardData>();
			WX.GetClipboardData(new()
			{
				success = (res) =>
				{
					ts.SetResult(new GDK.ClipboardData()
					{
						data = res.data,
					});
				},
				fail = ts.GetErrorHandler(),
			});
			return ts.Task;
		}
		public Task SetData(GDK.ClipboardData res)
		{
			var ts = new TaskCompletionSource<bool>();
			WX.SetClipboardData(new()
			{
				data = res.data,
				success = (resp) =>
				{
					ts.SetResult(true);
				},
				fail = ts.GetErrorHandler(),
			});
			return ts.Task;
		}
	}

	public class SystemAPI : GDK.SystemAPIBase
	{
		public override GDK.IClipboard Clipboard { get; set; } = new Clipboard();

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


		public override Task SetEnableDebug(GDK.SetEnableDebugOptions res)
		{
			var ts = new TaskCompletionSource<bool>();
			WX.SetEnableDebug(new()
			{
				enableDebug = res.enableDebug,
				success = (resp) =>
				{
					ts.SetResult(true);
				},
				fail = ts.GetErrorHandler(),
			});
			return ts.Task;
		}

		public override Task<GDK.AppCallUpResult> NavigateToApp(GDK.AppCallUpParams paras)
		{
			var ret = new TaskCompletionSource<GDK.AppCallUpResult>();
			WX.NavigateToMiniProgram(new()
			{
				appId = paras.appId,
				envVersion = paras.envVersion,
				extraData = paras.extraData,
				path = paras.path,
				success = (resp) =>
				{
					ret.SetResult(new GDK.AppCallUpResult()
					{

					});
				},
				fail = ret.GetErrorHandler(),
			});
			return ret.Task;
		}
		public override Task ExitProgram()
		{
			var ret = new TaskCompletionSource<bool>();
			WX.ExitMiniProgram(new()
			{
				success = (resp) =>
				{
					ret.SetResult(true);
				},
				fail = ret.GetErrorHandler(),
			});
			return ret.Task;
		}
		public override Task UpdateProgramForce()
		{
			var ret = new TaskCompletionSource<bool>();

			WX.ShowLoading(new() { title = "检查更新中...", mask = true });
			var updateManager = WX.GetUpdateManager();
			if (updateManager != null)
			{
				updateManager.OnCheckForUpdate((hasUpdate) =>
				{
					devlog.Info("检查更新开始:");
					if (hasUpdate.hasUpdate)
					{
						devlog.Info("有更新");
						// SDKProxy.showLoading({title:"检查更新中...",mask:true})
					}
					else
					{
						devlog.Info("没有更新");
						WX.HideLoading(new() { });
						ret.SetResult(true);
					}
				});

				updateManager.OnUpdateReady((resp) =>
				{
					devlog.Info("更新完成");
					WX.HideLoading(new() { });
					WX.ShowModal(new()
					{
						title = "提示",
						content = "新版本已经下载完成！",
						confirmText = "重启游戏",
						cancelText = "重启游戏",
						showCancel = false,
						success = (res) =>
						{
							if (res.confirm)
							{
								WX.GetUpdateManager().ApplyUpdate();
							}
						}
					});
				});

				updateManager.OnUpdateFailed((resp) =>
				{
					devlog.Info($"更新失败: {resp.errMsg}");
					WX.HideLoading(new() { });
					WX.ShowModal(new()
					{
						title = "提示",
						content = "更新失败,请重启游戏",
						confirmText = "重启游戏",
						cancelText = "重启游戏",
						showCancel = false,
						success = (res) =>
						{
							if (res.confirm)
							{
								WX.GetUpdateManager().ApplyUpdate();
							}
						}
					});
				});
			}
			return ret.Task;
		}

		public override void OnShow(Action<object> callback)
		{
			WX.OnShow(callback);
		}
		public override void OffShow(Action<object> callback)
		{
			WX.OffShow(callback);
		}
		public override void OnHide(Action<object> callback)
		{
			WX.OnHide(callback);
		}
		public override void OffHide(Action<object> callback)
		{
			WX.OffHide(callback);
		}

		public override void GetFPS(int fps)
		{
			WX.SetPreferredFramesPerSecond(fps);
		}


		protected Action<GDK.IOnMemoryWarningResult> _onMemoryWarning0;
		private Action<OnMemoryWarningListenerResult> _onMemoryWarning;
		public override void OnMemoryWarning(Action<GDK.IOnMemoryWarningResult> call)
		{
			_onMemoryWarning0 += call;
			if (_onMemoryWarning != null)
			{
				_onMemoryWarning = (resp) =>
				{
					_onMemoryWarning0?.Invoke(new GDK.OnMemoryWarningResult()
					{
						level = (GDK.MemoryWarningLevel)resp.level,
					});
				};
				WX.OnMemoryWarning(_onMemoryWarning);
			}
		}
		public override void OffMemoryWarning(Action<GDK.IOnMemoryWarningResult> call)
		{
			_onMemoryWarning0 -= call;
			if (_onMemoryWarning0 == null)
			{
				WX.OffMemoryWarning(_onMemoryWarning);
				_onMemoryWarning = null;
			}
		}

		public override Task<RestartMiniProgramResult> RestartMiniProgram(RestartMiniProgramOptions options)
		{
			var ts = new TaskCompletionSource<RestartMiniProgramResult>();
			UnityEngine.Debug.Log("WX.RestartMiniProgram");
			WX.RestartMiniProgram(new WeChatWASM.RestartMiniProgramOption()
			{
				success = (resp) =>
				{
					UnityEngine.Debug.Log("WX.RestartMiniProgram-done");
					ts.SetResult(new RestartMiniProgramResult()
					{
					});
				},
				fail = ts.GetErrorHandler(),
			});
			return ts.Task;
		}

	}

}
#endif
