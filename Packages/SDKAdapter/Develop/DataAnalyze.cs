
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using WeChatWASM;

namespace GDK
{
	public class DataAnalyze : DataAnalyzeBase
	{
		public override void ReportEvent<T>(string eventId, Dictionary<string, string> data)
		{
			var ss = string.Join(",", data.Select((key, value) => $"{key}=\"{value}\""));
			UnityEngine.Debug.Log($"[埋点] eventId={eventId}, data={ss}");
		}
	}
}
