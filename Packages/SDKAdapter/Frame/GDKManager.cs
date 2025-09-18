
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
        public string DefaultGDKName;

        /**
		 * 注册GDK插件配置
		 * @param name 插件名
		 * @param config 插件配置
		 */
        public void RegistPluginConfig(PackConfig config)
        {
            var name = config.name;
            Debug.Assert(!this._configMap.ContainsKey(name), $"config name {name} exists already!");
            this._configMap[name] = config;
            this.DefaultGDKName = name;
        }

        /**
		 * 通过配置模板生成插件
		 */
        protected UserAPI GenGdk(PackConfig config)
        {
                Debug.Log($"GenGdk: {config.name}");
            var temp = config.register();
            IModuleMap map = new ModuleMapDefault();
            
            List<IModule> addonList = new();
            var fields = temp.GetType().GetFields();
            foreach (var field in fields)
            {
                var pname = field.Name;
                Debug.Log($"add addon: {pname}");
                var fValue = field.GetValue(temp);
                var invoke = fValue.GetType().GetMethod("Invoke");
                var retValue = invoke.Invoke(fValue, null);
                var field2 = map.GetType().GetProperty(pname, BindingFlags.Instance | BindingFlags.Public);
                field2.SetValue(map, retValue);
                if (retValue is IModule addon)
                {
                    addonList.Add(addon);
                }
                Debug.Log($"add addon-done: {config.name}.{pname}");
            }

            // MetaInfo 也是 addon, 不能提前到 addon 加载前
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
                Debug.Log($"GenGdk-done: {config.name}");
            return api;
        }

        /**
		 * 设置默认插件
		 */
        public void SetDefaultGdk(string name)
        {
            if (this._pluginMap.TryGetValue(name, out var api))
            {
                Debug.Log($"SetDefaultGdk: {name}");
                UserAPI.Instance = api;
            }
            else
            {
                DevLog.Instance.Error($"invalid api instance [{name}]");
            }
        }

        public UserAPI GetPlugin(string name)
        {
            return this._pluginMap[name];
        }

        /**
		 * 初始化
		 */
        public void Init()
        {
            foreach (var (k, v) in this._pluginMap)
            {
                var plugin = this.GetPlugin(k);
                // 初始化插件内各个模块
                plugin._init();
            }
        }

        /**
		 * 传入配置并初始化
		 */
        public async Task InitWithGDKConfig(GDKConfigV2 info)
        {
		    Debug.Log("GDKManager::InitConfig");
            foreach (var (k, v) in this._pluginMap)
            {
                var plugin = this.GetPlugin(k);
                // 初始化插件内各个模块
                Debug.Log("plugin._initWithConfig");
                await plugin._initWithConfig(info);
            }
		    Debug.Log("GDKManager::InitConfig-done");
        }

        /**
		 * 创建插件对象，并注册
		 */
        public void InstantiateGDKInstance()
        {
            foreach (var (k, v) in this._configMap)
            {
                var config = this._configMap[k];
                var plugin = this.GenGdk(config);
                this._pluginMap[k] = plugin;
            }
        }
        public static readonly GDKManager Instance = new GDKManager();

    }

}
