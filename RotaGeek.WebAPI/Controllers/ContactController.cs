using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RotaGeek.Core.Entities;
using RotaGeek.Core.Interfaces;

namespace RotaGeek.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IRepository repository, ILogger<ContactController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Produces(typeof(List<Contact>))]
        public async Task<IActionResult> List(CancellationToken ct = default(CancellationToken))
        {
            _logger.LogInformation("api/contact Executing..");

            try
            {
                if (ct.IsCancellationRequested)
                    return StatusCode(500, "");

                var items = await _repository.List<Contact>(ct);

                _logger.LogInformation("api/contact Complete..");

                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("api/contact Failed..");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(Contact))]
        public async Task<IActionResult> Get(int id, CancellationToken ct = default(CancellationToken))
        {
            _logger.LogInformation("api/contact/Get Executing..");

            try
            {
                if (ct.IsCancellationRequested)
                    return StatusCode(500, "");

                var items = await _repository.Get<Contact>(id, ct);

                _logger.LogInformation("api/contact/Get Complete..");

                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("api/contact/Get Failed..");
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult Post(string name, string emailAddress, string message)
        {
            _logger.LogInformation("api/contact/Post Executing..");

            Contact _newContact = new Contact();
            _newContact.EmailAddress = emailAddress;
            _newContact.Name = name;
            _newContact.Id = 0;
            _newContact.EnteredDate = DateTime.Now;
            _newContact.Message = message;

            try
            {
                var items = _repository.Add<Contact>(_newContact);

                _logger.LogInformation("api/contact/Post Complete..");

                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("api/contact/Post Failed..");
                return StatusCode(500, ex);
            }
        }
    }
}