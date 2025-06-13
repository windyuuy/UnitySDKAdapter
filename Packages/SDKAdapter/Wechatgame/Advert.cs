
using System;
using System.Threading.Tasks;
using GDK;
using WeChatWASM;

namespace WechatGDK
{
    public class RewardedVideoAd : GDK.IRewardedVideoAd
    {
        protected WXRewardedVideoAd AdUnit;

        public RewardedVideoAd(WXRewardedVideoAd adUnit)
        {
            AdUnit = adUnit;
        }

        public bool IsReady { get; internal set; }

        public bool IsAlive { get; internal set; }

        public void Destroy()
        {
        }

        public void Hide()
        {
        }

        public Task<LoadAdUnitResult> Load()
        {
            var ts = new TaskCompletionSource<LoadAdUnitResult>();

            UnityEngine.Debug.Log($"load rewarded video ad");
            AdUnit.Load((resp) =>
            {
                UnityEngine.Debug.Log($"load rewarded video ad-ok: {resp.errCode}, {resp.errMsg}");
                ts.SetResult(new LoadAdUnitResult()
                {
                    IsOk = true,
                });
            }, (resp) =>
            {
                UnityEngine.Debug.Log($"load rewarded video ad-failed: {resp.errCode}, {resp.errMsg}");
                ts.SetResult(new LoadAdUnitResult()
                {
                    IsOk = false,
                    ErrCode = resp.errCode,
                    ErrMsg = resp.errMsg,
                });
            });

            return ts.Task;
        }

        public void SetStyle(AdUnitStyle style)
        {
        }

        public Task<ShowAdUnitResult> Show(IShowAdUnitOpInfo opInfo)
        {
            var ts = new TaskCompletionSource<ShowAdUnitResult>();

            UnityEngine.Debug.Log($"show rewarded video ad");
            AdUnit.Show((resp) =>
            {
                UnityEngine.Debug.Log($"show rewarded video ad-ok: {resp.errCode}, {resp.errMsg}");
                void Finish()
                {
                    AdUnit.OffClose(OnClose);
                    AdUnit.OffError(OnError);
                }
                void OnClose(WXRewardedVideoAdOnCloseResponse respClose)
                {
                    Finish();
                    UnityEngine.Debug.Log($"show rewarded video ad-closed: {respClose.isEnded}, {respClose.errMsg}");
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
                    Finish();
                    UnityEngine.Debug.LogError($"show rewarded video ad-failed2: {resp.errCode}, {resp.errMsg}");
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
                UnityEngine.Debug.LogError($"show rewarded video ad-failed: {resp.errCode}, {resp.errMsg}");
                ts.SetResult(new ShowAdUnitResult()
                {
                    IsOk = false,
                    ErrCode = resp.errCode,
                    ErrMsg = resp.errMsg,
                });
            });

            return ts.Task;
        }
    }

    public class Advert : GDK.AdvertV2Base
    {
        public override Task<IRewardedVideoAd> CreateRewardedVideoAd(AdCreateInfo createInfo)
        {
            UnityEngine.Debug.Log($"create rewarded video ad: {createInfo.PlacementId}");
            var adUnit = WX.CreateRewardedVideoAd(new()
            {
                adUnitId = createInfo.PlacementId,
            });
            UnityEngine.Debug.Log($"create rewarded video ad-ok: {createInfo.PlacementId}");
            IRewardedVideoAd ad = new RewardedVideoAd(adUnit);
            return Task<IRewardedVideoAd>.FromResult(ad);
        }

        public override bool IsAdvertTypeSupported(string advertType)
        {
            return true;
        }
    }
}
