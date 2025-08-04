using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GDK
{
	public interface AppCallUpParams
	{
		/**
		 * 要打开的外部程序类型
		 * "MiniProgram" | "NativeApp"
		 */
		public string apptype { get; set; }

		/**
		 * 要打开的小程序 appId
		 **/
		public string appId { get; set; }

		/**
		 * 打开的页面路径，如果为空则打开首页
		 **/
		public string path { get; set; }

		/**
		 * 需要传递给目标小程序的数据，目标小程序可在 App.onLaunch，App.onShow 中获取到这份数据。
		 **/
		public Dictionary<string, string> extraData { get; set; }

		/**
		 * - 要打开的小程序版本。仅在当前小程序为开发版或体验版时此参数有效。如果当前小程序是正式版，则打开的小程序必定是正式版。
		 * - 默认值 release
		 **/
		public string envVersion { get; set; }

		public bool? noRelaunchIfPathUnchanged { get; set; }
		public string shortLink { get; set; }
	}

	public class AppCallUpResult
	{
		public string ErrMsg;
	}

	public class ClipboardData
	{
		public string data { get; set; }
	}

	public interface IClipboard
	{
		public Task<ClipboardData> GetData();
		public Task SetData(ClipboardData res);
	}

	public interface IChooseDialogParams
	{
		/**
		 * 提示内容
		 */
		public string content { get; set; }

		/**
		 * 是否弹系统确认框
		 */
		public bool toShowChoices { get; set; }
	}

	public interface IChooseDialogResult
	{
		/**
		 * 用户选择的动作
		 * - cancel
		 * - sure
		 */
		public string action { get; }

		/**
		 * 调用过程中是否崩溃
		 */
		public bool crashed { get; }
	}

	public class ChooseDialogResult : IChooseDialogResult
	{
		/**
		 * 用户选择的动作
		 * - cancel
		 * - sure
		 */
		public string action { get; set; }

		/**
		 * 调用过程中是否崩溃
		 */
		public bool crashed { get; set; }
	}

	public interface ICheckPermissionParams
	{
		/**
		 * 要检查的权限，支持 Manifest.permission.XXXXX 中对应的字符串
		 * - "android.permission.READ_PHONE_STATE"
		 * - "android.permission.WRITE_EXTERNAL_STORAGE"
		 * - "android.permission.ACCESS_FINE_LOCATION"
		 */
		public string[] permissions { get; }

		/**
		 * 如果有缺失的权限，同时试着申请
		 * @default false
		 */
		public bool requestAtSameTime { get; }
	}

	public interface IGetSDKMetaInfo
	{
		public string key { get; }
	}

	public class AppSdkConfig
	{
		/**
		 * sdk名称
		 */
		public string name;

		/**
		 * 需要禁用的功能
		 */
		public Dictionary<string, bool> disables;

		/**
		 * sdk中的参数列表
		 */
		public Dictionary<string, object> parameters;
	}

	public class CheckPermissionError
	{
		public string message;
		public string stack;
	}

	public interface ICheckPermissionResult
	{
		/**
		 * 缺失的权限列表
		 */
		public string[] lackedPermissions { get; }

		public CheckPermissionError error { get; }
	}

	public class CheckPermissionResult : ICheckPermissionResult
	{
		/**
		 * 缺失的权限列表
		 */
		public string[] lackedPermissions { get; set; }

		public CheckPermissionError error { get; set; }
	}

	/**
	 * 内存警告等级
	 */
	public enum MemoryWarningLevel
	{
		TRIM_MEMORY_RUNNING_MODERATE = 5,
		TRIM_MEMORY_RUNNING_LOW = 10,
		TRIM_MEMORY_RUNNING_CRITICAL = 15,
	}

	public interface IOnMemoryWarningResult
	{
		/**
		 * 内存告警等级，只有 Android 才有，对应系统宏定义
		 */
		public MemoryWarningLevel level { get; }
	}

	public class OnMemoryWarningResult : IOnMemoryWarningResult
	{
		/**
		 * 内存告警等级，只有 Android 才有，对应系统宏定义
		 */
		public MemoryWarningLevel level { get; set; }
	}

	public class SetEnableDebugOptions
	{
		public bool enableDebug;
	}

	public class GetSafeAreaResult
	{
		public double left;
		public double right;
		public double top;
		public double bottom;
	}

	public class SetLoadingProgressOptions
	{
		public double progress;
	}

	public class RestartMiniProgramOptions
	{
		/// <summary>
		/// 打开的页面路径，path 中 ? 后面的部分会成为 query
		/// </summary>
		public string path;
	}

	public class RestartMiniProgramResult
	{
		public bool IsOk;
	}

	public class LaunchOptionsReferrerInfo
	{
		/** 来源小程序、公众号或 App 的 appId */
		public object ExtraData;

		/** 来源小程序传过来的数据，scene=1037或1038时支持 */
		public string AppId;

		public GameLiveInfo GameLiveInfo;
	}

	public class GameLiveInfo
	{
		/// <summary>直播者 openId</summary>
		public string StreamerOpenId;

		/// <summary>直播间 id</summary>
		public string FeedId;
	}

	public class LaunchOptions
	{
		/** 打开小游戏的场景值 */
		public string Scene;

		/** 打开小游戏的启动参数 query */
		public Dictionary<string, string> Query;

		public string Path;
		public bool IsSticky;

		/** shareTicket，详见获取更多转发信息 */
		public string ShareTicket;

		/**
		 * 来源信息。从另一个小程序、公众号或 App 进入小程序时返回。否则返回 {}。(参见后文注意) *
		 * - 部分版本在无referrerInfo的时候会返回 undefined，建议使用 options.referrerInfo && options.referrerInfo.appId 进行判断。
		 **/
		public LaunchOptionsReferrerInfo ReferrerInfo;
	}

	public class ExitProgramOptions
	{
		public bool ShowDialog;
	}

	public class ExitProgramResult
	{
		public bool IsOk;
	}

	/**
	 * 支持各种系统调用、系统事件侦听
	 */
	public interface ISystemAPI : IModule
	{
		/**
		 * 跳转游戏
		 */
		public Task<AppCallUpResult> NavigateToApp(AppCallUpParams paras);

		/**
		 * 退出当前游戏
		 * <param name="paras"></param>
		 */
		public Task<ExitProgramResult> ExitProgram(ExitProgramOptions paras);

		/**
		 * 用法示例：
		 * ```typescript
		 * onShow((data)=>{
		 * 	...
		 * })
		 * ```
		 */
		public void OnShow(Action<object> callback);

		public void OffShow(Action<object> callback);

		/**
		* 用法示例：
		* ```typescript
		* onHide(()=>{
		* 	...
		* })
		* ```
		*/
		public void OnHide(Action<object> callback);

		public void OffHide(Action<object> callback);

		/**
		 * 强制更新
		 */
		public Task UpdateProgramForce();

		/**
		 * 设置是否打开调试开关。此开关对正式版也能生效。
		 */
		public Task SetEnableDebug(SetEnableDebugOptions res);

		/**
		 * - 设置帧率
		 * 	- 可能和cocos的会冲突
		 */
		public void GetFPS(int fps);

		/**
		 * 剪切板
		 */
		public IClipboard Clipboard { get; set; }

		/**
		 * 获取屏幕的安全区域，单位像素
		 * @param callback
		 */
		public void GetSafeArea(Action<GetSafeAreaResult> callback);

		// 设置加载进度
		public void SetLoadingProgress(SetLoadingProgressOptions paras);

		/**
		 * 网页跳转
		 * @param url
		 */
		public void OpenURL(string url);

		/**
		 * 开启云客服
		 */
		public void StartYunkefu(string accessId, string name, string id, object customField, bool native);

		/**
		 *
		 * 是否存在原生客服中心
		 */
		public bool HasNativeAssistantCenter();

		/**
		 * hack web
		 * @param url
		 */
		public void ShowHackWeb(string url, double duration);

		/**
		 * set native sdk language
		 * @param lang
		 */
		public void SetSDKLanguage(string lang);

		/**
		 * 原生版本号，具体看C++
		 */
		public double NativeVersion { get; }

		/**
		 * SDK框架版本
		 */
		public string SdkFrameworkVersion { get; }

		/**
		 * 跳转app设置界面
		 * - 目前只支持 android
		 */
		Task<IChooseDialogResult> GotoAppSystemSettings(IChooseDialogParams paras);

		/**
		 * 检查是否已授予权限
		 * - 目前只支持 android
		 */
		Task<ICheckPermissionResult> CheckAppSystemPermissions(ICheckPermissionParams paras);

		/**
		 * 通过key获取原生SDK版本信息
		 * @param params
		 */
		Task<object> GetSDKMetaInfo(IGetSDKMetaInfo paras);

		/**
		 * 初始化appinfo
		 * 最终的参数优先从 优先从外层parameters加载，如果找不到则从sdk模块中加载。
		 */
		public void InitAppinfo(AppInfo appInfo);

		/**
		 * 动态修改appInfo的值，仅在内存中生效，不会影响磁盘中的配置
		 * @param key
		 * @param value
		 */
		void SetAppInfo(string key, string value);

		void SetAppInfo(string key, double value);
		void SetAppInfo(string key, bool value);

		/**
		 * 获取应用AppInfo
		 * @param key
		 */
		public object GetAppInfo(string key);

		/**
		 * 获取bool类型的数据，当遇到异常数据时，将返回默认值
		 * @param key
		 * @param def
		 */
		public bool GetAppInfoBool(string key, bool def);

		/**
		  * 获取double类型的数据，当遇到异常数据时，将返回默认值
		  * @param key
		  * @param def
		  */
		public double GetAppInfoDouble(string key, double def);

		/**
		 * 获取String类型的数据，当遇到异常数据时，将返回默认值
		 * @param key
		 * @param def
		 */
		string GetAppInfoString(string key, string def);

		/**
		 * 监听内存不足告警事件。
		 */
		public void OnMemoryWarning(Action<IOnMemoryWarningResult> call);

		public void OffMemoryWarning(Action<IOnMemoryWarningResult> call);

		public Task<RestartMiniProgramResult> RestartMiniProgram(RestartMiniProgramOptions options);

		public LaunchOptions GetLaunchOptionsSync();
	}
}