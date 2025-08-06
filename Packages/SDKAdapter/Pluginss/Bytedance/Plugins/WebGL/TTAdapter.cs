#if SUPPORT_BYTEDANCE
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using GDK;
using UnityEngine;

namespace BytedanceGDK
{
	internal class TTAdapter
	{
		[DllImport("__Internal")]
		public static extern void GDK_Bytedance_ReportEvent(string eventid, string data);

		[DllImport("__Internal")]
		public static extern void GDK_Bytedance_ShowLoading(int sessionId, string optionsJson);

		[DllImport("__Internal")]
		public static extern void GDK_Bytedance_HideLoading(int sessionId);

		[DllImport("__Internal")]
		public static extern void GDK_Bytedance_ShowModal(int i, string optionsJson);

		[DllImport("__Internal")]
		public static extern void GDK_Bytedance_ShowToast(int sessionId, string optionsJson);

		[DllImport("__Internal")]
		public static extern void GDK_Bytedance_HideToast(int sessionId);

		[DllImport("__Internal")]
		public static extern string GDK_Bytedance_GetUserDataPath();

		[DllImport("__Internal")]
		private static extern void GDK_Bytedance_Openlink(string openlink);

		public static void SendRequest<TResp>(Action<int, string> call, object obj, Action<TResp> callback)
		{
			var sessionId = JsCallbackListener.Shared.ListenCallback<TResp>(callback);
			var paraStr = JsonUtility.ToJson(obj);
			call(sessionId, paraStr);
		}

		public static Task<TResp> SendJsRequestAsync<TReq, TResp>(Action<int, string> call, TReq obj)
		{
			var ts = new TaskCompletionSource<TResp>();
			var sessionId = JsCallbackListener.Shared.ListenCallback<TResp>((resp) => { ts.SetResult(resp); });
			var paraStr = JsonUtility.ToJson(obj);
			DevLog.Instance.Log($"SendJsRequestAsync: {paraStr}");
			call(sessionId, paraStr);
			return ts.Task;
		}

		public static void SendRequest<TResp>(Action<int> call, Action<TResp> callback)
		{
			var sessionId = JsCallbackListener.Shared.ListenCallback<TResp>(callback);
			call(sessionId);
		}

		public static Task<TResp> SendJsRequestAsync<TResp>(Action<int> call)
		{
			var ts = new TaskCompletionSource<TResp>();
			var sessionId = JsCallbackListener.Shared.ListenCallback<TResp>((resp) => { ts.SetResult(resp); });
			call(sessionId);
			return ts.Task;
		}
	}
}
#endif