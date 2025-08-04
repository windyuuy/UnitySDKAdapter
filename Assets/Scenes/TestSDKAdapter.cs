using System.Collections;
using System.Collections.Generic;
using GDK;
using UnityEngine;

public class TestSDKAdapter : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        DevelopGDK.Config.Register();
#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR
        WechatGDK.Config.Register();
#endif

        var fakeUserApi = new FakeUserApi();
        fakeUserApi.Init();

#if UNITY_EDITOR
        DevelopGDK.Config.UseAsDefault();
#else
        WechatGDK.Config.UseAsDefault();
#endif
        await fakeUserApi.InitConfig(new GDKConfigV2());
    }
}
