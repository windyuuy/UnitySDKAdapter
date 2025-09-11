#if UNITY_EDITOR || true

	using System.Threading.Tasks;
	using GDK;
	using Lang.Time;

	namespace DevelopGDK
	{
		public class User : GDK.UserBase
		{
			public override bool CheckIsUserBind(long userId)
			{
				return true;
			}

			public override Task<GetFriendCloudStorageResult> GetFriendCloudStorage(GetFriendCloudStorageReq obj)
			{
				return Task.FromResult(new GetFriendCloudStorageResult());
			}

			private const string OpenIdKey = "DevelopGDK_OpenId_Key";

			public override Task<LoginResult> Login(LoginParams paras)
			{
				string openId;
				if (UnityEngine.PlayerPrefs.HasKey(OpenIdKey))
				{
					openId = UnityEngine.PlayerPrefs.GetString(OpenIdKey);
				}
				else
				{
					openId = Date.Now().ToString();
					UnityEngine.PlayerPrefs.SetString(OpenIdKey, openId);
				}

				return Task.FromResult(new LoginResult()
				{
					IsOk = true,
					OpenId = openId,
				});
			}

			public override Task SetUserCloudStorage(SetFriendCloudStorageReq obj)
			{
				return Task.CompletedTask;
			}

			public override Task ShowBindDialog()
			{
				return Task.CompletedTask;
			}

			public override Task showUserCenter()
			{
				return Task.CompletedTask;
			}

			public override Task<UserDataUpdateResult> Update()
			{
				return Task.FromResult(new UserDataUpdateResult());
			}
		}
	}
#endif