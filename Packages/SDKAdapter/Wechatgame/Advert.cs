
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
            UnityEngine.Debug.Log($"load rewarded video ad: {PlacementId}");
            if (ShowTask != null)
            {
                // 需要处理, 否则 show 不回调
                UnityEngine.Debug.LogError($"load rewarded video ad-failed, already showing: {PlacementId}");
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
                UnityEngine.Debug.Log($"load rewarded video, already loading: {PlacementId}");
                return LoadingTask;
            }

            var ts = new TaskCompletionSource<LoadAdUnitResult>();
            LoadingTask = ts.Task;

            AdUnit.Load((resp) =>
            {
                UnityEngine.Debug.Log($"load rewarded video ad-ok: {PlacementId}, {resp.errCode}, {resp.errMsg}");

                IsReady = true;

                ts.SetResult(new LoadAdUnitResult()
                {
                    IsOk = true,
                });
            }, (resp) =>
            {
                UnityEngine.Debug.LogError($"load rewarded video ad-failed: {PlacementId}, {resp.errCode}, {resp.errMsg}");

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
            UnityEngine.Debug.Log($"show rewarded video ad: {PlacementId}");
            if (ShowTask != null)
            {
                // 需要处理, 否则 show 不回调
                UnityEngine.Debug.LogError($"show rewarded video ad-failed, already showing: {PlacementId}");
                return Task.FromResult(new ShowAdUnitResult()
                {
                    IsOk = false,
                    ErrCode = -1,
                    ErrMsg = $"ShowFailed, Advert is already Showing: {PlacementId}, {opInfo.Scene}",
                });
            }
            var ts = new TaskCompletionSource<ShowAdUnitResult>();
            ShowTask = ts.Task;

            IsReady = false;
            LoadingTask = null;
            AdUnit.Show((resp) =>
            {
                UnityEngine.Debug.Log($"show rewarded video ad-ok: {PlacementId}, {resp.errCode}, {resp.errMsg}");
                void Finish()
                {
                    ShowTask = null;
                    AdUnit.OffClose(OnClose);
                    AdUnit.OffError(OnError);
                }
                void OnClose(WXRewardedVideoAdOnCloseResponse respClose)
                {
                    UnityEngine.Debug.Log($"show rewarded video ad-closed: {PlacementId}, {respClose.isEnded}, {respClose.errMsg}");
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
                    UnityEngine.Debug.LogError($"show rewarded video ad-failed2: {PlacementId}, {resp.errCode}, {resp.errMsg}");
                    Finish();
                    ts.SetResult(new ShowAdUnitResult()
                    {
                        IsOk = false,
                        ErrMsg = respError.errMsg,
                        ErrCode = respError.errCode,
                    });
                }
                AdUnit.OnClose(OnClose);
                AdUnit.OnError(OnError);
            }, (resp) =>
            {
                UnityEngine.Debug.LogError($"show rewarded video ad-failed: {PlacementId}, {resp.errCode}, {resp.errMsg}");
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
            UnityEngine.Debug.Log($"create rewarded video ad: {createInfo.PlacementId}");
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
            UnityEngine.Debug.Log($"create rewarded video ad-ok: {createInfo.PlacementId}");
            return Task<IRewardedVideoAd>.FromResult(rewardedVideoAd as GDK.IRewardedVideoAd);
        }

        public override bool IsAdvertTypeSupported(string advertType)
        {
            return true;
        }
    }
}
