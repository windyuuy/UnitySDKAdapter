using System.Threading.Tasks;

namespace GDK
{

    public interface AdvertType
    {
        /**
         * 条幅广告
         */
        public const string BannerAdvert = "BannerAdvert";
        /**
         * 激励视频广告
         */
        public const string RewardedVideoAdvert = "RewardedVideoAdvert";

        /**
         * 全屏视频广告
         */
        public const string FullscreenVideoAdvert = "FullscreenVideoAdvert";

        /**
         * 原生信息流广告
         */
        public const string FeedAdvert = "FeedAdvert";

        /**
         * 插屏广告
         */
        public const string InterstitialAdvert = "InterstitialAdvert";
    }

    public class AdCreateInfo
    {

        public AdCreateInfo() { }
        public AdCreateInfo(AdCreateInfo info)
        {
            this.advertType = info.advertType;
            this.appId = info.appId;
            this.placementId = info.placementId;
            this.isDebug = info.isDebug;
        }

        /// <summary>
        /// AdvertType
        /// </summary>
        public string advertType;
        public string appId;
        public string placementId;
        public bool isDebug = false;
    }

    public class ShowAdUnitResult: BaseResponse
    {
        public bool couldReward;
        public bool isEnded;
    }

    public interface IShowAdUnitOpInfo
    {
        public string scene { get; }
    }

    public class ShowAdUnitOpInfo : IShowAdUnitOpInfo
    {
        public string scene { get; set; }
    }

    public class AdUnitStyle
    {
        public double width;
        public double top;
        public double x;
        public double y;
        public double height;
        public double bottom;
    }

    public class LoadAdUnitResult : BaseResponse
    {
    }

    public interface IAdvertUnit
    {
        public Task<LoadAdUnitResult> Load();
        public Task<ShowAdUnitResult> Show(IShowAdUnitOpInfo opInfo);
        public bool isReady { get; }
        public bool isAlive { get; }
        public void destroy();
        public void setStyle(AdUnitStyle style);
        public void hide();
    }

    public interface IRewardedVideoAd : IAdvertUnit
    {

    }

    public interface IAdvertV2 : IModule
    {
        /**
         * 是个单例
         * 创建激励视频广告对象
         */
        public Task<IAdvertUnit> CreateAdvertUnit(AdCreateInfo createInfo);

        /**
		 * 是个单例
		 * 创建激励视频广告对象
		 */
        public Task<IRewardedVideoAd> CreateRewardedVideoAd(AdCreateInfo createInfo);

        /**
         * 是否支持该类型广告
         * @param advertType 广告类型
         */
        public bool isAdvertTypeSupported(string advertType);
    }
}
