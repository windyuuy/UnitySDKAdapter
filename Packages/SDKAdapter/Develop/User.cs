
using System.Threading.Tasks;
using GDK;

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

        public override Task<LoginResult> Login(LoginParams paras)
        {
			return Task.FromResult(new LoginResult());
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