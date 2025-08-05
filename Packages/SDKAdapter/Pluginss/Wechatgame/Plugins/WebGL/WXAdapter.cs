#if SUPPORT_WECHATGAME
using System.Runtime.InteropServices;

namespace WechatGDK
{
	internal class WXAdapter
	{
		[DllImport("__Internal")]
		public static extern void GDK_Wechatgame_ReportEvent(string eventid, string data);
		
		[DllImport("__Internal")]
		private static extern void GDK_Wechatgame_Openlink(string openlink);
	}
}
#endif
