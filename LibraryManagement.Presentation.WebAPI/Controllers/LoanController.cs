using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Presentation.WebAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public LoanController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Loan>> GetAllLoans()
        {
            return _context.Loans
                .Include(l => l.Reader)
                .Include(l => l.Book)
                .ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Loan> GetLoan(int id)
        {
            var loan = _context.Loans
                .Include (l => l.Reader)
                .Include(l => l.Book)
                .FirstOrDefault(l =>  l.Id == id);

            return loan == null ? NotFound() : Ok(loan);
        }

        [HttpPost]
        public IActionResult PostLoan(CreateLoanDto loanDto)
        {
            var loan = new Loan
            {
                ReaderId = loanDto.ReaderId,
                BookId = loanDto.BookId,
                LoanDate = loanDto.LoanDate,
                ReturnDate = null
            };

            _context.Loans.Add(loan);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetLoan), new { id = loan.Id }, loan);
        }

        [HttpPut("{id}")]
        public IActionResult PutLoan(int id, UpdateLoanDto loanDto)
        {
            var loan = _context.Loans.FirstOrDefault(l => l.Id == id);

            if (loan == null)
                return NotFound();

            loan.ReaderId = loanDto.ReaderId;
            loan.BookId = loanDto.BookId;
            loan.LoanDate = loanDto.LoanDate;
            loan.ReturnDate = loanDto.ReturnDate;

            _context.SaveChanges();
            return Ok(loan);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLoan(int id)
        {
            var loan = _context.Loans.FirstOrDefault(l => l.Id == id);

            if(loan == null)
                return NotFound();

            _context.Loans.Remove(loan);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("return/{id}")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
                return NotFound();

            if (loan.ReturnDate != null)
                return BadRequest("Książka już została zwrócona.");

            loan.ReturnDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
