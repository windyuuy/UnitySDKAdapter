
using System.Collections.Generic;

namespace GDK
{

    public interface IDataAnalyze : IModule
    {
        public void ReportEvent<T>(string eventId, T data);
        public void ReportEvent(string eventId, Dictionary<string, string> data);
    }
}
