using System;
using System.Collections.Generic;
using WeChatWASM;
using System.Linq;

namespace GDK
{
	public class DataAnalyze : DataAnalyzeBase
	{
		public override void ReportEvent<T>(string eventId, T data)
		{
			var dict = new Dictionary<string, string>();
			var fields = data.GetType()
				.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
			foreach (var field in fields)
			{
				var value = field.GetValue(data);
				if (value is string str)
				{
					dict[field.Name] = str;
				}
				else
				{
					dict[field.Name] = $"{value}";
				}
			}

			var ss = string.Join(",", dict.Select((item) => $"{item.Key}=\"{item.Value}\""));
			UnityEngine.Debug.Log($"[埋点] eventId={eventId}, data={{{ss}}}");

			WX.ReportEvent(eventId, dict);
		}

		public override void ReportEvent(string eventId, Dictionary<string, string> dict)
		{
			var ss = string.Join(",", dict.Select((item) => $"{item.Key}=\"{item.Value}\""));
			UnityEngine.Debug.Log($"[埋点] eventId={eventId}, data={{{ss}}}");

			WX.ReportEvent(eventId, dict);
		}
	}
}