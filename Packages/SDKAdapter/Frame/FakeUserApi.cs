using System.Threading.Tasks;

namespace GDK
{
    /**
	 * 初始入口
	 */
    public class FakeUserApi
    {
        public FakeUserApi init()
        {
            GDKManager.Instance.instantiateGDKInstance();
            GDKManager.Instance.init();
            GDKManager.Instance.setDefaultGdk(GDKManager.Instance.defaultGDKName);
            return this;
        }

        public string pluginName
        {
            get
            {
                return GDKManager.Instance.defaultGDKName;
            }
        }

        public async Task initConfig(GDKConfigV2 config)
        {
            await GDKManager.Instance.initWithGDKConfig(config);
        }

        public static FakeUserApi fakeGdk = new FakeUserApi();
    }

}
