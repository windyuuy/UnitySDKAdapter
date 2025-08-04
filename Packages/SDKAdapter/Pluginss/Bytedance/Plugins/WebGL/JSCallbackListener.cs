using System;
using System.Collections.Generic;
using AOT;
using UnityEngine;

namespace BytedanceGDK
{
	public class JsCallbackListener : MonoBehaviour
	{
		[Serializable]
		internal class CallbackInfo
		{
			public long sessionId;
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

				var gameObject = new GameObject("JsCallbackListener");
				GameObject.DontDestroyOnLoad(gameObject);
				_shared = gameObject.AddComponent<JsCallbackListener>();
				if (_shared == null)
				{
					Debug.LogError("cannot create JsCallbackListener");
				}

				return _shared;
			}
		}

		private long _callIdAcc = 0;

		public long GetCallIdAcc()
		{
			return ++_callIdAcc;
		}

		protected readonly Dictionary<long, Action<string>> CallbackMap = new();

		public long ListenCallback<T>(Action<T> callback)
		{
			var sessionId = GetCallIdAcc();
			Debug.Log($"ListenCallback: {sessionId}");
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
			Debug.Log($"ReceiveCallback: {sessionId}, {jsonValue}");
			if (CallbackMap.Remove(sessionId, out var callback))
			{
				Debug.Log($"ReceiveCallback-ToCall: {sessionId}, {jsonValue}");
				callback?.Invoke(jsonValue);
			}
			else
			{
				Debug.LogError($"CallId not Registed: {sessionId}, {jsonValue}");
			}
		}
	}
}