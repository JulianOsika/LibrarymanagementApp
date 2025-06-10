using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

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

        [HttpGet("{id}")]
        public ActionResult<Reader> GetReader(int id)
        {
            var reader = _context.Readers.FirstOrDefault(x => x.Id == id);
            return reader == null ? NotFound() : reader;
        }

        [HttpPost]
        public IActionResult PostReader(Reader reader)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Readers.Add(reader);
            _context.SaveChanges();

            return Ok(reader);
        }

        [HttpPut("{id}")]
        public IActionResult PutReader(int id, Reader newReader)
        {
            var oldReader = _context.Readers.FirstOrDefault(r => r.Id == id);

            if(oldReader == null)
                return NotFound();

            oldReader.Name = newReader.Name;
            oldReader.Surname = newReader.Surname;
            oldReader.BirthDate = newReader.BirthDate;

            _context.Readers.Update(oldReader);
            _context.SaveChanges();

            return Ok(oldReader);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReader(int id)
        {
            var reader = _context.Readers.FirstOrDefault(r => r.Id == id);

            if(reader == null)
                return NotFound();

            _context.Readers.Remove(reader);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
