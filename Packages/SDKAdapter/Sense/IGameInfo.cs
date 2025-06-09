using System.Collections.Generic;

namespace GDK
{

    public interface GetSystemInfoResult
    {
        /**
		 * 手机品牌
		 */
        public string brand { get; }

        /**
		 * 手机型号
		 */
        public string model { get; }

        /**
		 * 设备像素比
		 */
        public string pixelRatio { get; }

        /**
		 * 屏幕宽度
		 */
        public double screenWidth { get; }

        /**
		 * 屏幕高度
		 */
        public double screenHeight { get; }

        /**
		 * 窗口宽度
		 */
        public double windowWidth { get; }

        /**
		 * 窗口高度
		 */
        public double windowHeight { get; }

        /**
		 * 状态栏的高度	
		 */
        public double statusBarHeight { get; }

        /**
		 * 微信设置的语言
		 */
        public string language { get; }

        /**
		 * 微信版本号
		 */
        public string version { get; }

        /**
		 * 操作系统版本
		 */
        public string system { get; }

        /**
		 * 客户端平台
		 */
        public string platform { get; }

        /**
		 * 用户字体大小设置。以“我-设置-通用-字体大小”中的设置为准，单位 px。	
		 */
        public double fontSizeSetting { get; }

        /**
		 * 客户端基础库版本	
		 */
        public double SDKVersion { get; }

        /**
		 * (仅Android小游戏) 性能等级，-2 或 0：该设备无法运行小游戏，-1：性能未知，>=1 设备性能值，该值越高，设备性能越好 (目前设备最高不到50)	
		 */
        public double benchmarkLevel { get; }
    }

    public class LaunchOptionsReferrerInfo
    {
        /** 来源小程序、公众号或 App 的 appId */
        public object extraData;
        /** 来源小程序传过来的数据，scene=1037或1038时支持 */
        public string appId;
    }

    public class LaunchOptions
    {
        /** 打开小游戏的场景值 */
        public double scene;
        /** 打开小游戏的启动参数 query */
        public Dictionary<string, string> query;
        public string path;
        public bool isSticky;
        /** shareTicket，详见获取更多转发信息 */
        public string shareTicket;
        /**
		 * 来源信息。从另一个小程序、公众号或 App 进入小程序时返回。否则返回 {}。(参见后文注意) *
		 * - 部分版本在无referrerInfo的时候会返回 undefined，建议使用 options.referrerInfo && options.referrerInfo.appId 进行判断。
		 **/
        public LaunchOptionsReferrerInfo referrerInfo;
    }

    /**
	 * 游戏信息
	 */
    public interface IGameInfo : IModule
    {
        /**
         * 游戏的启动模式，可以是：
		 * - develop 开发
		 * - test 测试
		 * - release 发布
		 */
        public string mode { get; }

        /**
		 * 程序appid
		 */
        public string appId { get; }

        /**
		 * 游戏版本号
		 **/
        public string gameVersion { get; }
    }

}
