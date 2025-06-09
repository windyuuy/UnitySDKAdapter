
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
	}
}