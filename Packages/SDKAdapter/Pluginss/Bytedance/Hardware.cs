#if SUPPORT_BYTEDANCE
using GDK;
using TTSDK;

namespace BytedanceGDK
{
	public class Hardware : HardwareBase
	{
		public override GDK.DeviceInfo GetDeviceInfo()
		{
			var deviceInfo = TT.GetSystemInfo();
			return new GDK.DeviceInfo
			{
				brand = deviceInfo.brand,
				model = deviceInfo.model,
				platform = deviceInfo.platform,
				system = deviceInfo.system,
				hostName = deviceInfo.hostName,
				hostVersion = deviceInfo.hostVersion,
			};
		}
	}
}
#endif
