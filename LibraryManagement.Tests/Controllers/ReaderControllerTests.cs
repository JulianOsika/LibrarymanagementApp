using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Presentation.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Controllers
{
    public class ReaderControllerTests
    {
        [Fact]
        public void DeleteReader_ReturnsNotFound_WhenReaderDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new LibraryDbContext(options);
            var controller = new ReaderController(context);

            var result = controller.DeleteReader(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
