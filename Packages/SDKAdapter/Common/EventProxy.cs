using System;

namespace GDK
{
	public class EventProxy<TTargetType, TSourceAction>
	{
		private Func<Action<TTargetType>, TSourceAction> AddCallbackHandler;
		private Action<TSourceAction> RemoveCallbackHandler;

		private Action<TTargetType> Callback;
		private TSourceAction CleanId;

		private void EmitCallback(TTargetType obj)
		{
			Callback?.Invoke(obj);
		}

		public EventProxy(Func<Action<TTargetType>, TSourceAction> addCallbackHandler,
			Action<TSourceAction> removeCallbackHandler)
		{
			AddCallbackHandler = addCallbackHandler;
			RemoveCallbackHandler = removeCallbackHandler;
		}

		public void Add(Action<TTargetType> callback)
		{
			if (Callback == null)
			{
				CleanId = AddCallbackHandler(EmitCallback);
			}

			Callback += callback;
		}

		public void Remove(Action<TTargetType> callback)
		{
			Callback -= callback;
			if (Callback == null)
			{
				RemoveCallbackHandler(CleanId);
				CleanId = default;
			}
		}
	}
}