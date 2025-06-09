
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

        public bool isReady { get; internal set; }

        public bool isAlive { get; internal set; }

        public void destroy()
        {
        }

        public void hide()
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

        public void setStyle(AdUnitStyle style)
        {
        }

        public Task<ShowAdUnitResult> Show(IShowAdUnitOpInfo opInfo)
        {
            var ts = new TaskCompletionSource<ShowAdUnitResult>();

            UnityEngine.Debug.Log($"show rewarded video ad");
            AdUnit.Show((resp) =>
            {
                void Finish()
                {
                    AdUnit.OffClose(OnClose);
                    AdUnit.OffError(OnError);
                }
                void OnClose(WXRewardedVideoAdOnCloseResponse respClose)
                {
                    Finish();
                    UnityEngine.Debug.Log($"show rewarded video ad-ok: {respClose.isEnded}, {respClose.errMsg}");
                    ts.SetResult(new ShowAdUnitResult()
                    {
                        IsOk = true,
                        couldReward = respClose.isEnded,
                        isEnded = respClose.isEnded,
                        ErrMsg = respClose.errMsg,
                    });
                }
                void OnError(WXADErrorResponse respError)
                {
                    Finish();
                    UnityEngine.Debug.Log($"show rewarded video ad-failed2: {resp.errCode}, {resp.errMsg}");
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
                UnityEngine.Debug.Log($"show rewarded video ad-failed: {resp.errCode}, {resp.errMsg}");
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
            UnityEngine.Debug.Log($"create rewarded video ad: {createInfo.placementId}");
            var adUnit = WX.CreateRewardedVideoAd(new()
            {
                adUnitId = createInfo.placementId,
            });
            UnityEngine.Debug.Log($"create rewarded video ad-ok: {createInfo.placementId}");
            IRewardedVideoAd ad = new RewardedVideoAd(adUnit);
            return Task<IRewardedVideoAd>.FromResult(ad);
        }

        public override bool isAdvertTypeSupported(string advertType)
        {
            return true;
        }
    }
}
