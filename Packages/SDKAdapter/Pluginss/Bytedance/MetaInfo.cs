#if SUPPORT_BYTEDANCE
using GDK;

namespace BytedanceGDK
{
	public class MetaInfo : IMetaInfo
	{
		public string pluginName { get; set; } = "Bytedance";
		public string pluginVersion { get; set; } = "1.0.0";
		public string apiPlatform { get; set; } = "bytedance";
		public string apiPlatformLocale { get; set; } = "抖音小游戏";
	}
}
#endif
