
#if SUPPORT_WECHATGAME
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GDK;
using WeChatWASM;

namespace WechatGDK
{
    public class RewardedVideoAd : GDK.IRewardedVideoAd, IDisposable
    {
        protected WXRewardedVideoAd AdUnit;
        public string PlacementId { get; private set; }

        public RewardedVideoAd(WXRewardedVideoAd adUnit, string placementId)
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

        protected Action<WXRewardedVideoAdOnCloseResponse> OnCloseCallbacks;
        protected Action<WXADErrorResponse> OnErrorCallbacks;

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

            AdUnit.Load((resp) =>
            {
                DevLog.Instance.Log($"load rewarded video ad-ok: {PlacementId}, {resp.errCode}, {resp.errMsg}");

                IsReady = true;

                ts.SetResult(new LoadAdUnitResult()
                {
                    IsOk = true,
                });
            }, (resp) =>
            {
                DevLog.Instance.Error($"load rewarded video ad-failed: {PlacementId}, {resp.errCode}, {resp.errMsg}");

                LoadingTask = null;

                ts.SetResult(new LoadAdUnitResult()
                {
                    IsOk = false,
                    ErrCode = resp.errCode,
                    ErrMsg = resp.errMsg,
                });
            });

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
            adUnit.Show((resp) =>
            {
                DevLog.Instance.Log($"show rewarded video ad-ok: {placementId}, {resp.errCode}, {resp.errMsg}");
                void Finish()
                {
                    ShowTask = null;
                    adUnit.OffClose(OnClose);
                    adUnit.OffError(OnError);
                }
                void OnClose(WXRewardedVideoAdOnCloseResponse respClose)
                {
                    WX.ReportEvent("e2004", new Dictionary<string, string>()
                    {
                        ["placement_id"] = placementId,
                        ["instance_id"] = adUnit.instanceId,
                        ["rewardable"] = $"{respClose.isEnded}",
                        ["errmsg"] = respClose.errMsg,
                    });
                    DevLog.Instance.Log($"show rewarded video ad-closed: {placementId}, {adUnit.instanceId}, {respClose.isEnded}, {respClose.errMsg}");
                    Finish();
                    ts.SetResult(new ShowAdUnitResult()
                    {
                        IsOk = true,
                        CouldReward = respClose.isEnded,
                        IsEnded = respClose.isEnded,
                        ErrMsg = respClose.errMsg,
                    });
                }
                void OnError(WXADErrorResponse respError)
                {
                    DevLog.Instance.Error($"show rewarded video ad-failed2: {placementId}, {resp.errCode}, {resp.errMsg}");
                    Finish();
                    ts.SetResult(new ShowAdUnitResult()
                    {
                        IsOk = false,
                        ErrMsg = respError.errMsg,
                        ErrCode = respError.errCode,
                    });
                }
                adUnit.OnClose(OnClose);
                adUnit.OnError(OnError);
            }, (resp) =>
            {
                DevLog.Instance.Error($"show rewarded video ad-failed: {placementId}, {resp.errCode}, {resp.errMsg}");
                ShowTask = null;
                ts.SetResult(new ShowAdUnitResult()
                {
                    IsOk = false,
                    ErrCode = resp.errCode,
                    ErrMsg = resp.errMsg,
                });
            });

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
            var adUnit = WX.CreateRewardedVideoAd(new()
            {
                adUnitId = placementId,
            });
            var rewardedVideoAd = new RewardedVideoAd(adUnit, placementId);
            // AdvertMap.Add(placementId, rewardedVideoAd);
            // }
            DevLog.Instance.Log($"create rewarded video ad-ok: {placementId}, {adUnit.instanceId}");
            WX.ReportEvent("e2003", new Dictionary<string, string>()
            {
                ["placement_id"] = placementId,
                ["instance_id"] = adUnit.instanceId,
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
