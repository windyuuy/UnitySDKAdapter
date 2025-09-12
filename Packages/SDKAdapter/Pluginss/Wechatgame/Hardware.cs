#if SUPPORT_WECHATGAME
	using System;
	using System.Threading.Tasks;
	using GDK;
	using UnityEngine;
	using WeChatWASM;

	namespace WechatGDK
	{
		public class DeviceMotion : IDeviceMotion
		{
			public Task<bool> Start(StartDeviceMotionOptions options)
			{
				var ts = new TaskCompletionSource<bool>();
				WX.StartDeviceMotionListening(new StartDeviceMotionListeningOption
				{
					success = (resp) => { ts.SetResult(true); },
					fail = (resp) => { ts.SetResult(false); },
					interval = options.IntervalType,
				});
				return ts.Task;
			}

			public Task<bool> Stop()
			{
				var ts = new TaskCompletionSource<bool>();
				WX.StopDeviceMotionListening(new StopDeviceMotionListeningOption
				{
					success = (resp) => { ts.SetResult(true); },
					fail = (resp) => { ts.SetResult(false); },
				});
				return ts.Task;
			}

			protected EventProxy<DoubleVector3, Action<OnDeviceMotionChangeListenerResult>> OnChangeEvent = new(action =>
			{
				void Handle(OnDeviceMotionChangeListenerResult result)
				{
					action(new DoubleVector3(result.gamma, result.beta, result.alpha));
				}

				WX.OnDeviceMotionChange(Handle);
				return Handle;
			}, cleanId=>WX.OffDeviceMotionChange(cleanId));
			public event Action<DoubleVector3> OnChange
			{
				add => OnChangeEvent.Add(value);
				remove => OnChangeEvent.Remove(value);
			}
		}

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

			public override IDeviceMotion DeviceMotion { get; } = new DeviceMotion();
		}
	}
#endif