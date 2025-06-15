using System.Runtime.Serialization;

namespace LibraryManagement.Presentation.WebAPI.SOAP
{
    [DataContract]
    public class LibraryStats
    {
        [DataMember]
        public int BooksCount { get; set; }

        [DataMember]
        public int ReadersCount { get; set; }

        [DataMember]
        public int ActiveLoansCount { get; set; }
    }
}
