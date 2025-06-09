using System;

namespace GDK {
	/**
	 * 插件包注册信息
	 */
	public class PackConfig {
		/**
		 * 插件名
		 */
		public string name;
		/**
		 * api平台名称
		 * * browser 浏览器
		 * * native APP原生
		 * * wechatgame 微信
		 * * qqplay QQ玩一玩
		 * * unknown 未知平台
		*/
		public  string platform;
		/** 本地化api平台名 */
		public string platformLocale;
		/** sdk版本号 */
		public  string version;
        /**
		 * 插件附件列表注册信息
		 */
        public Func<ModuleClassMap> register;
	}

}