#if SUPPORT_WECHATGAME
using GDK;
using WeChatWASM;

namespace WechatGDK
{
	public class Hardware : HardwareBase
	{
		public override GDK.DeviceInfo GetDeviceInfo()
		{
			var deviceInfo = WX.GetDeviceInfo();
			return new GDK.DeviceInfo
			{
				abi = deviceInfo.abi,
				benchmarkLevel = deviceInfo.benchmarkLevel,
				brand = deviceInfo.brand,
				cpuType = deviceInfo.cpuType,
				deviceAbi = deviceInfo.deviceAbi,
				memorySize = deviceInfo.memorySize,
				model = deviceInfo.model,
				platform = deviceInfo.platform,
				system = deviceInfo.system,
			};
		}
	}
}
#endif
