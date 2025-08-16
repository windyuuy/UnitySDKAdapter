#if SUPPORT_BYTEDANCE
	using System;
	using System.Collections;
	using System.Threading.Tasks;
	using GDK;
	using MonoExtLib.Loom;
	using TTSDK;
	using UnityEngine;
	using UnityEngine.Networking;

	namespace BytedanceGDK
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
				public string openid; //   用户唯一标识
				public string session_key; //  会话密钥
				public string unionid; //  用户在开放平台的唯一标识符，若当前小程序已绑定到微信开放平台账号下会返回，详见 UnionID 机制说明。
				public int errcode; //   错误码
				public string errmsg; //   错误信息

				/// <summary>
				/// 匿名用户在当前小游戏的 ID，如果请求时有 anonymous_code 参数才会返回
				/// </summary>
				public string anonymous_openidString;

				public long error;
			}

			public override Task<LoginResult> Login(LoginParams paras)
			{
				var sysInfo = TT.GetSystemInfo();
				if (sysInfo.platform == "devtools")
				{
					TT.Login((code, acode, isLogin) => { Debug.Log("模拟器登录成功"); },
						(errMsg) => { Debug.Log($"模拟器登录失败: {errMsg}"); });
					return Task.FromResult(new LoginResult
					{
						IsOk = true,
						OpenId = "simulateopenid",
						Code = "0",
					});
				}
				else
				{
					var ts = new TaskCompletionSource<LoginResult>();
					TT.Login((code, acode, isLogin) =>
						{
							DevLog.Instance.Log($"TT.Login-success: {code}, {acode}, {isLogin}");
							var url =
								$"https://minigame.zijieapi.com/mgplatform/api/apps/jscode2session?appid={paras.AppId}&code={code}&anonymous_code={acode}&secret={paras.AppSecret}";
							DevLog.Instance.Log($"Code2Session: {url}");

							IEnumerator Code2Session(string url)
							{
								var uwr = UnityWebRequest.Get(url);
								yield return uwr.SendWebRequest();
								var text = uwr.downloadHandler.text;
								DevLog.Instance.Log($"Code2Session-Resp: {uwr.responseCode}, {text}");
								uwr.Dispose();
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
										Code = code,
										ErrMsg = "Code2Session-ParseJSON-failed",
									});
								}
							}

							LoomMG.SharedLoom.StartCoroutine(Code2Session(url));
						}, (errMsg) =>
						{
							DevLog.Instance.Error($"TT.Login-failed: {errMsg}");
							ts.SetResult(new LoginResult()
							{
								IsOk = false,
								Code = "-1",
								ErrMsg = errMsg,
							});
						},
						true);
					return ts.Task;
				}
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
#endif