
using System.Threading.Tasks;
using GDK;

namespace WechatGDK
{
    public class User : GDK.UserBase
    {
        public GDK.IModuleMap api { get; set; }

        public override bool checkIsUserBind(long userId)
        {
            throw new System.NotImplementedException();
        }

        public override Task<GetFriendCloudStorageResult> getFriendCloudStorage(GetFriendCloudStorageReq obj)
        {
            throw new System.NotImplementedException();
        }

        public override Task<LoginResult> login(LoginParams paras)
        {
            throw new System.NotImplementedException();
        }

        public override Task setUserCloudStorage(SetFriendCloudStorageReq obj)
        {
            throw new System.NotImplementedException();
        }

        public override Task showBindDialog()
        {
            throw new System.NotImplementedException();
        }

        public override Task showUserCenter()
        {
            throw new System.NotImplementedException();
        }

        public override Task<UserDataUpdateResult> update()
        {
            throw new System.NotImplementedException();
        }
    }
}