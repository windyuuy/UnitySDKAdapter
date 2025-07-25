
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeChatWASM;

namespace GDK
{
	public class DataAnalyze : DataAnalyzeBase
	{
		public override void ReportEvent<T>(string eventId, T data)
		{
			var dict = new Dictionary<string, string>();
			var fields = data.GetType().GetFields();
			foreach (var field in fields)
			{
				var value = field.GetValue(data);
				if (value is string str)
				{
					dict[field.Name] = str;
				}
			}
			WX.ReportEvent(eventId, data);
		}
		public override void ReportEvent(string eventId, Dictionary<string, string> data)
		{
			WX.ReportEvent(eventId, data);
		}
	}
}
