using System.Threading.Tasks;
using UnityEngine;

namespace GDK
{
	// 自动生成，成员使用register函数注册
	public class UserAPI
	{
		public static UserAPI Instance;

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
				Version = "1.0.9",
			};
			return info;
		}

		protected readonly GDKFrameWorkMetaInfo MetaInfo = getGDKMetaInfo();

		/**
		 * gdk的框架版本号
		 **/
		public string GdkVersion =>
			this.MetaInfo.Version;

		public void Init()
		{
			DevLog.Instance.Warn("redundant init for gdk, skipped");
		}

		protected bool BeInitConfigOnce = false;

		public async Task InitConfig(GDKConfigV2 config)
		{
			if (this.BeInitConfigOnce)
			{
				DevLog.Instance.Warn("redundant initConfig for gdk, skipped");
			}
			else
			{
				this.BeInitConfigOnce = true;
				await GDKManager.Instance.InitWithGDKConfig(config);
			}
		}

		/**
		 * 初始化插件内各个模块
		 * @param info 外部传入的配置
		 */
		internal void _init()
		{
			Debug.Log($"Plugin::_init: {this.PluginName}");
			var fields = this._m.GetType().GetFields();
			foreach (var field in fields)
			{
				// 初始化广告等具体模块
				var addon = field.GetValue(this._m) as IModule;
				// if (addon.init)
				{
					Debug.Log($"addon::Init {field.Name}");
					addon.Init();
					Debug.Log($"addon::Init-done {field.Name}");
				}
			}
			Debug.Log($"Plugin::_init-done: {this.PluginName}");
		}

		/**
		 * 初始化插件内各个模块
		 * @param info 外部传入的配置
		 */
		internal async Task _initWithConfig(GDKConfigV2 info)
		{
			DevLog.Instance.Log($"Init Plugin: {this.PluginName}");
			await this._m.GameInfo.InitWithConfig(info);
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
					await addon.InitWithConfig(info);
				}
			}
		}

		protected bool CheckModuleAttr(
			string moduleName,
			string attrName,
			string attrType = null
		)
		{
			return true;
		}

		public bool IsSupport(
			string moduleName,
			string attrName,
			string attrType = null
		)
		{
			return this.CheckModuleAttr(moduleName, attrName, attrType);
		}

		/** 当前实际平台 */
		public string RuntimePlatform { get; }

		public IUserData UserData => this._m.UserData;
		public IAdvertV2 AdvertV2 => this._m.AdvertV2;
		public IFileSystem FileSystem => this._m.FileSystem;
		public IShare Share => this._m.Share;
		public IStorage Storage => this._m.Storage;
		public IDataAnalyze DataAnalyze => this._m.DataAnalyze;
		public IWidgets Widgets => this._m.Widgets;
		public ISystemAPI SystemAPI => this._m.SystemAPI;
		public IGameInfo GameInfo => this._m.GameInfo;
		public IUser User => this._m.User;
		public ISupport Support => this._m.Support;
		public IHardware Hardware => this._m.Hardware;

		/** 批量导出接口 */
		// $batch_export() begin
		/**
		 * 插件名
		 * * develop 网页开发测试
		 * * wechat 微信
		 * * qqplay 玩一玩
		 * * app 原生APP
		 **/
		public string PluginName
		{
			get
			{
				if (!this.CheckModuleAttr("metaInfo", "pluginName"))
				{
					return null;
				}

				return this._m.MetaInfo.pluginName;
			}
		}

		/**
		 * 插件版本
		 */
		public string PluginVersion
		{
			get
			{
				if (!this.CheckModuleAttr("metaInfo", "pluginVersion"))
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
		public string ApiPlatform
		{
			get
			{
				if (!this.CheckModuleAttr("metaInfo", "apiPlatform"))
				{
					return null;
				}

				return this._m.MetaInfo.apiPlatform;
			}
		}

		/** 本地化api平台名 */
		public string ApiPlatformLocale
		{
			get
			{
				if (!this.CheckModuleAttr("metaInfo", "apiPlatformLocale"))
				{
					return null;
				}

				return this._m.MetaInfo.apiPlatformLocale;
			}
		}

		public string OpenId
		{
			get
			{
				if (!this.CheckModuleAttr("userData", "openId"))
				{
					return null;
				}

				return this._m.UserData.openId;
			}
		}
	}
}