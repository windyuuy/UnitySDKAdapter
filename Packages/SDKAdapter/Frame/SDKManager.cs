using System.Threading.Tasks;

namespace GDK
{
	public abstract class SDKManager
	{
		public enum SDKManagerInitState
		{
			Uninit = 0,
			Initing = 1,
			Inited = 2,
		}

		public static SDKManager Instance { get; private set; }

		private SDKManagerInitState _initState = SDKManagerInitState.Uninit;

		public static void SetSDKManager<T>() where T : SDKManager, new()
		{
			if (Instance == null)
			{
				Instance = new T();
			}
		}

		public virtual async Task<int> Init()
		{
			if (_initState > 0)
			{
				return 0;
			}

			_initState = SDKManagerInitState.Initing;

			await OnInit();
			_initState = SDKManagerInitState.Inited;
			return 0;
		}

		protected abstract Task<int> OnInit();

		public virtual bool IsInited()
		{
			return _initState == SDKManagerInitState.Inited;
		}

		public virtual bool IsIniting()
		{
			return _initState == SDKManagerInitState.Initing;
		}

		public virtual bool IsInitingOrInited()
		{
			return _initState > 0;
		}
	}
}