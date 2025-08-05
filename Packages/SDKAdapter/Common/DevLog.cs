using Lang.Loggers;

namespace GDK
{
	public static class DevLog
	{
		public static Logger Instance = new Logger().AppendTag("wdk");
	}
}