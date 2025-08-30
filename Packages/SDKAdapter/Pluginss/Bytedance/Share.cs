#if SUPPORT_BYTEDANCE
	using System;
	using System.Threading.Tasks;
	using GDK;
	using TTSDK;
	using TTSDK.UNBridgeLib.LitJson;

	namespace BytedanceGDK
	{
		public static class ShareUtils
		{
			public static TTShare.ShareParam ToTTShareAppMessageParam(this GDK.OnShareAppMessageParam paras)
			{
				return new TTShare.ShareParam(new TTSDK.UNBridgeLib.LitJson.JsonData(), null, null, null);
			}
		}

		public class Share : GDK.ShareBase
		{
			public override void OnShareAppMessage(GDK.OnShareAppMessageParam defaultParam,
				Func<GDK.OnShareAppMessageParas, GDK.OnShareAppMessageParam> action = null)
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

			public override void ShareAppMessage(ShareAppMessageParas paras)
			{
				var shareJson = new JsonData();
				shareJson["imageUrl"] = paras.imageUrl;
				shareJson["query"] = paras.query;
				shareJson["title"] = paras.title;
				shareJson["desc"] = paras.desc;
				shareJson["templateId"] = paras.templateId;
				shareJson["toCurrentGroup"] = paras.toCurrentGroup;
				TT.ShareAppMessage(shareJson);
			}

			public override Task<ShowShareMenuResult> ShowShareMenu(ShowShareMenuParas paras)
			{
				var ts = new TaskCompletionSource<ShowShareMenuResult>();
				TT.ShowShareMenu(() =>
				{
					ts.SetResult(new ShowShareMenuResult
					{
						IsOk = true,
						ErrCode = 0,
						ErrMsg = "",
					});
				}, (resp) =>
				{
					ts.SetResult(new ShowShareMenuResult
					{
						IsOk = false,
						ErrCode = -1,
						ErrMsg = resp,
					});
				});

				return ts.Task;
			}
		}
	}
#endif