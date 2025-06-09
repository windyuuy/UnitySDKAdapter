
using System;
using System.Threading.Tasks;

namespace GDK
{

    public abstract class UserBase : IUser
    {
        public bool isNativeRealNameSystem => false;
        public Task showMinorInfo(string info)
        {
            return Task.CompletedTask;
        }
        public Task<ShowRealNameDialogResult> showRealNameDialog(long userID, bool force)
        {
            return Task.FromResult(new ShowRealNameDialogResult());
        }

        public Task<BindUserResult> bindUser()
        {
            return Task.FromResult(new BindUserResult());
        }
        public void setAccountChangeListener(Action f)
        {
        }
        public void init()
        {
        }
        public Task initWithConfig(GDKConfigV2 info)
        {
            return Task.CompletedTask;
        }
        public void setLoginSupport(LoginSupportOptions loginSupport)
        {
        }
        public IModuleMap api { get; set; }
        public abstract Task<LoginResult> login(LoginParams paras);
        public Action<bool, object> bindCallback;
        public Action rebootCallback;
        public void setBindCallback(Action<bool, object> callback)
        {
            this.bindCallback = callback;
        }

        public void setRebootCallback(Action callback)
        {
            this.rebootCallback = callback;
        }


        public abstract Task showUserCenter();
        public abstract Task showBindDialog();
        public Task checkSession(ReqParams paras)
        {
            return Task.CompletedTask;
        }

        public abstract Task<UserDataUpdateResult> update();
        public abstract Task<GetFriendCloudStorageResult> getFriendCloudStorage(GetFriendCloudStorageReq obj);
        public abstract Task setUserCloudStorage(SetFriendCloudStorageReq obj);
        public abstract bool checkIsUserBind(long userId);
    }
}
