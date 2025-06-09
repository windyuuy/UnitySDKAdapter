using System.Threading.Tasks;
using Lang.Loggers;

namespace GDK
{
    // 自动生成，成员使用register函数注册
    public class UserAPI
    {
        public static UserAPI Instance;
        public Logger devlog = new Logger();
        /**
		 * 附件map
		 */
        private IModuleMap _m;
        public UserAPI(IModuleMap moduleMap)
        {
            this._m = moduleMap;
        }

        /**
         * 获取GDK元信息
         */
        public static GDKFrameWorkMetaInfo getGDKMetaInfo()
        {
            GDKFrameWorkMetaInfo info = new GDKFrameWorkMetaInfo()
            {
                version = "1.0.9",
            };
            return info;
        }
        protected readonly GDKFrameWorkMetaInfo metaInfo = getGDKMetaInfo();

        /**
		 * gdk的框架版本号
		 **/
        public string gdkVersion =>
             this.metaInfo.version;

        public void init()
        {
            devlog.Warn("redundant init for gdk, skipped");
        }

        protected bool beInitConfigOnce = false;
        public async Task initConfig(GDKConfigV2 config)
        {
            if (this.beInitConfigOnce)
            {
                devlog.Warn("redundant initConfig for gdk, skipped");
            }
            else
            {
                this.beInitConfigOnce = true;
                await GDKManager.Instance.initWithGDKConfig(config);
            }
        }

        /**
		 * 初始化插件内各个模块
		 * @param info 外部传入的配置
		 */
        protected void _init()
        {
            var fields = this._m.GetType().GetFields();
            foreach (var field in fields)
            {
                // 初始化广告等具体模块
                var addon = field.GetValue(this._m) as IModule;
                // if (addon.init)
                {
                    addon.init();
                }
            }
        }

        /**
		 * 初始化插件内各个模块
		 * @param info 外部传入的配置
		 */
        internal async Task _initWithConfig(GDKConfigV2 info)
        {
            await this._m.GameInfo.initWithConfig(info);
            var fields = this._m.GetType().GetProperties();
            foreach (var field in fields)
            {
                if (field.Name == nameof(IGameInfo).Substring(1))
                {
                    continue;
                }
                // 初始化广告等具体模块
                var retValue = field.GetValue(this._m);
                if (retValue is IModule addon)
                {
                    await addon.initWithConfig(info);
                }
            }
        }

        protected bool checkModuleAttr(
              string moduleName,
              string attrName,
              string attrType = null
        )
        {
            return true;
        }

        public bool support(
             string moduleName,
             string attrName,
             string attrType = null
         )
        {
            return this.checkModuleAttr(moduleName, attrName, attrType);
        }

        /** 当前实际平台 */
        public string runtimePlatform { get; }
        public IUserData userData =>
             this._m.UserData;
        public IAdvertV2 advertV2 =>
             this._m.AdvertV2;

        /** 批量导出接口 */
        // $batch_export() begin
        /**
		 * 插件名
		 * * develop 网页开发测试
		 * * wechat 微信
		 * * qqplay 玩一玩
		 * * app 原生APP
		 **/
        public string pluginName
        {
            get
            {
                if (!this.checkModuleAttr("metaInfo", "pluginName"))
                {
                    return null;
                }
                return this._m.MetaInfo.pluginName;
            }
        }
        /**
		 * 插件版本
		 */
        public string pluginVersion
        {
            get
            {
                if (!this.checkModuleAttr("metaInfo", "pluginVersion"))
                {
                    return null;
                }
                return this._m.MetaInfo.pluginVersion;
            }
        }
        /**
		 * api平台名称
		 * * browser 浏览器
		 * * native APP原生
		 * * wechatgame 微信
		 * * qqplay QQ玩一玩
		 * * unknown 未知平台
		 */
        public string apiPlatform
        {
            get
            {
                if (!this.checkModuleAttr("metaInfo", "apiPlatform"))
                {
                    return null;
                }
                return this._m.MetaInfo.apiPlatform;
            }
        }
        /** 本地化api平台名 */
        public string apiPlatformLocale
        {
            get
            {
                if (!this.checkModuleAttr("metaInfo", "apiPlatformLocale"))
                {
                    return null;
                }
                return this._m.MetaInfo.apiPlatformLocale;
            }
        }

        public string openId
        {
            get
            {
                if (!this.checkModuleAttr("userData", "openId"))
                {
                    return null;
                }
                return this._m.UserData.openId;
            }
        }

    }
}
