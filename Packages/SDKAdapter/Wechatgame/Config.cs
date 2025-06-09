
using GDK;

namespace WechatGDK
{
	public class Config
	{
		public static void Register()
		{
			// default config
			GDKManager.Instance.registPluginConfig(new PackConfig()
			{
				name = "wechat",
				version = "1.0.5",
				platform = "wechatgame",
				platformLocale = "微信",
				register = () => new RegisterList(),
			});
		}

		public static void UseAsDefault()
		{
			GDKManager.Instance.setDefaultGdk("wechat");
		}
	}
}
