#if SUPPORT_BYTEDANCE
	using System;
	using System.Threading.Tasks;
	using EngineAdapter.Diagnostics;
	using WeChatWASM;
	using GDK;
	using TTSDK;
	using GeneralCallbackResult = WeChatWASM.GeneralCallbackResult;

	namespace BytedanceGDK
	{
		public static class DefaultErrorHandler
		{
			public static Action<GeneralCallbackResult> GetErrorHandler<T>(this TaskCompletionSource<T> ts)
			{
				return (GeneralCallbackResult resp2) => { ts.SetException(new Exception(resp2.errMsg)); };
			}
		}

		public class Clipboard : GDK.IClipboard
		{
			public Task<GDK.ClipboardData> GetData()
			{
				var ts = new TaskCompletionSource<GDK.ClipboardData>();
				TT.GetClipboardData((ok, data) =>
				{
					if (ok)
					{
						ts.SetResult(new GDK.ClipboardData()
						{
							data = data,
						});
					}
					else
					{
						ts.GetErrorHandler();
					}
				});
				return ts.Task;
			}

			public Task SetData(GDK.ClipboardData res)
			{
				var ts = new TaskCompletionSource<bool>();
				TT.SetClipboardData(res.data, (ok, errMsg) =>
				{
					if (ok)
					{
						ts.SetResult(true);
					}
					else
					{
						ts.SetException(new GDKError(errMsg));
					}
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


			public override Task SetEnableDebug(GDK.SetEnableDebugOptions res)
			{
				TT.EnableTTSDKDebugToast = res.enableDebug;
				return Task.CompletedTask;
			}

			public override Task<GDK.AppCallUpResult> NavigateToApp(GDK.AppCallUpParams paras)
			{
				var ts = new TaskCompletionSource<GDK.AppCallUpResult>();
				TT.NavigateToMiniProgram(new NavigateToMiniProgramParam()
				{
					AppId = paras.appId,
					EnvVersion = EnvVersionEnum.Current,
					ExtraData = paras.extraData,
					Path = paras.path,
					Success = (resp) =>
					{
						ts.SetResult(new GDK.AppCallUpResult()
						{
							ErrMsg = resp.ErrMsg,
						});
					},
					Fail = (resp) => { ts.SetException(new GDK.GDKError(resp.ErrMsg)); },
					// appId = paras.appId,
					// envVersion = paras.envVersion,
					// extraData = paras.extraData,
					// path = paras.path,
					// success = (resp) =>
					// {
					// 	ret.SetResult(new GDK.AppCallUpResult()
					// 	{
					// 	});
					// },
					// fail = ret.GetErrorHandler(),
				});
				return ts.Task;
			}

			public override Task<ExitProgramResult> ExitProgram(ExitProgramOptions paras)
			{
				var ret = new TaskCompletionSource<GDK.ExitProgramResult>();
				TT.ExitMiniProgram(paras.ShowDialog, (ok) =>
				{
					ret.SetResult(new GDK.ExitProgramResult
					{
						IsOk = ok,
					});
				});
				return ret.Task;
			}

			public override Task UpdateProgramForce()
			{
				var ret = new TaskCompletionSource<bool>();

				Api.Widgets.ShowLoading(new() { title = "检查更新中...", mask = true });
				var updateManager = TT.GetUpdateManager();
				if (updateManager != null)
				{
					updateManager.OnCheckForUpdate((hasUpdate) =>
					{
						DevLog.Instance.Info("检查更新开始:");
						if (hasUpdate.HasUpdate)
						{
							DevLog.Instance.Info("有更新");
							// SDKProxy.showLoading({title:"检查更新中...",mask:true})
						}
						else
						{
							DevLog.Instance.Info("没有更新");
							Api.Widgets.HideLoading();
							ret.SetResult(true);
						}
					});

					updateManager.OnUpdateReady(() =>
					{
						DevLog.Instance.Info("更新完成");
						Api.Widgets.HideLoading();
						Api.Widgets.ShowModal(new()
						{
							title = "提示",
							content = "新版本已经下载完成！",
							confirmText = "重启游戏",
							cancelText = "重启游戏",
							showCancel = false,
							callback = (res) =>
							{
								if (res.confirm)
								{
									TT.GetUpdateManager().ApplyUpdate(new());
								}
							}
						});
					});

					updateManager.OnUpdateFailed((resp) =>
					{
						DevLog.Instance.Info($"更新失败: {resp.Err}");
						Api.Widgets.HideLoading();
						Api.Widgets.ShowModal(new()
						{
							title = "提示",
							content = "更新失败,请重启游戏",
							confirmText = "重启游戏",
							cancelText = "重启游戏",
							showCancel = false,
							callback = (res) =>
							{
								if (res.confirm)
								{
									TT.GetUpdateManager().ApplyUpdate(new());
								}
							}
						});
					});
				}

				return ret.Task;
			}

			public override void OnShow(Action<object> callback)
			{
				DevLog.Instance.Error($"TT.{nameof(OnShow)} not implemented");
			}

			public override void OffShow(Action<object> callback)
			{
				DevLog.Instance.Error($"TT.{nameof(OffShow)} not implemented");
			}

			public override void OnHide(Action<object> callback)
			{
				DevLog.Instance.Error($"TT.{nameof(OnHide)} not implemented");
			}

			public override void OffHide(Action<object> callback)
			{
				DevLog.Instance.Error($"TT.{nameof(OffHide)} not implemented");
			}

			public override void GetFPS(int fps)
			{
				TT.SetPreferredFramesPerSecond(fps);
			}

			public override void OnMemoryWarning(Action<GDK.IOnMemoryWarningResult> call)
			{
				DevLog.Instance.Error($"{nameof(OnMemoryWarning)} not supported");
			}

			public override void OffMemoryWarning(Action<GDK.IOnMemoryWarningResult> call)
			{
				DevLog.Instance.Error($"{nameof(OffMemoryWarning)} not supported");
			}

			public override Task<RestartMiniProgramResult> RestartMiniProgram(RestartMiniProgramOptions options)
			{
				DevLog.Instance.Log("TT.RestartMiniProgram");
				var ok = TT.RestartMiniProgramSync();
				DevLog.Instance.Log("TT.RestartMiniProgram-done");
				return Task.FromResult(new RestartMiniProgramResult()
				{
					IsOk = ok,
				});
			}

			public override LaunchOptions GetLaunchOptionsSync()
			{
				var launchOptions = TT.GetLaunchOptionsSync();
				var referrerInfo = launchOptions.RefererInfo;
				return new LaunchOptions
				{
					Scene = launchOptions.Scene,
					Query = launchOptions.Query,
					Path = null,
					IsSticky = false,
					ShareTicket = launchOptions.ShareTicket,
					ReferrerInfo = new()
					{
						ExtraData = null,
						AppId = null,
						GameLiveInfo = null,
					}
				};
			}
		}
	}
#endif