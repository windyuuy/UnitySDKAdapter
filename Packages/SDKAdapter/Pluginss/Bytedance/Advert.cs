#if SUPPORT_BYTEDANCE
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using GDK;
	using TTSDK;

	namespace BytedanceGDK
	{
		public class RewardedVideoAd : GDK.IRewardedVideoAd, IDisposable
		{
			protected TTRewardedVideoAd AdUnit;
			public string PlacementId { get; private set; }

			public RewardedVideoAd(TTRewardedVideoAd adUnit, string placementId)
			{
				AdUnit = adUnit;
				PlacementId = placementId;
			}

			public bool IsReady { get; internal set; }

			public bool IsAlive { get; internal set; }

			public void Destroy()
			{
			}

			public void Hide()
			{
			}

			protected Task<LoadAdUnitResult> LoadingTask;

			public Task<LoadAdUnitResult> Load()
			{
				DevLog.Instance.Log($"load rewarded video ad: {PlacementId}");
				if (ShowTask != null)
				{
					// 需要处理, 否则 show 不回调
					DevLog.Instance.Error($"load rewarded video ad-failed, already showing: {PlacementId}");
					return Task.FromResult(new LoadAdUnitResult()
					{
						IsOk = false,
						ErrCode = -1,
						ErrMsg = $"LoadFailed, Advert is already Showing: {PlacementId}",
					});
				}

				if (LoadingTask != null)
				{
					// 正在loading
					DevLog.Instance.Log($"load rewarded video, already loading: {PlacementId}");
					return LoadingTask;
				}

				var ts = new TaskCompletionSource<LoadAdUnitResult>();
				LoadingTask = ts.Task;

				var adUnit = AdUnit;
				adUnit.Load();

				void Finish()
				{
					adUnit.OnLoad -= OnLoad;
					adUnit.OnError -= OnError;
				}

				void OnLoad()
				{
					Finish();
					DevLog.Instance.Log($"load rewarded video ad-ok: {PlacementId}");

					IsReady = true;

					ts.SetResult(new LoadAdUnitResult() { IsOk = true, });
				}

				adUnit.OnLoad += OnLoad;

				void OnError(int code, string message)
				{
					Finish();
					DevLog.Instance.Error(
						$"load rewarded video ad-failed: {PlacementId}, {code}, {message}");

					LoadingTask = null;

					ts.SetResult(new LoadAdUnitResult()
					{
						IsOk = false,
						ErrCode = code,
						ErrMsg = message,
					});
				}

				adUnit.OnError += OnError;

				return LoadingTask;
			}

			public void SetStyle(AdUnitStyle style)
			{
			}

			protected Task<ShowAdUnitResult> ShowTask;

			public Task<ShowAdUnitResult> Show(IShowAdUnitOpInfo opInfo)
			{
				var placementId = PlacementId;
				var adUnit = AdUnit;

				DevLog.Instance.Log($"show rewarded video ad: {placementId}");
				if (ShowTask != null)
				{
					// 需要处理, 否则 show 不回调
					DevLog.Instance.Error($"show rewarded video ad-failed, already showing: {placementId}");
					return Task.FromResult(new ShowAdUnitResult()
					{
						IsOk = false,
						ErrCode = -1,
						ErrMsg = $"ShowFailed, Advert is already Showing: {placementId}, {opInfo.Scene}",
					});
				}

				var ts = new TaskCompletionSource<ShowAdUnitResult>();
				ShowTask = ts.Task;

				IsReady = false;
				LoadingTask = null;
				adUnit.Show();

				DevLog.Instance.Log($"show rewarded video ad-ok: {placementId}");

				void Finish()
				{
					ShowTask = null;
					adUnit.OnClose -= OnClose;
					adUnit.OnError -= OnError;
				}

				void OnClose(bool isEnded, int count)
				{
					TT.ReportAnalytics("e2004", new Dictionary<string, string>()
					{
						["placement_id"] = placementId,
						["instance_id"] = adUnit.GetHashCode().ToString(),
						["rewardable"] = $"{isEnded}",
						["errmsg"] = "",
						["count"]=count.ToString(),
					});
					DevLog.Instance.Log(
						$"show rewarded video ad-closed: {placementId}, {adUnit.GetHashCode().ToString()}, {isEnded}, count={count}");
					Finish();
					ts.SetResult(new ShowAdUnitResult()
					{
						IsOk = true,
						CouldReward = isEnded,
						IsEnded = isEnded,
						Count=count,
						ErrMsg = "",
					});
				}

				void OnError(int code, string message)
				{
					DevLog.Instance.Error(
						$"show rewarded video ad-failed2: {placementId}, {code}, {message}");
					Finish();
					ts.SetResult(new ShowAdUnitResult()
					{
						IsOk = false,
						ErrMsg = message,
						ErrCode = code,
					});
				}

				adUnit.OnClose += OnClose;
				adUnit.OnError += OnError;

				return ShowTask;
			}

			public void Dispose()
			{
				this.Destroy();
			}
		}

		public class Advert : GDK.AdvertV2Base
		{
			// protected static readonly Dictionary<string, IAdvertUnit> AdvertMap = new();
			public override Task<IRewardedVideoAd> CreateRewardedVideoAd(AdCreateInfo createInfo)
			{
				DevLog.Instance.Log($"create rewarded video ad: {createInfo.PlacementId}");
				var placementId = createInfo.PlacementId;
				// if (!(AdvertMap.TryGetValue(placementId, out var advert) && advert is IRewardedVideoAd rewardedVideoAd))
				// {
				var adUnit = TT.CreateRewardedVideoAd(new()
				{
					AdUnitId = placementId,
				});
				var rewardedVideoAd = new RewardedVideoAd(adUnit, placementId);
				// AdvertMap.Add(placementId, rewardedVideoAd);
				// }
				DevLog.Instance.Log($"create rewarded video ad-ok: {placementId}, {adUnit.GetHashCode().ToString()}");
				TT.ReportAnalytics("e2003", new Dictionary<string, string>()
				{
					["placement_id"] = placementId,
					["instance_id"] = adUnit.GetHashCode().ToString(),
				});
				return Task<IRewardedVideoAd>.FromResult(rewardedVideoAd as GDK.IRewardedVideoAd);
			}

			public override bool IsAdvertTypeSupported(string advertType)
			{
				return true;
			}
		}
	}
#endif