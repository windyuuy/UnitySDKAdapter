using System;

namespace GDK
{
	public class EventProxy<T, TDelegate>
	{
		private Func<Action<T>, TDelegate> AddCallbackHandler;
		private Action<TDelegate> RemoveCallbackHandler;

		private Action<T> Callback;
		private TDelegate CleanId;

		private void EmitCallback(T obj)
		{
			Callback?.Invoke(obj);
		}

		public EventProxy(Func<Action<T>, TDelegate> addCallbackHandler,
			Action<TDelegate> removeCallbackHandler)
		{
			AddCallbackHandler = addCallbackHandler;
			RemoveCallbackHandler = removeCallbackHandler;
		}

		public void Add(Action<T> callback)
		{
			if (Callback == null)
			{
				CleanId = AddCallbackHandler(EmitCallback);
			}

			Callback += callback;
		}

		public void Remove(Action<T> callback)
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