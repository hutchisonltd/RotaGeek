using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RotaGeek.Core.Entities;
using RotaGeek.Core.Interfaces;
using RotaGeek.Infrastructure.Data;
using RotaGeek.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RotaGeek.ContactControllerTest
{
    public class ContactControllerTest
    {
        IRepository _service;
        AppDbContext _context;

        private readonly Mock<ILogger<ContactController>> _mockLogger;

        public ContactControllerTest()
        {
            _mockLogger = new Mock<ILogger<ContactController>>();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=NOTEBOOKCHRIS;Initial Catalog=RotaGeek;Persist Security Info=True;User ID=rotageek;Password=R0taG33k1!;");
            _context = new AppDbContext(optionsBuilder.Options);
            _service = new EFRepository(_context);
        }

        [Fact]
        public async Task Get()
        {
            CancellationToken ct = new CancellationToken();

            //ACT
            var controller = new ContactController(_service, _mockLogger.Object);
            IActionResult result = await controller.List(ct);
            OkObjectResult okResult = result as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            // tests
            var actualConfiguration = okResult.Value as List<Contact>;


        }
    }
}
