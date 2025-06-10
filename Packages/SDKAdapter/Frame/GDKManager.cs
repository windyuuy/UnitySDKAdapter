
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using EngineAdapter.Diagnostics;

namespace GDK
{

    public class GDKManager
    {
        protected Dictionary<string, PackConfig> _configMap = new();
        protected Dictionary<string, UserAPI> _pluginMap = new();
        public string defaultGDKName;

        /**
		 * 注册GDK插件配置
		 * @param name 插件名
		 * @param config 插件配置
		 */
        public void registPluginConfig(PackConfig config)
        {
            var name = config.name;
            Debug.Assert(!this._configMap.ContainsKey(name), $"config name {name} exists already!");
            this._configMap[name] = config;
            this.defaultGDKName = name;
        }

        /**
		 * 通过配置模板生成插件
		 */
        protected UserAPI genGdk(PackConfig config)
        {
            var temp = config.register();
            IModuleMap map = new ModuleMapDefault();
            List<IModule> addonList = new();
            var fields = temp.GetType().GetFields();
            foreach (var field in fields)
            {
                var pname = field.Name;
                var fValue = field.GetValue(temp);
                var invoke = fValue.GetType().GetMethod("Invoke");
                var retValue = invoke.Invoke(fValue, null);
                var field2 = map.GetType().GetProperty(pname, BindingFlags.Instance | BindingFlags.Public);
                field2.SetValue(map, retValue);
                if (retValue is IModule addon)
                {
                    addonList.Add(addon);
                }
            }

            {
                var metaInfo = map.MetaInfo;
                metaInfo.pluginName = config.name;
                metaInfo.pluginVersion = config.version;
                metaInfo.apiPlatformLocale = config.platform;
                metaInfo.apiPlatformLocale = config.platformLocale;
            }

            var api = new UserAPI(map);
            foreach (var addon in addonList)
            {
                addon.Api = map;
            }
            return api;
        }

        /**
		 * 设置默认插件
		 */
        public void setDefaultGdk(string name)
        {
            if (this._pluginMap.TryGetValue(name, out var api))
            {
                UserAPI.Instance = api;
            }
            else
            {
                Debug.LogError($"invalid api instance [{name}]");
            }
        }

        public UserAPI getPlugin(string name)
        {
            return this._pluginMap[name];
        }

        /**
		 * 初始化
		 */
        public void init()
        {
            foreach (var (k, v) in this._pluginMap)
            {
                var plugin = this.getPlugin(k);
                // 初始化插件内各个模块
                plugin.init();
            }
        }

        /**
		 * 传入配置并初始化
		 */
        public async Task initWithGDKConfig(GDKConfigV2 info)
        {
            foreach (var (k, v) in this._pluginMap)
            {
                var plugin = this.getPlugin(k);
                // 初始化插件内各个模块
                await plugin._initWithConfig(info);
            }
        }

        /**
		 * 创建插件对象，并注册
		 */
        public void instantiateGDKInstance()
        {
            foreach (var (k, v) in this._configMap)
            {
                var config = this._configMap[k];
                var plugin = this.genGdk(config);
                this._pluginMap[k] = plugin;
            }
        }
        public static readonly GDKManager Instance = new GDKManager();

    }

}
