namespace GDK {
	/**
	 * 插件元信息
	 */
	public interface IMetaInfo {
		/**
		* 插件名
		* * develop 网页开发测试
		* * wechat 微信
		* * qqplay 玩一玩
		* * app 原生APP
		**/
		public string pluginName{get;set;}

		/**
		 * 插件版本
		 */
		public string pluginVersion{get;set;}

		/** 
		 * api平台名称 
		 * * browser 浏览器
		 * * native APP原生
		 * * wechatgame 微信
		 * * qqplay QQ玩一玩
		 * * unknown 未知平台
		*/
		public string apiPlatform{get;set;}

		/** 本地化api平台名 */
		public string apiPlatformLocale{get;set;}
		
	}
}