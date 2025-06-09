using System;
using System.Threading.Tasks;
using GDK;

namespace DevelopGDK
{

	public class RewardedVideoAd : IRewardedVideoAd
	{
		public bool isReady { get; set; }

		public bool isAlive { get; set; }

		protected bool IsUsed = false;

		public RewardedVideoAd()
		{
			this.isAlive = true;
		}
		public void destroy()
		{
			this.isAlive = false;
		}

		public void hide()
		{
		}

		public Task<LoadAdUnitResult> Load()
		{
			var isReady = new Random().NextDouble() > 0.2f;
			this.isReady = isReady;
			return Task.FromResult(new LoadAdUnitResult() { IsOk = isReady });
		}

		public void setStyle(AdUnitStyle style)
		{
		}

		public Task<ShowAdUnitResult> Show(IShowAdUnitOpInfo opInfo)
		{
			if (this.IsUsed)
			{
				return Task.FromResult(new ShowAdUnitResult() { IsOk = false, isEnded = false, ErrMsg = "Advert is used" });
			}

			this.IsUsed = true;
			this.isReady = false;
			var isEnded = new Random().NextDouble() > 0.2f;
			return Task.FromResult(new ShowAdUnitResult() { IsOk = true, isEnded = isEnded, couldReward = isEnded, });
		}
	}
	public class Advert : GDK.AdvertV2Base
	{
		public override Task<IRewardedVideoAd> CreateRewardedVideoAd(AdCreateInfo createInfo)
		{
			var ad = new RewardedVideoAd();
			return Task.FromResult(ad as IRewardedVideoAd);
		}

		public override bool isAdvertTypeSupported(string advertType)
		{
			return true;
		}
	}
}
