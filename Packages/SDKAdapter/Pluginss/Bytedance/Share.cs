#if SUPPORT_BYTEDANCE
	using System;
	using GDK;
	using TTSDK;

	namespace BytedanceGDK
	{
		public static class ShareUtils
		{
			public static TTShare.ShareParam ToTTShareAppMessageParam(this GDK.ShareAppMessageParam paras)
			{
				return new TTShare.ShareParam(new TTSDK.UNBridgeLib.LitJson.JsonData(), null, null, null);
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