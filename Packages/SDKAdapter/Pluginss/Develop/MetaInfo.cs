using GDK;

namespace DevelopGDK
{
	public class MetaInfo : IMetaInfo
	{
		public string pluginName { get; set; } = "develop";
		public string pluginVersion { get; set; } = "1.0.0";
		public string apiPlatform { get; set; } = "develop";
		public string apiPlatformLocale { get; set; } = "开发版";
	}
}