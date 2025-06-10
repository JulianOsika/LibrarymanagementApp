using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public ReaderController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Reader>> GetAllReaders()
        {
            return _context.Readers.ToList();
        }
    }
}
