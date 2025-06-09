
namespace GDK
{
	public class MetaInfoBase : IMetaInfo
	{
		public string pluginName { get; set; } = "unkown";
		public string pluginVersion { get; set; } = "unkown";
		public string apiPlatform { get; set; } = "unkown";
		public string apiPlatformLocale { get; set; } = "unkown";
	}
}
