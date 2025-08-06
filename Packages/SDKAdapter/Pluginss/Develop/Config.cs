#if UNITY_EDITOR

using GDK;

namespace DevelopGDK
{
	public class Config
	{
		/// <summary>
		/// Registers the development plugin configuration with default values.
		/// </summary>
		/// <remarks>
		/// This method registers a plugin configuration named "develop" with version "1.0.4",
		/// targeting the "browser" platform with localized name "开发模式" (Development Mode).
		/// The registration uses the provided RegisterList for initialization.
		/// </remarks>
		public static void Register()
		{
			// default config
			GDKManager.Instance.RegistPluginConfig(new()
			{
				name = "develop",
				version = "1.0.4",
				platform = "browser",
				platformLocale = "开发模式",
				register = () => new RegisterList(),
			});
		}

		public static void UseAsDefault()
		{
			GDKManager.Instance.SetDefaultGdk("develop");
		}
	}
}
#endif
