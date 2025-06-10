using System.Collections.Generic;
using System.Threading.Tasks;

namespace GDK
{

    /**
	 * GDK元信息
	 */
    public class GDKFrameWorkMetaInfo
    {
        public string Version;
    }

    public class AppInfo
    {
        /**
		 * sdk的相关配置
		 */
        public AppSdkConfig[] SdkConfigs;
        /**
		 * 全局参数，往往存放一些自定义参数
		 */
        public Dictionary<string, object> Parameters;
    }


    public class GDKConfigV2
    {
        /**
		 * 游戏参数列表
		 */
        public AppInfo AppInfo;

        /**
		 * 游戏的版本号
		 */
        public string GameVersion;

    }

    /**
	 * 附件公共接口
	 */
    public interface IModule
    {
        public IModuleMap Api { get; set; }
        public void Init();
        /**
		 * 模块传入配置初始化入口
		 */
        public Task InitWithConfig(GDKConfigV2 info);
    }
}