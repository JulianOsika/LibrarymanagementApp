using LibraryManagement.Presentation.SoapService.DataContract;
using System.ServiceModel;

namespace LibraryManagement.Presentation.SoapService.ServiceContract
{
    [ServiceContract]
    public interface ILibraryService
    {
        [OperationContract]
        LibraryStats GetLibraryStats();
    }
}
