#if UNITY_EDITOR

namespace DevelopGDK
{
	public class Support : GDK.SupportBase
	{
		public Support()
		{
			this.supportShare = true;
			this.supportShareTickets = false;
			this.requireSubDomainRank = false;
			this.requireAuthorize = false;
		}
	}
}
#endif
