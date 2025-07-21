
using System;
using System.Threading.Tasks;
using WeChatWASM;

namespace GDK
{
	public class DataAnalyze : DataAnalyzeBase
	{
		public override void ReportEvent<T>(string eventId, T data)
		{
			WX.ReportEvent(eventId, data);
		}
	}
}
