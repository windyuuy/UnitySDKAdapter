
namespace GDK
{

    public interface IDataAnalyze : IModule
    {
        public void ReportEvent<T>(string eventId, T data);
    }    
}
