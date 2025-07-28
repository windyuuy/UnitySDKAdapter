
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GDK
{
	public class DataAnalyzeBase : IDataAnalyze
	{
		public virtual IModuleMap Api { get; set; }

		public virtual void Init()
		{
		}

		public virtual Task InitWithConfig(GDKConfigV2 info)
		{
			return Task.CompletedTask;
		}

		public virtual void ReportEvent<T>(string eventId, T data)
		{
			UnityEngine.Debug.Log($"[埋点] eventId={eventId}, data={UnityEngine.JsonUtility.ToJson(data)}");
		}
		public virtual void ReportEvent(string eventId, Dictionary<string, string> data)
		{
			var ss = string.Join(",", data.Select((item) => $"{item.Key}=\"{item.Value}\""));
			UnityEngine.Debug.Log($"[埋点] eventId={eventId}, data={ss}");
		}
	}
}
