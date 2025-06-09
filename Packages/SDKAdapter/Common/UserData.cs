
using System.Threading.Tasks;

namespace GDK
{
	public abstract class UserDataBase : IUserData
	{
		public abstract string openId { get; set; }

		public abstract string openKey { get; set; }

		public abstract string password { get; set; }

		public abstract string nickName { get; set; }

		public abstract long userId { get; set; }

		public abstract bool isNewUser { get; set; }

		public abstract string avatarUrl { get; set; }

		public abstract double backupTime { get; set; }

		public abstract int followGzh { get; set; }

		public abstract double channelId { get; set; }

		public abstract double createTime { get; set; }

		public abstract double sex { get; set; }

		public abstract double isWhiteUser { get; set; }

		public abstract double isMaster { get; set; }

		public abstract double roomId { get; set; }

		public abstract IModuleMap api { get; set; }

		public abstract void init();

		public abstract Task initWithConfig(GDKConfigV2 info);
	}
}
