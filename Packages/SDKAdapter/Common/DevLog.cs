using Lang.Loggers;

namespace GDK
{
	public class DevLog
	{
		// public static Logger Instance = new Logger().AppendTag("wdk");
		public static DevLog Instance = new DevLog();

		/**
		 * log通道打印日志，并储至日志文件
		 * @param args
		 */
		public void Log(object args)
		{
			UnityEngine.Debug.Log(args);
		}

		/**
		 * 将消息打印到控制台，不存储至日志文件
		 */
		public void Debug(object args)
		{
			UnityEngine.Debug.Log(args);
		}

		/**
		 * 将消息打印到控制台，不存储至日志文件
		 */
		public void Info(object args)
		{
			UnityEngine.Debug.Log(args);
		}

		/**
		 * 将消息打印到控制台，并储至日志文件
		 */
		public void Warn(object args)
		{
			UnityEngine.Debug.LogWarning(args);
		}

		/**
		 * 将消息打印到控制台，并储至日志文件
		 */
		public void Error(object args)
		{
			UnityEngine.Debug.LogError(args);
		}
	}
}