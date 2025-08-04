using System.Threading.Tasks;

namespace GDK
{
    /**
	 * 初始入口
	 */
    public class FakeUserApi
    {
        public FakeUserApi Init()
        {
            GDKManager.Instance.InstantiateGDKInstance();
            GDKManager.Instance.Init();
            GDKManager.Instance.SetDefaultGdk(GDKManager.Instance.DefaultGDKName);
            return this;
        }

        public string pluginName
        {
            get
            {
                return GDKManager.Instance.DefaultGDKName;
            }
        }

        public async Task InitConfig(GDKConfigV2 config)
        {
            await GDKManager.Instance.InitWithGDKConfig(config);
        }

        public static FakeUserApi fakeGdk = new FakeUserApi();
    }

}
