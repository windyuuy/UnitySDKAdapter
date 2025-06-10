
using System.Threading.Tasks;
using GDK;

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

        public override Task<LoginResult> Login(LoginParams paras)
        {
            throw new System.NotImplementedException();
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