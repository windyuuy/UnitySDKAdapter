
using System;
using System.Threading.Tasks;

namespace GDK
{
	public abstract class AdvertV2Base : IAdvertV2
	{
		public IModuleMap api { get; set; }

		public virtual void init()
		{
		}

		public virtual Task initWithConfig(GDKConfigV2 info)
		{
			return Task.CompletedTask;
		}

		/**
		* 是个单例
		* 创建激励视频广告对象
		*/
		public virtual async Task<IAdvertUnit> CreateAdvertUnit(AdCreateInfo createInfo)
		{
			if (createInfo.advertType == AdvertType.RewardedVideoAdvert)
			{
				if (this.isAdvertTypeSupported(AdvertType.RewardedVideoAdvert))
				{
					return await this.CreateRewardedVideoAd(createInfo);
				}
			}
			throw new NotImplementedException($"invalid advert type: {createInfo.advertType}");
		}
		public abstract bool isAdvertTypeSupported(string advertType);

		/**
		 * 是个单例
		 * 创建激励视频广告对象
		 */
		public abstract Task<IRewardedVideoAd> CreateRewardedVideoAd(AdCreateInfo createInfo);

	}
}
