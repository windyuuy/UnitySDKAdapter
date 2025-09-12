using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GDK
{
	public class DeviceMotionBase : IDeviceMotion
	{
		public virtual Task<bool> Start(StartDeviceMotionOptions options)
		{
			return Task.FromResult(false);
		}

		public virtual Task<bool> Stop()
		{
			return Task.FromResult(false);
		}

		public virtual event Action<DoubleVector3> OnChange;
	}

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

		public virtual IDeviceMotion DeviceMotion { get; } = new DeviceMotionBase();
	}
}