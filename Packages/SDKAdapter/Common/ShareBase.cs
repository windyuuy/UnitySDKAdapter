using System;
using System.Threading.Tasks;

namespace GDK
{
	public class ShareBase: IShare
	{
		public virtual IModuleMap Api { get; set; }
		public virtual void Init()
		{
			
		}

		public virtual Task InitWithConfig(GDKConfigV2 info)
		{
			return Task.CompletedTask;
		}

		public virtual void OnShareAppMessage(ShareAppMessageParam defaultParam, Action<Action<ShareAppMessageParam>> action = null)
		{
			
		}

		public virtual void OnShareTimeline(Action<Action<OnShareTimelineListenerResult>> callback)
		{
			
		}

		public virtual void OnAddToFavorites(Action<Action<OnAddToFavoritesListenerResult>> callback)
		{
			
		}
	}
}