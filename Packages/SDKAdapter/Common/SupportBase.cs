
namespace GDK
{
	public abstract class SupportBase : ISupport
	{
		public bool supportShare { get; set; } = false;
		public bool supportShareTickets { get; set; } = false;
		public bool requireSubDomainRank { get; set; } = false;
		public bool requireAuthorize { get; set; } = false;
		public bool supportBuiltinCommitLog { get; set; } = false;
		public bool supportBuiltinOnlineLoopLog { get; set; } = false;
		public bool supportBuiltinIdentityCertification { get; set; } = false;
		public bool requireManagerAdLifecycle { get; set; } = false;
		public bool isNativePlugin { get; set; } = false;
	}
}
