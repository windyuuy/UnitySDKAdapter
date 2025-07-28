using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeChatWASM;

namespace WechatGDK
{
	public static class ShareUtils
	{
		public static WXShareAppMessageParam ToWXShareAppMessageParam(this GDK.ShareAppMessageParam paras)
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

		public static GDK.ShareAppMessageParam ToShareAppMessageParam(this WXShareAppMessageParam paras)
		{
			return new GDK.ShareAppMessageParam
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
		public override void OnShareAppMessage(GDK.ShareAppMessageParam defaultParam,
			Action<Action<GDK.ShareAppMessageParam>> action = null)
		{
			WX.OnShareAppMessage(defaultParam.ToWXShareAppMessageParam(),
				(wxaction) =>
				{
					action?.Invoke(userparas => { wxaction.Invoke(userparas.ToWXShareAppMessageParam()); });
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
	}
}