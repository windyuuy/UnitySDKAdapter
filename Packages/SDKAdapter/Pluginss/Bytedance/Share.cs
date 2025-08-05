#if SUPPORT_BYTEDANCE
	using System;
	using TTSDK;
	using TTSDK.UNBridgeLib.LitJson;
	using WeChatWASM;

	namespace BytedanceGDK
	{
		public static class ShareUtils
		{
			public static TTShare.ShareParam ToTTShareAppMessageParam(this GDK.ShareAppMessageParam paras)
			{
				return new TTShare.ShareParam(new JsonData(), null, null, null);
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
				Func<GDK.ShareAppMessageParas, GDK.ShareAppMessageParam> action = null)
			{
				TT.OnShareAppMessage((shareOption) =>
				{
					var result = action?.Invoke(new()
					{
						channel = shareOption.channel,
						webViewUrl = shareOption.webViewUrl,
					});
					return result.ToTTShareAppMessageParam();
				});
			}

			public override void OnShareTimeline(Action<Action<GDK.OnShareTimelineListenerResult>> callback)
			{
			#if UNITY_EDITOR
				throw new NotImplementedException();
			#else
				DevLog.Instance.Error($"{nameof(OnShareTimeline)} not support in ttsdk");
			#endif
			}

			public override void OnAddToFavorites(Action<Action<GDK.OnAddToFavoritesListenerResult>> callback)
			{
			#if UNITY_EDITOR
				throw new NotImplementedException();
			#else
				DevLog.Instance.Error($"{nameof(OnAddToFavorites)} not support in ttsdk");
			#endif
			}
		}
	}
#endif