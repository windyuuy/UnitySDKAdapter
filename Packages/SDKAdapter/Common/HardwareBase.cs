using System.Threading.Tasks;
using UnityEngine;

namespace GDK
{
	public class HardwareBase : IHardware
	{
		public IModuleMap Api { get; set; }

		public void Init()
		{
		}

		public Task InitWithConfig(GDKConfigV2 info)
		{
			return Task.CompletedTask;
		}

		public virtual DeviceInfo GetDeviceInfo()
		{
			return new DeviceInfo
			{
				abi = "unkown",
				benchmarkLevel = -1,
				brand = "pc",
				cpuType = SystemInfo.processorType,
				deviceAbi = "unknown",
				memorySize = $"{SystemInfo.systemMemorySize}B",
				model = SystemInfo.deviceModel,
				platform = Application.platform.ToString(),
				system = SystemInfo.operatingSystem,
			};
		}
	}
}