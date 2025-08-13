#if SUPPORT_BYTEDANCE
namespace BytedanceGDK
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

		public override bool SupportNavToSidebarReward => true;
	}
}
#endif