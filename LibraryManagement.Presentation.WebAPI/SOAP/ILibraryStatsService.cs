using System.ServiceModel;

namespace LibraryManagement.Presentation.WebAPI.SOAP
{
    [ServiceContract]
    public interface ILibraryStatsService
    {
        [OperationContract]
        public LibraryStats GetLibraryStats();
    }
}
