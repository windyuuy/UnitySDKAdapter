#if UNITY_EDITOR
using System;
using System.Threading.Tasks;
using GDK;

namespace DevelopGDK
{

	public interface AppInfoKeys
	{
		/** string */
		public const string RewardedVideoAdSimulateLoadFailed = "develop.rewardedvideoad.simulateloadfailed";
		public const string RewardedVideoAdSimulateShowFailed = "develop.rewardedvideoad.simulateshowfailed";
	}

	public class RewardedVideoAd : IRewardedVideoAd
	{
		public bool IsReady { get; set; }

		public bool IsAlive { get; set; }

		protected bool IsUsed = false;

		public RewardedVideoAd()
		{
			this.IsAlive = true;
		}
		public void Destroy()
		{
			this.IsAlive = false;
		}

		public void Hide()
		{
		}

		public float LoadAdFailedProperbility = 0.2f;
		public float ShowAdFailedProperbility = 0.2f;

		public Task<LoadAdUnitResult> Load()
		{
			var isReady = new Random().NextDouble() >= LoadAdFailedProperbility;
			if (!isReady)
			{
				DevLog.Instance.Error($"模拟加载激励视频广告失败");
			}
			this.IsReady = isReady;
			return Task.FromResult(new LoadAdUnitResult() { IsOk = isReady });
		}

		public void SetStyle(AdUnitStyle style)
		{
		}

		public Task<ShowAdUnitResult> Show(IShowAdUnitOpInfo opInfo)
		{
			if (this.IsUsed)
			{
				return Task.FromResult(new ShowAdUnitResult() { IsOk = false, IsEnded = false, ErrMsg = "Advert is used" });
			}

			this.IsUsed = true;
			this.IsReady = false;
			var isEnded = new Random().NextDouble() >= ShowAdFailedProperbility;
			if (!isEnded)
			{
				DevLog.Instance.Error($"模拟展示激励视频广告失败");
			}
			return Task.FromResult(new ShowAdUnitResult() { IsOk = true, IsEnded = isEnded, CouldReward = isEnded, });
		}
	}
	public class Advert : GDK.AdvertV2Base
	{
		public override Task<IRewardedVideoAd> CreateRewardedVideoAd(AdCreateInfo createInfo)
		{
			var ad = new RewardedVideoAd();
			var loadAdFailedProperbility = this.Api.SystemAPI.GetAppInfoDouble(AppInfoKeys.RewardedVideoAdSimulateLoadFailed, 0);
			var showAdFailedProperbility = this.Api.SystemAPI.GetAppInfoDouble(AppInfoKeys.RewardedVideoAdSimulateShowFailed, 0);
			ad.LoadAdFailedProperbility = (float)loadAdFailedProperbility;
			ad.ShowAdFailedProperbility = (float)showAdFailedProperbility;
			return Task.FromResult(ad as IRewardedVideoAd);
		}

		public override bool IsAdvertTypeSupported(string advertType)
		{
			return true;
		}
	}
}

#endif
