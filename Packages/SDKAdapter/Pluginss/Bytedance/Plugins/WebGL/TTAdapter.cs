#if SUPPORT_BYTEDANCE
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

namespace BytedanceGDK
{
	internal class TTAdapter
	{
		[DllImport("__Internal")]
		public static extern void GDK_Bytedance_ReportEvent(string eventid, string data);

		[DllImport("__Internal")]
		public static extern void GDK_Bytedance_ShowLoading(long sessionId, string optionsJson);

		[DllImport("__Internal")]
		public static extern void GDK_Bytedance_HideLoading(long sessionId);

		[DllImport("__Internal")]
		public static extern void GDK_Bytedance_ShowModal(long sessionId, string optionsJson);

		[DllImport("__Internal")]
		public static extern void GDK_Bytedance_ShowToast(long sessionId, string optionsJson);

		[DllImport("__Internal")]
		public static extern void GDK_Bytedance_HideToast(long sessionId);

		[DllImport("__Internal")]
		public static extern string GDK_Bytedance_GetUserDataPath();

		public static void SendRequest<TResp>(Action<long, string> call, object obj, Action<TResp> callback)
		{
			var sessionId = JsCallbackListener.Shared.ListenCallback<TResp>(callback);
			var paraStr = JsonUtility.ToJson(obj);
			call(sessionId, paraStr);
		}

		public static Task<TResp> SendRequestAsync<TResp>(Action<long, string> call, object obj)
		{
			var ts = new TaskCompletionSource<TResp>();
			var sessionId = JsCallbackListener.Shared.ListenCallback<TResp>((resp) => { ts.SetResult(resp); });
			var paraStr = JsonUtility.ToJson(obj);
			call(sessionId, paraStr);
			return ts.Task;
		}

		public static void SendRequest<TResp>(Action<long> call, Action<TResp> callback)
		{
			var sessionId = JsCallbackListener.Shared.ListenCallback<TResp>(callback);
			call(sessionId);
		}

		public static Task<TResp> SendRequestAsync<TResp>(Action<long> call)
		{
			var ts = new TaskCompletionSource<TResp>();
			var sessionId = JsCallbackListener.Shared.ListenCallback<TResp>((resp) => { ts.SetResult(resp); });
			call(sessionId);
			return ts.Task;
		}
	}
}
#endif