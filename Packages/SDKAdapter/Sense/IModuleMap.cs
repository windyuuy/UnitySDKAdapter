namespace GDK
{
    public interface IModuleMap
    {
        /// <summary>
        /// 插件元信息
        /// </summary>
        public IMetaInfo MetaInfo { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public IUserData UserData { get; set; }
        /// <summary>
        /// 用户管理
        /// </summary>
        public IUser User { get; set; }
        /// <summary>
        /// 游戏信息
        /// </summary>
        public IGameInfo GameInfo { get; set; }
        /// <summary>
        /// 系统信息
        /// </summary>
        // public ISystemInfo systemInfo{get;set;}
        /// <summary>
        /// 系统管理
        /// </summary>
        public ISystemAPI SystemAPI{get;set;}
        /// <summary>
        /// 分享
        /// </summary>
        public IShare Share{get;set;}
        /// <summary>
        /// 支付
        /// </summary>
        // public IPay pay{get;set;}

        /// <summary>
        /// 原生广告V2
        /// </summary>
        public IAdvertV2 AdvertV2 { get; set; }
        public IFileSystem FileSystem { get; set; }
        public IStorage Storage { get; set; }
        public IDataAnalyze DataAnalyze { get; set; }

        /// <summary>
        /// 广告
        /// </summary>
        // public IAdvert advert{get;set;}
        /// <summary>
        /// 客服反馈
        /// </summary>
        // public ICustomer customer{get;set;}
        /// <summary>
        /// 基本UI组件
        /// </summary>
        public IWidgets Widgets{get;set;}
        /// <summary>
        /// 子域、排行榜相关
        /// </summary>
        // public ISubContext subContext{get;set;}
        /// <summary>
        /// 平台特性
        /// </summary>
        public ISupport Support{get;set;}
        /// <summary>
        /// 全局错误处理
        /// </summary>
        // public IExcept except{get;set;}
        /// <summary>
        /// 用户授权相关
        /// </summary>
        // public IAuth auth{get;set;}
        /// <summary>
        /// 硬件附加功能管理
        /// </summary>
        public IHardware Hardware{get;set;}
        /// <summary>
        /// 原生统计日志
        /// </summary>
        // public ILog log{get;set;}
        /// <summary>
        /// 本地推送通知
        /// </summary>
        // public ILocalPush localPush{get;set;}
        /// <summary>
        /// 热更
        /// </summary>
        // public IHotUpdate hotUpdate{get;set;}
        /// <summary>
        /// 热更
        /// </summary>
        // public IAppPay appPay{get;set;}
    }

    public class ModuleMapDefault : IModuleMap
    {
        public IMetaInfo MetaInfo { get; set; }
        public IUserData UserData { get; set; }
        public IUser User { get; set; }
        public IShare Share { get; set; }
        public IAdvertV2 AdvertV2 { get; set; }
        public IFileSystem FileSystem { get; set; }
        public IStorage Storage { get; set; }
        public IDataAnalyze DataAnalyze { get; set; }
        public IWidgets Widgets { get; set; }
        public IGameInfo GameInfo { get; set; }
        public ISupport Support { get; set; }
        public IHardware Hardware { get; set; }
        public ISystemAPI SystemAPI { get; set; }
    }
}
