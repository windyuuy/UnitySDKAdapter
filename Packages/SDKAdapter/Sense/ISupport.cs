namespace GDK
{
	/**
	 * 各种功能支持项配置
	 */
	public interface ISupport
	{
		/** 是否支持分享 */
		public bool supportShare { get; set; }
		/** 是否支持群分享 */
		public bool supportShareTickets { get; set; }
		/** 是否需要支持子域 */
		public bool requireSubDomainRank { get; set; }
		/** 是否需要鉴权认证 */
		public bool requireAuthorize { get; set; }

		/**
		 * 内部是否已经集成打点
		 */
		public bool supportBuiltinCommitLog { get; set; }

		/**
		 * 是否已集成在线时长打点
		 */
		public bool supportBuiltinOnlineLoopLog { get; set; }

		/**
		 * 是否自带实名认证
		 */
		public bool supportBuiltinIdentityCertification { get; set; }

		/**
		 * 是否需要自己维护广告生命周期
		 * （部分小游戏平台需要自己维护）
		 */
		public bool requireManagerAdLifecycle { get; set; }

		/**
		 * 是否是原生插件
		 */
		public bool isNativePlugin { get; set; }

	}
}