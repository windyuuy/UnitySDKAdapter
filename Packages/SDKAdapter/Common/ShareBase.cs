using System;
using System.Threading.Tasks;
using Lang.Loggers;
using UnityEngine;

namespace GDK
{
	public class ShareBase : IShare
	{
		public virtual IModuleMap Api { get; set; }

		public virtual void Init()
		{
		}

		public virtual Task InitWithConfig(GDKConfigV2 info)
		{
			return Task.CompletedTask;
		}

		public virtual void OnShareAppMessage(GDK.OnShareAppMessageParam defaultParam,
			Func<GDK.OnShareAppMessageParas, GDK.OnShareAppMessageParam> action = null)
		{
		}

		public virtual void OnShareTimeline(Action<Action<OnShareTimelineListenerResult>> callback)
		{
		}

		public virtual void OnAddToFavorites(Action<Action<OnAddToFavoritesListenerResult>> callback)
		{
		}

		public virtual void ShareAppMessage(ShareAppMessageParas paras)
		{
			Debug.Log($"调用分享");
		}

		public virtual Task<ShowShareMenuResult> ShowShareMenu(ShowShareMenuParas paras)
		{
			Debug.Log($"显示分享");
			return Task.FromResult(new ShowShareMenuResult
			{
				IsOk = true,
				ErrCode = 0,
				ErrMsg = "",
			});
		}
	}
}