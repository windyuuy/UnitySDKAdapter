#if SUPPORT_WECHATGAME
	using System;
	using System.Threading.Tasks;
	using GDK;
	using WeChatWASM;
	using OnAddToFavoritesListenerResult = WeChatWASM.OnAddToFavoritesListenerResult;
	using OnShareTimelineListenerResult = WeChatWASM.OnShareTimelineListenerResult;

	namespace WechatGDK
	{
		public static class WXAsyncHelper
		{
			// public Task<TSuccess> GetTask<TSuccess, TFail, TComplete>(ICallback<TSuccess, TFail, TComplete> options)
			// {
			// 	var ts = new TaskCompletionSource<TSuccess>();
			// }
		}

		public static class ShareUtils
		{
			public static WXShareAppMessageParam ToWXShareAppMessageParam(this GDK.OnShareAppMessageParam paras)
			{
				return new WXShareAppMessageParam
				{
					title = paras.title,
					imageUrl = paras.imageUrl,
					query = paras.query,
					imageUrlId = paras.imageUrlId,
					toCurrentGroup = paras.toCurrentGroup,
					path = paras.path,
				};
			}

			public static GDK.OnShareAppMessageParam ToShareAppMessageParam(this WXShareAppMessageParam paras)
			{
				return new GDK.OnShareAppMessageParam
				{
					title = paras.title,
					imageUrl = paras.imageUrl,
					query = paras.query,
					imageUrlId = paras.imageUrlId,
					toCurrentGroup = paras.toCurrentGroup,
					path = paras.path,
				};
			}

			public static OnShareTimelineListenerResult ToWXOnShareTimelineListenerResult(
				this GDK.OnShareTimelineListenerResult paras)
			{
				return new OnShareTimelineListenerResult
				{
					imageUrl = paras.imageUrl,
					imagePreviewUrl = paras.imagePreviewUrl,
					imagePreviewUrlId = paras.imagePreviewUrlId,
					imageUrlId = paras.imageUrlId,
					path = paras.path,
					query = paras.query,
					title = paras.title,
				};
			}

			public static OnAddToFavoritesListenerResult ToWXOnAddToFavoritesListenerResult(
				this GDK.OnAddToFavoritesListenerResult paras)
			{
				return new OnAddToFavoritesListenerResult
				{
					disableForward = paras.disableForward,
					imageUrl = paras.imageUrl,
					query = paras.query,
					title = paras.title,
				};
			}
		}

		public class Share : GDK.ShareBase
		{
			public virtual void OnShareAppMessage(OnShareAppMessageParam defaultParam,
				Func<OnShareAppMessageParas, OnShareAppMessageParam> action = null)
			{
				WX.OnShareAppMessage(defaultParam.ToWXShareAppMessageParam(),
					(wxaction) =>
					{
						action?.Invoke(new GDK.OnShareAppMessageParas
						{
							call = (resp) => { wxaction.Invoke(resp.ToWXShareAppMessageParam()); },
							webViewUrl = null,
							channel = null
						});
					});
			}

			public override void OnShareTimeline(Action<Action<GDK.OnShareTimelineListenerResult>> callback)
			{
				WX.OnShareTimeline((wxaction) =>
				{
					callback?.Invoke((userparas) =>
					{
						wxaction.Invoke(userparas.ToWXOnShareTimelineListenerResult());
					});
				});
			}

			public override void OnAddToFavorites(Action<Action<GDK.OnAddToFavoritesListenerResult>> callback)
			{
				WX.OnAddToFavorites((wxaction) =>
				{
					callback?.Invoke((userparas) =>
					{
						wxaction.Invoke(userparas.ToWXOnAddToFavoritesListenerResult());
					});
				});
			}

			public override void ShareAppMessage(ShareAppMessageParas paras)
			{
				WX.ShareAppMessage(new ShareAppMessageOption
				{
					imageUrl = paras.imageUrl,
					imageUrlId = paras.imageUrlId,
					path = paras.path,
					query = paras.query,
					title = paras.title,
					toCurrentGroup = paras.toCurrentGroup,
				});
			}

			public override Task<ShowShareMenuResult> ShowShareMenu(ShowShareMenuParas paras)
			{
				var ts = new TaskCompletionSource<ShowShareMenuResult>();
				WX.ShowShareMenu(new ShowShareMenuOption
				{
					success = (resp) =>
					{
						ts.SetResult(new ShowShareMenuResult
						{
							IsOk = true,
							ErrCode = 0,
							ErrMsg = "",
						});
					},
					fail = (resp) =>
					{
						ts.SetResult(new ShowShareMenuResult
						{
							IsOk = false,
							ErrCode = -1,
							ErrMsg = resp.errMsg,
						});
					},
					menus = paras.menus,
					withShareTicket = paras.withShareTicket,
				});
				return ts.Task;
			}
		}
	}
#endif