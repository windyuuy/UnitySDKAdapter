
using System;
using System.Threading.Tasks;
using UnityEngine;
using WeChatWASM;

namespace GDK
{
	public class DataAnalyze : DataAnalyzeBase
	{
		public override void ReportEvent<T>(string eventId, T data)
		{
			UnityEngine.Debug.Log($"eventId={eventId}, data={JsonUtility.ToJson(data)}");
		}
	}
}
