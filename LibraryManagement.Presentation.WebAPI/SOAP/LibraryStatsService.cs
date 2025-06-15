using LibraryManagement.Infrastructure.Persistence;

namespace LibraryManagement.Presentation.WebAPI.SOAP
{
    public class LibraryStatsService : ILibraryStatsService
    {
        private readonly LibraryDbContext _context;

        public LibraryStatsService(LibraryDbContext context)
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
