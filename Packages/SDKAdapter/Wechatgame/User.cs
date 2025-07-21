
using System;
using System.Collections;
using System.Threading.Tasks;
using GDK;
using MonoExtLib.Loom;
using UnityEngine;
using UnityEngine.Networking;
using WeChatWASM;

namespace WechatGDK
{
    public class User : GDK.UserBase
    {
        public override bool CheckIsUserBind(long userId)
        {
            throw new System.NotImplementedException();
        }

        public override Task<GetFriendCloudStorageResult> GetFriendCloudStorage(GetFriendCloudStorageReq obj)
        {
            throw new System.NotImplementedException();
        }

        [Serializable]
        public class Code2SessionResp
        {
            public string openid;//   用户唯一标识
            public string session_key;//  会话密钥
            public string unionid;//  用户在开放平台的唯一标识符，若当前小程序已绑定到微信开放平台账号下会返回，详见 UnionID 机制说明。
            public int errcode;//   错误码
            public string errmsg;//   错误信息
        }

        public override Task<LoginResult> Login(LoginParams paras)
        {
            var ts = new TaskCompletionSource<LoginResult>();
            WX.Login(new LoginOption()
            {
                success = (resp1) =>
                {
                    UnityEngine.Debug.Log($"WX.Login-success: {resp1.code}");
                    var url = $"https://api.weixin.qq.com/sns/jscode2session?appid={paras.AppId}&secret={paras.AppSecret}&js_code={resp1.code}&grant_type=authorization_code";
                    UnityEngine.Debug.Log($"Code2Session: {url}");
                    IEnumerator Code2Session(string url)
                    {
                        var uwr = UnityWebRequest.Get(url);
                        yield return uwr.SendWebRequest();
                        var text = uwr.downloadHandler.text;
                        UnityEngine.Debug.Log($"Code2Session-Resp: {uwr.responseCode}, {text}");
                        try
                        {
                            var resp = JsonUtility.FromJson<Code2SessionResp>(text);

                            ts.SetResult(new LoginResult()
                            {
                                IsOk = true,
                                Code = resp.errcode.ToString(),
                                OpenId = resp.openid,
                                Unionid = resp.unionid,
                                ErrMsg = resp.errmsg,
                            });
                        }
                        catch (Exception exception)
                        {
                            Debug.LogException(exception);
                            ts.SetResult(new LoginResult()
                            {
                                IsOk = false,
                                Code = resp1.code,
                                ErrMsg = "Code2Session-ParseJSON-failed",
                            });
                        }
                    }
                    LoomMG.SharedLoom.StartCoroutine(Code2Session(url));
                },
                fail = (resp) =>
                {
                    Debug.LogError($"WX.Login-failed: {resp.errno}, {resp.errMsg}");
                    ts.SetResult(new LoginResult()
                    {
                        IsOk = false,
                        Extra = resp,
                        Code = resp.errno.ToString(),
                        ErrMsg = resp.errMsg,
                    });
                },
            });
            return ts.Task;
        }

        public override Task SetUserCloudStorage(SetFriendCloudStorageReq obj)
        {
            throw new System.NotImplementedException();
        }

        public override Task ShowBindDialog()
        {
            throw new System.NotImplementedException();
        }

        public override Task showUserCenter()
        {
            throw new System.NotImplementedException();
        }

        public override Task<UserDataUpdateResult> Update()
        {
            throw new System.NotImplementedException();
        }
    }
}