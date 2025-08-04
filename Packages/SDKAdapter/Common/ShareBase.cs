using System;
using System.Threading.Tasks;
using Lang.Loggers;

namespace GDK
{
	public class ShareBase: IShare
	{
		public Logger devlog = new Logger();

		public virtual IModuleMap Api { get; set; }
		public virtual void Init()
		{
			
		}

		public virtual Task InitWithConfig(GDKConfigV2 info)
		{
			return Task.CompletedTask;
		}

		public virtual void OnShareAppMessage(GDK.ShareAppMessageParam defaultParam,
			Func<GDK.ShareAppMessageParas, GDK.ShareAppMessageParam> action = null)
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