#if SUPPORT_WECHATGAME
using System;
using System.Threading.Tasks;
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
					success = (resp) =>
					{
						ts.SetResult(true);
					},
					fail = (resp) =>
					{
						ts.SetException(new GDK.GDKError(resp.errMsg));
					},
				});
				return ts.Task;
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
				throw GDK.ResultTemplatesExtractor.GDKResultTemplates.make<GDK.GDKErrorExtra>(code);
			}
		}

		public static Task wrapReq<TC>(Action<TC> func, TC req, GDK.GDKErrorCode code)
			where TC : ICallback<GeneralCallbackResult, GeneralCallbackResult, GeneralCallbackResult>, new()
		{
			try
			{
				var ts = new TaskCompletionSource<bool>();
				req.success = (resp) =>
					{
						ts.SetResult(true);
					};
				req.fail = (resp) =>
				{
					ts.SetException(new GDK.GDKError(resp.errMsg));
				};
				func(req);
				return ts.Task;
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
				throw GDK.ResultTemplatesExtractor.GDKResultTemplates.make(code, exception);
			}
		}
	}
	public class KeyBoard : GDK.IKeyBoard
	{
		public virtual Task hideKeyboard()
		{
			return WXReqHelper.wrapReq<HideKeyboardOption>(WX.HideKeyboard, GDK.GDKErrorCode.API_HIDE_KEYBOARD_FAILED);
		}
	}

	public class Widgets : GDK.WidgetsBase
	{
		public override GDK.IKeyBoard keyboard { get; } = new KeyBoard();
		public override Task showLoading(GDK.ShowLoadingParams obj2)
		{
			return WXReqHelper.wrapReq<ShowLoadingOption>(WX.ShowLoading, new()
			{
				title = obj2.title,
				mask = obj2.mask,
			}, GDK.GDKErrorCode.API_SHOW_LOADING_FAILED);
		}
		public override Task hideLoading()
		{
			return WXReqHelper.wrapReq<HideLoadingOption>(WX.HideLoading, GDK.GDKErrorCode.API_HIDE_LOADING_FAILED);
		}
		public override Task showToast(GDK.ShowToastOptions obj2)
		{
			return WXReqHelper.wrapReq<ShowToastOption>(WX.ShowToast, new ShowToastOption()
			{
				duration = obj2.duration,
				title = obj2.title,
			}, GDK.GDKErrorCode.API_SHOW_TOAST_FAILED);
		}
		public override Task hideToast()
		{
			return WXReqHelper.wrapReq<HideToastOption>(WX.HideToast, GDK.GDKErrorCode.API_HIDE_TOAST_FAILED);
		}
		public override Task<GDK.ShowConfirmResult> showConfirm(GDK.ShowConfirmOptions obj2)
		{

			var ret = new TaskCompletionSource<GDK.ShowConfirmResult>();

			var opt = new ShowModalOption()
			{
				success = (data) =>
				{
					var result = new GDK.ShowConfirmResult()
					{
						confirm = data.confirm,
						cancel = data.confirm,
						extra = data,
					};
					ret.SetResult(result);
				},
				fail = (resp) =>
				{
					UnityEngine.Debug.LogError(resp.errMsg);
					ret.SetException(GDK.ResultTemplatesExtractor.GDKResultTemplates.make(GDK.GDKErrorCode.API_SHOW_MODAL_FAILED, new GDK.GDKErrorExtra(resp.errMsg)));
				},
				title = obj2.title,
				content = obj2.content,
				confirmText = obj2.okLabel ?? "确定",
				cancelText = obj2.cancelLabel ?? "取消",
			};

			WX.ShowModal(opt);

			return ret.Task;

		}

		public override Task<GDK.ShowAlertResult> showAlert(GDK.ShowAlertOptions obj2)
		{
			var ret = new TaskCompletionSource<GDK.ShowAlertResult>();

			var opt = new ShowModalOption()
			{
				success = (data) =>
					{
						var result = new GDK.ShowAlertResult();
						result.extra = data;
						ret.SetResult(result);
					},
				fail = (resp) =>
						{
							ret.SetException(GDK.ResultTemplatesExtractor.GDKResultTemplates.make(GDK.GDKErrorCode.API_SHOW_MODAL_FAILED, new GDK.GDKErrorExtra(resp.errMsg)));
						},
			};

			opt.title = obj2.title;
			opt.content = obj2.content;
			opt.confirmText = obj2.okLabel ?? "确定";
			opt.showCancel = false;

			WX.ShowModal(opt);

			return ret.Task;
		}

		public override Task<GDK.ShowPromptResult> showPrompt(GDK.ShowPromptOptions obj2)
		{
			return null;
		}

		public override Task hideLaunchingView()
		{
			return Task.CompletedTask;
		}

	}
}
#endif
