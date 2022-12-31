using LibraryManagement.Business.Contracts;
using LibraryManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/books")]
    public class BookController : BaseController
    {
        private readonly IBookBusinessService _businessService;
        public BookController(ILogger logger,
            IBookBusinessService bookBusinessService) : base(logger)
        {
            _businessService = bookBusinessService ?? throw new ArgumentNullException(nameof(bookBusinessService));

        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddAsync([FromBody, Required] BookDomainModel bookDomainModel)
        {
            return Accepted(await _businessService
                .AddAsync(bookDomainModel).ConfigureAwait(false));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _businessService.GetAllAsync()
                .ConfigureAwait(false));
        }

        [HttpGet]
        [Route("{resourceId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute, Required] Guid resourceId)
        {
            return Ok(await _businessService.GetByIdAsync(resourceId).ConfigureAwait(false));
        }
    }
}
