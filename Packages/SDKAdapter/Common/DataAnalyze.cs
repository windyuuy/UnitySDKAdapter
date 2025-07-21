
using System;
using System.Threading.Tasks;

namespace GDK
{
	public abstract class DataAnalyzeBase : IDataAnalyze
	{
		public virtual IModuleMap Api { get; set; }

		public virtual void Init()
		{
		}

		public virtual Task InitWithConfig(GDKConfigV2 info)
		{
			return Task.CompletedTask;
		}

		public abstract void ReportEvent<T>(string eventId, T data);
    }
}
