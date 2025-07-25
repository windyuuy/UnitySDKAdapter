
using System.Collections.Generic;

namespace GDK
{

    public interface IDataAnalyze : IModule
    {
        public void ReportEvent<T>(string eventId, Dictionary<string, string> data);
    }
}
