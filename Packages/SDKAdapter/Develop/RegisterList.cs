namespace DevelopGDK
{
	public class RegisterList : GDK.ModuleClassMap
	{
		public RegisterList()
		{
			this.AdvertV2 = () => new Advert();
			this.GameInfo = () => new GameInfo();
			this.User = () => new User();
			// this.Pay = () => new Pay();
			// this.Share = () => new Share();
			// this.SystemInfo = () => new SystemInfo();
			this.UserData = () => new UserData();
			// this.Customer = () => new Customer();
			// this.Widgets = () => new Widgets();
			// this.SubContext = () => new SubContext();
			this.Support = () => new Support();
			// this.Except = () => new Except();
			// this.Auth = () => new Auth();
			this.SystemAPI = () => new SystemAPI();
		}
	}
}
