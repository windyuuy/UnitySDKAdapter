namespace GDK
{
	public class DeviceInfo
	{
		/// <summary>应用（微信APP）二进制接口类型（仅 Android 支持）</summary>
		public string abi;

		public double benchmarkLevel;

		/// <summary>设备品牌</summary>
		public string brand;

		/// <summary>
		/// 需要基础库： `2.29.0`
		/// 设备 CPU 型号（仅 Android 支持）（Tips: GPU 型号可通过 WebGLRenderingContext.getExtension('WEBGL_debug_renderer_info') 来获取）
		/// </summary>
		public string cpuType;

		/// <summary>
		/// 需要基础库： `2.25.1`
		/// 设备二进制接口类型（仅 Android 支持）
		/// </summary>
		public string deviceAbi;

		/// <summary>
		/// 需要基础库： `2.30.0`
		/// 设备内存大小，单位为 MB
		/// </summary>
		public string memorySize;

		/// <summary>设备型号。新机型刚推出一段时间会显示unknown，微信会尽快进行适配。</summary>
		public string model;

		/// <summary>客户端平台</summary>
		public string platform;

		/// <summary>操作系统及版本</summary>
		public string system;
	}

	public interface IHardware : IModule
	{
		public DeviceInfo GetDeviceInfo();
	}
}