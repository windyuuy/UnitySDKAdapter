#if SUPPORT_BYTEDANCE
	using System;
	using System.Threading.Tasks;
	using GDK;
	using TTSDK;
	using UnityEngine;

	namespace BytedanceGDK
	{
		public class KeyBoard : GDK.KeyBoardBase<TTKeyboard.OnKeyboardInputEvent, TTKeyboard.OnKeyboardConfirmEvent,
			TTKeyboard.OnKeyboardCompleteEvent>
		{
			public KeyBoard()
			{
				OnKeyboardInputHandler = (value) =>
				{
					if (OnKeyboardInputAction != null)
					{
						var result = new OnKeyboardInputResult
						{
							value = value,
						};
						OnKeyboardInputAction(result);
					}
				};

				OnKeyboardCompleteHandler = (value) =>
				{
					if (OnKeyboardCompleteAction != null)
					{
						var result = new OnKeyboardCompleteResult
						{
							value = value,
						};
						OnKeyboardCompleteAction(result);
					}
				};

				OnKeyboardConfirmHandler = (value) =>
				{
					if (OnKeyboardConfirmAction != null)
					{
						var result = new OnKeyboardConfirmResult
						{
							value = value,
						};
						OnKeyboardConfirmAction(result);
					}
				};
			}

			public override Task ShowKeyboard(GDK.ShowKeyboardOptions options)
			{
				var ts = new TaskCompletionSource<bool>();
				TT.ShowKeyboard(new TTKeyboard.ShowKeyboardOptions()
					{
						confirmHold = options.confirmHold,
						confirmType = options.confirmType,
						defaultValue = options.defaultValue,
						maxLength = (int)options.maxLength,
						multiple = options.multiple,
					}
					, () => { ts.SetResult(true); },
					(errMsg) => { ts.SetException(new GDKError(errMsg)); }
				);
				return ts.Task;
			}

			public override Task UpdateKeyboard(UpdateKeyboardOptions options)
			{
				var ts = new TaskCompletionSource<bool>();
				TT.UpdateKeyboard(options.value,
					() => { ts.SetResult(true); },
					(errMsg) => { ts.SetException(new GDKError(errMsg)); }
				);
				return ts.Task;
			}

			public override Task HideKeyboard()
			{
				var ts = new TaskCompletionSource<bool>();
				TT.HideKeyboard(() => { ts.SetResult(true); }, (errMsg) => { ts.SetException(new GDK.GDKError(errMsg)); });
				return ts.Task;
			}

			public override void OnKeyboardInputEvent(Action<OnKeyboardInputResult> callback)
			{
				if (OnKeyboardInputAction == null)
				{
					TT.OnKeyboardInput += OnKeyboardInputHandler;
				}

				OnKeyboardInputAction += callback;
			}

			public override void OffKeyboardInputEvent(Action<OnKeyboardInputResult> callback)
			{
				OnKeyboardInputAction += callback;

				if (OnKeyboardInputAction == null)
				{
					TT.OnKeyboardInput -= OnKeyboardInputHandler;
				}
			}

			public override void OnKeyboardConfirm(Action<OnKeyboardConfirmResult> callback)
			{
				if (OnKeyboardConfirmAction == null)
				{
					TT.OnKeyboardConfirm += OnKeyboardConfirmHandler;
				}

				OnKeyboardConfirmAction += callback;
			}

			public override void OffKeyboardConfirm(Action<OnKeyboardConfirmResult> callback)
			{
				OnKeyboardConfirmAction += callback;

				if (OnKeyboardConfirmAction == null)
				{
					TT.OnKeyboardConfirm -= OnKeyboardConfirmHandler;
				}
			}

			public override void OnKeyboardComplete(Action<OnKeyboardCompleteResult> callback)
			{
				if (OnKeyboardCompleteAction == null)
				{
					TT.OnKeyboardComplete += OnKeyboardCompleteHandler;
				}

				OnKeyboardCompleteAction += callback;
			}

			public override void OffKeyboardComplete(Action<OnKeyboardCompleteResult> callback)
			{
				OnKeyboardCompleteAction += callback;

				if (OnKeyboardCompleteAction == null)
				{
					TT.OnKeyboardComplete -= OnKeyboardCompleteHandler;
				}
			}
		}

		public class Widgets : GDK.WidgetsBase
		{
			public override GDK.IKeyBoard keyboard { get; } = new KeyBoard();

			public override Task ShowLoading(GDK.ShowLoadingParams obj2)
			{
				// return TTAdapter.SendRequestAsync<>()
				var ts = new TaskCompletionSource<ShowLoadingResult>();
				var sessionId = JsCallbackListener.Shared.ListenCallback<GDK.ShowLoadingResult>((resp) =>
				{
					ts.SetResult(resp);
				});
				var paraStr = JsonUtility.ToJson(obj2);
				TTAdapter.GDK_Bytedance_ShowLoading(sessionId, paraStr);
				return ts.Task;
			}

			public override Task HideLoading()
			{
				return TTAdapter.SendJsRequestAsync<ShowWidgetResult>(TTAdapter.GDK_Bytedance_HideLoading);
			}

			public override Task<ShowWidgetResult> ShowToast(ShowToastOptions obj2)
			{
				return TTAdapter.SendJsRequestAsync<ShowToastOptions, ShowWidgetResult>(TTAdapter.GDK_Bytedance_ShowToast,
					obj2);
			}

			public override Task hideToast()
			{
				return TTAdapter.SendJsRequestAsync<ShowWidgetResult>(TTAdapter.GDK_Bytedance_HideToast);
			}

			public override Task<GDK.ShowModalResult> ShowModal(GDK.ShowModalOptions obj2)
			{
				return TTAdapter.SendJsRequestAsync<GDK.ShowModalOptions, ShowModalResult>(TTAdapter.GDK_Bytedance_ShowModal,
					obj2);
			}

			public override Task hideLaunchingView()
			{
				return Task.CompletedTask;
			}
		}
	}
#endif