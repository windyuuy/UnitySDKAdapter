#if SUPPORT_WECHATGAME
	using System;
	using System.Threading.Tasks;
	using GDK;
	using UnityEngine;
	using WeChatWASM;

	namespace WechatGDK
	{
		public static class WXReqHelper
		{
			public static Task wrapReq<TC>(Action<TC> func, GDK.GDKErrorCode code)
				where TC : ICallback<GeneralCallbackResult, GeneralCallbackResult, GeneralCallbackResult>, new()
			{
				try
				{
					var ts = new TaskCompletionSource<bool>();
					func(new TC()
					{
						success = (resp) => { ts.SetResult(true); },
						fail = (resp) => { ts.SetException(new GDK.GDKError(resp.errMsg)); },
					});
					return ts.Task;
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
					throw GDK.ResultTemplatesExtractor.GDKResultTemplates.make<GDK.GDKErrorExtra>(code);
				}
			}

			public static Task wrapReq<TC>(Action<TC> func, TC req, GDK.GDKErrorCode code)
				where TC : ICallback<GeneralCallbackResult, GeneralCallbackResult, GeneralCallbackResult>, new()
			{
				try
				{
					var ts = new TaskCompletionSource<bool>();
					req.success = (resp) => { ts.SetResult(true); };
					req.fail = (resp) => { ts.SetException(new GDK.GDKError(resp.errMsg)); };
					func(req);
					return ts.Task;
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
					throw GDK.ResultTemplatesExtractor.GDKResultTemplates.make(code, exception);
				}
			}
		}

		public class KeyBoard : GDK.KeyBoardBase<Action<OnKeyboardInputListenerResult>,
			Action<OnKeyboardInputListenerResult>, Action<OnKeyboardInputListenerResult>>
		{
			public KeyBoard()
			{
				OnKeyboardInputHandler = (resp) =>
				{
					if (OnKeyboardInputAction != null)
					{
						var result = new OnKeyboardInputResult
						{
							value = resp.value,
						};
						OnKeyboardInputAction(result);
					}
				};

				OnKeyboardCompleteHandler = (resp) =>
				{
					if (OnKeyboardCompleteAction != null)
					{
						var result = new OnKeyboardCompleteResult
						{
							value = resp.value,
						};
						OnKeyboardCompleteAction(result);
					}
				};

				OnKeyboardConfirmHandler = (resp) =>
				{
					if (OnKeyboardConfirmAction != null)
					{
						var result = new OnKeyboardConfirmResult
						{
							value = resp.value,
						};
						OnKeyboardConfirmAction(result);
					}
				};
			}

			public override Task ShowKeyboard(GDK.ShowKeyboardOptions options)
			{
				var ts = new TaskCompletionSource<bool>();
				WX.ShowKeyboard(new ShowKeyboardOption
				{
					confirmHold = options.confirmHold,
					confirmType = options.confirmType,
					defaultValue = options.defaultValue,
					maxLength = options.maxLength,
					multiple = options.multiple,

					success = (res) => { ts.SetResult(true); },
					fail = (resp) => { ts.SetException(new GDKError(resp.errMsg)); },
				});
				return ts.Task;
			}

			public override Task UpdateKeyboard(GDK.UpdateKeyboardOptions options)
			{
				var ts = new TaskCompletionSource<bool>();
				WX.UpdateKeyboard(new()
				{
					success = (resp) => { ts.SetResult(true); },
					fail = (resp) => { ts.SetException(new GDKError(resp.errMsg)); },
					value = options.value,
				});
				return ts.Task;
			}

			public override Task HideKeyboard()
			{
				return WXReqHelper.wrapReq<HideKeyboardOption>(WX.HideKeyboard, GDK.GDKErrorCode.API_HIDE_KEYBOARD_FAILED);
			}

			public override void OnKeyboardInputEvent(Action<OnKeyboardInputResult> callback)
			{
				if (OnKeyboardInputAction == null)
				{
					WX.OnKeyboardInput(OnKeyboardInputHandler);
				}

				OnKeyboardInputAction += callback;
			}

			public override void OffKeyboardInputEvent(Action<OnKeyboardInputResult> callback)
			{
				OnKeyboardInputAction -= callback;

				if (OnKeyboardInputAction == null)
				{
					WX.OffKeyboardInput(OnKeyboardInputHandler);
				}
			}

			public override void OnKeyboardConfirm(Action<OnKeyboardConfirmResult> callback)
			{
				if (OnKeyboardConfirmAction == null)
				{
					WX.OnKeyboardConfirm(OnKeyboardConfirmHandler);
				}

				OnKeyboardConfirmAction += callback;
			}

			public override void OffKeyboardConfirm(Action<OnKeyboardConfirmResult> callback)
			{
				OnKeyboardConfirmAction -= callback;

				if (OnKeyboardConfirmAction == null)
				{
					WX.OffKeyboardConfirm(OnKeyboardConfirmHandler);
				}
			}

			public override void OnKeyboardComplete(Action<OnKeyboardCompleteResult> callback)
			{
				if (OnKeyboardCompleteAction == null)
				{
					WX.OnKeyboardComplete(OnKeyboardCompleteHandler);
				}

				OnKeyboardCompleteAction += callback;
			}

			public override void OffKeyboardComplete(Action<OnKeyboardCompleteResult> callback)
			{
				OnKeyboardCompleteAction -= callback;

				if (OnKeyboardCompleteAction == null)
				{
					WX.OffKeyboardComplete(OnKeyboardCompleteHandler);
				}
			}
		}

		public class Widgets : GDK.WidgetsBase
		{
			public override GDK.IKeyBoard keyboard { get; } = new KeyBoard();

			public override Task ShowLoading(GDK.ShowLoadingParams obj2)
			{
				return WXReqHelper.wrapReq<ShowLoadingOption>(WX.ShowLoading, new()
				{
					title = obj2.title,
					mask = obj2.mask,
				}, GDK.GDKErrorCode.API_SHOW_LOADING_FAILED);
			}

			public override Task HideLoading()
			{
				return WXReqHelper.wrapReq<HideLoadingOption>(WX.HideLoading, GDK.GDKErrorCode.API_HIDE_LOADING_FAILED);
			}

			public override Task<ShowWidgetResult> ShowToast(GDK.ShowToastOptions obj2)
			{
				var ts = new TaskCompletionSource<ShowWidgetResult>();
				WX.ShowToast(new ShowToastOption
				{
					duration = obj2.duration,
					icon = obj2.icon,
					image = obj2.image,
					mask = obj2.mask,
					title = obj2.title,

					success = (resp) =>
					{
						ts.SetResult(new()
						{
							IsOk = true,
							errMsg = resp.errMsg,
						});
					},
					fail = (resp) => { ts.SetException(new GDK.GDKError(resp.errMsg)); },
				});
				return ts.Task;
			}

			public override Task hideToast()
			{
				return WXReqHelper.wrapReq<HideToastOption>(WX.HideToast, GDK.GDKErrorCode.API_HIDE_TOAST_FAILED);
			}

			public override Task<GDK.ShowModalResult> ShowModal(GDK.ShowModalOptions obj2)
			{
				var ret = new TaskCompletionSource<GDK.ShowModalResult>();

				var opt = new ShowModalOption
				{
					success = (data) =>
					{
						var result = new GDK.ShowModalResult
						{
							confirm = data.confirm,
							cancel = data.cancel,
							result = data.content,
							errMsg = data.errMsg,
							extra = data,
						};
						obj2.callback?.Invoke(result);
						ret.SetResult(result);
					},
					fail = (resp) =>
					{
						DevLog.Instance.Error(resp.errMsg);
						var result = new GDK.ShowModalResult
						{
							errMsg = resp.errMsg,
							extra = resp,
						};
						obj2.callback?.Invoke(result);
						ret.SetResult(result);
					},
					cancelColor = obj2.cancelColor,
					title = obj2.title,
					content = obj2.content,
					editable = obj2.editable,
					placeholderText = obj2.placeholderText,
					confirmText = obj2.confirmText ?? "确定",
					cancelText = obj2.cancelText ?? "取消",
					confirmColor = obj2.confirmColor,
					showCancel = obj2.showCancel,
				};

				WX.ShowModal(opt);

				return ret.Task;
			}

			public override Task hideLaunchingView()
			{
				return Task.CompletedTask;
			}
		}
	}
#endif