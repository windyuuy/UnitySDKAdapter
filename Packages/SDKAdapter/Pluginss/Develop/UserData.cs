#if UNITY_EDITOR

using System.Threading.Tasks;
using GDK;

namespace DevelopGDK
{
	public class UserData : GDK.UserDataBase
	{
		public override string openId { get; set; } = "0999999";

		public double gameId
		{
			get
			{
				return -1;
			}
		}
		public override string openKey { get; set; }
		public override string password { get; set; }
		public override string nickName { get; set; }
		public override long userId { get; set; }
		public override bool isNewUser { get; set; }
		public override string avatarUrl { get; set; }
		public override double backupTime { get; set; }
		public override int followGzh { get; set; }
		public override double channelId { get; set; }
		public override double createTime { get; set; }
		public override double sex { get; set; }
		public override double isWhiteUser { get; set; }
		public override double isMaster { get; set; }
		public override double roomId { get; set; }
		public override IModuleMap Api { get; set; }

		public override void Init()
		{
		}

		public override Task InitWithConfig(GDKConfigV2 info)
		{
			return Task.CompletedTask;
		}
	}
}

#endif
