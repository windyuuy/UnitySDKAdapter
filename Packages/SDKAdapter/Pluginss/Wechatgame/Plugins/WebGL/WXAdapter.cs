#if SUPPORT_WECHATGAME
using System.Runtime.InteropServices;

namespace WechatGDK
{
	internal class WXAdapter
	{
		[DllImport("__Internal")]
		public static extern void GDK_Bytedance_ReportEvent(string eventid, string data);
	}
}
#endif
