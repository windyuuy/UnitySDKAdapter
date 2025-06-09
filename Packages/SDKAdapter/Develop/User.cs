
using System.Threading.Tasks;
using GDK;

namespace DevelopGDK
{
    public class User : GDK.UserBase
    {
		public override bool checkIsUserBind(long userId)
		{
			return true;
        }

        public override Task<GetFriendCloudStorageResult> getFriendCloudStorage(GetFriendCloudStorageReq obj)
        {
			return Task.FromResult(new GetFriendCloudStorageResult());
        }

        public override Task<LoginResult> login(LoginParams paras)
        {
			return Task.FromResult(new LoginResult());
        }

		public override Task setUserCloudStorage(SetFriendCloudStorageReq obj)
		{
			return Task.CompletedTask;
        }

        public override Task showBindDialog()
        {
			return Task.CompletedTask;
        }

        public override Task showUserCenter()
        {
			return Task.CompletedTask;
        }

        public override Task<UserDataUpdateResult> update()
        {
			return Task.FromResult(new UserDataUpdateResult());
        }
    }
}