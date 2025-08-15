#if UNITY_EDITOR || true

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

			public override bool SupportOpenLink => true;
			public override bool SupportNavToSidebarReward => true;
		}
	}
#endif