using System;

namespace GDK
{
    /**
	 * 插件附件注册列表
	 */
    public class ModuleClassMap
    {
        public Func<IMetaInfo> MetaInfo = () => new MetaInfoBase();
        // public Func<IAdvert> Advert;
        public Func<IGameInfo> GameInfo;
        public Func<IUser> User;
        // public Func<IPay> Pay;
        // public Func<IShare> Share;   
        // public Func<ISystemInfo> SystemInfo;
        public Func<ISystemAPI> SystemAPI = () => new SystemAPIBase();
        public Func<IUserData> UserData;
        // public Func<ICustomer> Customer;
        public Func<IWidgets> Widgets;
        // public Func<ISubContext> SubContext;
        public Func<ISupport> Support;
        // public Func<IExcept> Except;
        // public Func<IAuth> Auth;
        // public Func<IHardware> Hardware = () => new HardwareBase();
        // public Func<ILog> Log = () => new LogBase();
        // public Func<ILocalPush> LocalPush;
        public Func<IAdvertV2> AdvertV2;
        // public Func<IHotUpdate> HotUpdate;
        // public Func<IAppPay> AppPay;
    }
}
