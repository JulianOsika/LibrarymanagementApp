using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Presentation.SoapService.DataContract;

namespace LibraryManagement.Presentation.SoapService.ServiceContract
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryDbContext _context;

        public LibraryService(LibraryDbContext context)
        {
            _context = context;
        }

        public LibraryStats GetLibraryStats()
        {
            return new LibraryStats
            {
                BooksCount = _context.Books.Count(),
                ReadersCount = _context.Readers.Count(),
                ActiveLoansCount = _context.Loans.Where(l => l.ReturnDate == null).Count()
            };
        }
    }
}
