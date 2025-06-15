using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BookController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            return book == null ? NotFound() : book;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult PostBook (Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Books.Add(book);
            _context.SaveChanges();

            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult PutBook(int id, Book newBook)
        {
            var oldBook = _context.Books.FirstOrDefault(x => x.Id == id);

            if (oldBook == null)
                return NotFound();

            oldBook.Title= newBook.Title;
            oldBook.Description= newBook.Description;
            oldBook.AuthorName= newBook.AuthorName;
            oldBook.AuthorSurname= newBook.AuthorSurname;

            _context.Books.Update(oldBook);
            _context.SaveChanges();

            return Ok(oldBook);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);

            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableBooks()
        {
            var books = await _context.Books
                .Include(b => b.Loans)
                .Where(b => !b.Loans.Any(l => l.ReturnDate == null))
                .ToListAsync();

            return Ok(books);
        }
    }
}
