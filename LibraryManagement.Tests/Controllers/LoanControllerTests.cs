using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Presentation.WebAPI.Controllers;
using LibraryManagement.Presentation.WebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Controllers
{
    public class LoanControllerTests
    {
        [Fact]
        public async Task ReturnBook_SetsReturnDate_WhenLoanExistsAndNotReturned()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new LibraryDbContext(options);

            var loan = new Loan
            {
                Id = 1,
                BookId = 1,
                ReaderId = 1,
                LoanDate = DateTime.UtcNow,
                ReturnDate = null
            };

            context.Loans.Add(loan);
            context.SaveChanges();

            var controller = new LoanController(context);

            var result = await controller.ReturnBook(1);

            var updatedLoan = await context.Loans.FirstAsync(l => l.Id == 1);
            Assert.NotNull(updatedLoan.ReturnDate);
        }

        [Fact]
        public void PostLoan_ReturnsBadRequest_WhenBookAlreadyLoaned()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new LibraryDbContext(options);

            context.Books.Add(new Book
            {
                Id = 10,
                Title = "Test Book",
                Description = "description",
                AuthorName = "Jan",
                AuthorSurname = "Kowalski"
            });

            context.Loans.Add(new Loan
            {
                BookId = 10,
                ReaderId = 1,
                LoanDate = DateTime.Now,
                ReturnDate = null
            });

            context.SaveChanges();

            var controller = new LoanController(context);

            var dto = new CreateLoanDto
            {
                BookId = 10,
                ReaderId = 2,
                LoanDate = DateTime.Now
            };

            var result = controller.PostLoan(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }


    }
}
