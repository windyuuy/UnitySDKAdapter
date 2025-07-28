
using System;
using System.Threading.Tasks;

namespace GDK
{
	public abstract class AdvertV2Base : IAdvertV2
	{
		public IModuleMap Api { get; set; }

		public virtual void Init()
		{
		}

		public virtual Task InitWithConfig(GDKConfigV2 info)
		{
			return Task.CompletedTask;
		}

		/**
		* 是个单例
		* 创建激励视频广告对象
		*/
		public virtual async Task<IAdvertUnit> CreateAdvertUnit(AdCreateInfo createInfo)
		{
			if (createInfo.AdvertType == AdvertType.RewardedVideoAdvert)
			{
				if (this.IsAdvertTypeSupported(AdvertType.RewardedVideoAdvert))
				{
					return await this.CreateRewardedVideoAd(createInfo);
				}
			}
			throw new NotImplementedException($"invalid advert type: {createInfo.AdvertType}");
		}
		public abstract bool IsAdvertTypeSupported(string advertType);

		/**
		 * 是个单例
		 * 创建激励视频广告对象
		 */
		public abstract Task<IRewardedVideoAd> CreateRewardedVideoAd(AdCreateInfo createInfo);

	}
}
