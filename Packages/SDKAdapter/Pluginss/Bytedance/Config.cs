
#if SUPPORT_BYTEDANCE
using GDK;

namespace BytedanceGDK
{
	public class Config
	{
		internal static string PlatformName = "bytedance";
		public static void Register()
		{
			// default config
			GDKManager.Instance.RegistPluginConfig(new PackConfig()
			{
				name = PlatformName,
				version = "1.0.0",
				platform = PlatformName,
				platformLocale = "抖音",
				register = () => new RegisterList(),
			});
		}

		public static void UseAsDefault()
		{
			GDKManager.Instance.SetDefaultGdk(PlatformName);
		}
	}
}
#endif
