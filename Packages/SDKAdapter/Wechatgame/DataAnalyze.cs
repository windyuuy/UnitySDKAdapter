
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeChatWASM;

namespace GDK
{
	public class DataAnalyze : DataAnalyzeBase
	{
		public override void ReportEvent<T>(string eventId, Dictionary<string, string> data)
		{
			WX.ReportEvent(eventId, data);
		}
	}
}
