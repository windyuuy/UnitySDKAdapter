
using System;
using System.Threading.Tasks;

namespace GDK
{

    public abstract class UserBase : IUser
    {
        public bool IsNativeRealNameSystem => false;
        public Task showMinorInfo(string info)
        {
            return Task.CompletedTask;
        }
        public Task<ShowRealNameDialogResult> ShowRealNameDialog(long userID, bool force)
        {
            return Task.FromResult(new ShowRealNameDialogResult());
        }

        public Task<BindUserResult> BindUser()
        {
            return Task.FromResult(new BindUserResult());
        }
        public void SetAccountChangeListener(Action f)
        {
        }

        public virtual Task<GetUserInfoResult> GetUserInfo(GetUserInfoOptions options)
        {
            return Task.FromResult(new GetUserInfoResult
            {
                cloudID = null,
                encryptedData = null,
                iv = null,
                rawData = null,
                signature = null,
                userInfo = new UserInfo
                {
                    avatarUrl = "",
                    city = null,
                    country = null,
                    gender = 0,
                    language = null,
                    nickName = "用户名六个字",
                    province = null,
                },
                ErrMsg = null
            });
        }

        public void Init()
        {
        }
        public Task InitWithConfig(GDKConfigV2 info)
        {
            return Task.CompletedTask;
        }
        public void SetLoginSupport(LoginSupportOptions loginSupport)
        {
        }
        public IModuleMap Api { get; set; }
        public abstract Task<LoginResult> Login(LoginParams paras);
        public Action<bool, object> bindCallback;
        public Action rebootCallback;
        public void SetBindCallback(Action<bool, object> callback)
        {
            this.bindCallback = callback;
        }

        public void SetRebootCallback(Action callback)
        {
            this.rebootCallback = callback;
        }


        public abstract Task showUserCenter();
        public abstract Task ShowBindDialog();
        public Task CheckSession(ReqParams paras)
        {
            return Task.CompletedTask;
        }

        public abstract Task<UserDataUpdateResult> Update();
        public abstract Task<GetFriendCloudStorageResult> GetFriendCloudStorage(GetFriendCloudStorageReq obj);
        public abstract Task SetUserCloudStorage(SetFriendCloudStorageReq obj);
        public abstract bool CheckIsUserBind(long userId);
    }
}
