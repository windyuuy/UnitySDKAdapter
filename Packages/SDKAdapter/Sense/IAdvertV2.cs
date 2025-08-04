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
            this.AdvertType = info.AdvertType;
            this.AppId = info.AppId;
            this.PlacementId = info.PlacementId;
            this.IsDebug = info.IsDebug;
        }

        /// <summary>
        /// AdvertType
        /// </summary>
        public string AdvertType;
        public string AppId;
        public string PlacementId;
        public bool IsDebug = false;
    }

    public class ShowAdUnitResult : BaseResponse
    {
        public bool CouldReward = false;
        public bool IsEnded = false;
        public int Count=1;
    }

    public interface IShowAdUnitOpInfo
    {
        public string Scene { get; }
    }

    public class ShowAdUnitOpInfo : IShowAdUnitOpInfo
    {
        public string Scene { get; set; }
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
        /// <summary>
        /// 已Load
        /// </summary>
        public bool IsReady { get; }
        /// <summary>
        /// 未Destroy
        /// </summary>
        public bool IsAlive { get; }
        public void Destroy();
        public void SetStyle(AdUnitStyle style);
        public void Hide();
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
        public bool IsAdvertTypeSupported(string advertType);
    }
}
