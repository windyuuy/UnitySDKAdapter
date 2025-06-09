using System.Threading.Tasks;

namespace GDK
{

    /**
	 * GDK元信息
	 */
    public class GDKFrameWorkMetaInfo
    {
        public string version;
    }

    public struct AppInfo
    {
    }


    public class GDKConfigV2
    {
        /**
		 * 游戏参数列表
		 */
        public AppInfo appInfo;

        /**
		 * 游戏的版本号
		 */
        public string gameVersion;

    }

    /**
	 * 附件公共接口
	 */
    public interface IModule
    {
        public IModuleMap api { get; set; }
        public void init();
        /**
		 * 模块传入配置初始化入口
		 */
        public Task initWithConfig(GDKConfigV2 info);
    }
}