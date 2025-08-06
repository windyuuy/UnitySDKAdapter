#if SUPPORT_BYTEDANCE
using System;
using System.Collections.Generic;
using AOT;
using UnityEngine;
using GDK;

namespace BytedanceGDK
{
	public class JsCallbackListener : MonoBehaviour
	{
		[Serializable]
		internal class CallbackInfo
		{
			public int sessionId;
		}

		private static JsCallbackListener _shared;

		public static JsCallbackListener Shared
		{
			get
			{
				if (_shared != null)
				{
					return _shared;
				}

				var gameObject = new GameObject("BytedanceGDK_JsCallbackListener");
				GameObject.DontDestroyOnLoad(gameObject);
				_shared = gameObject.AddComponent<JsCallbackListener>();
				if (_shared == null)
				{
					DevLog.Instance.Error("cannot create BytedanceGDK_JsCallbackListener");
				}

				return _shared;
			}
		}

		private int _callIdAcc = 0;

		public int GetCallIdAcc()
		{
			return ++_callIdAcc;
		}

		protected readonly Dictionary<int, Action<string>> CallbackMap = new();

		public int ListenCallback<T>(Action<T> callback)
		{
			var sessionId = GetCallIdAcc();
			DevLog.Instance.Log($"ListenCallback: {sessionId}");
			CallbackMap.Add(sessionId, (jsonStr) =>
			{
				try
				{
					var obj = JsonUtility.FromJson<T>(jsonStr);
					callback?.Invoke(obj);
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			});
			return sessionId;
		}

		[MonoPInvokeCallback(typeof(Action<string>))]
		public void ReceiveCallback(string jsonValue)
		{
			var info = JsonUtility.FromJson<CallbackInfo>(jsonValue);
			var sessionId = info.sessionId;
			DevLog.Instance.Log($"JsCallbackListener::ReceiveCallback: {sessionId}, {jsonValue}");
			if (CallbackMap.Remove(sessionId, out var callback))
			{
				DevLog.Instance.Log($"ReceiveCallback-ToCall: {sessionId}, {jsonValue}");
				callback?.Invoke(jsonValue);
			}
			else
			{
				DevLog.Instance.Error($"CallId not Registed: {sessionId}, {jsonValue}");
			}
		}
	}
}
#endif
