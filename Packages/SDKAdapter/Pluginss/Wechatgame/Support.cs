#if SUPPORT_WECHATGAME
namespace WechatGDK
{
	public class Support : GDK.SupportBase
	{
		public Support()
		{
			this.supportShare = true;
			this.supportShareTickets = true;
			this.requireSubDomainRank = true;
			this.requireAuthorize = true;
		}

		public override bool supportOpenLink => false;
	}
}
#endif
