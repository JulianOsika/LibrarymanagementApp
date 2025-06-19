using LibraryManagement.Presentation.WebAPI.Controllers;
using LibraryManagement.Presentation.WebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Controllers
{
    public class LoginControllerTests
    {
        [Fact]
        public void Login_ReturnsOk_WhenCredentialsAreValid()
        {
            var controller = new LoginController();

            var validCredentials = new LoginDto
            {
                Username = "user",
                Password = "user123"
            };

            var result = controller.Login(validCredentials);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void Login_ReturnsUnauthorized_WhenCredentialsInvalid()
        {
            var controller = new LoginController();

            var badCredentials = new LoginDto
            {
                Username = "invalid",
                Password = "wrong"
            };

            var result = controller.Login(badCredentials);

            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
