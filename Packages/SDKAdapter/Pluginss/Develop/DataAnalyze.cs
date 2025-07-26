
using System;
using System.Collections.Generic;
using System.Linq;

namespace GDK
{
	public class DataAnalyze : DataAnalyzeBase
	{
		public override void ReportEvent<T>(string eventId, T data)
		{
			UnityEngine.Debug.Log($"[埋点] eventId={eventId}, data={UnityEngine.JsonUtility.ToJson(data)}");
		}
		public override void ReportEvent(string eventId, Dictionary<string, string> data)
		{
			var ss = string.Join(",", data.Select((key, value) => $"{key}=\"{value}\""));
			UnityEngine.Debug.Log($"[埋点] eventId={eventId}, data={ss}");
		}
	}
}
