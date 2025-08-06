#if SUPPORT_WECHATGAME
using GDK;

namespace WechatGDK
{
	public class MetaInfo : IMetaInfo
	{
		public string pluginName { get; set; } = "wechatgame";
		public string pluginVersion { get; set; } = "1.0.0";
		public string apiPlatform { get; set; } = "wechatgame";
		public string apiPlatformLocale { get; set; } = "微信小游戏";
	}
}
#endif
