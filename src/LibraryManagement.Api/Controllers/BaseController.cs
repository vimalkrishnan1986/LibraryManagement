using LibraryManagement.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [CustomExceptionFilter]
    public class BaseController : ControllerBase
    {
        protected readonly ILogger _logger;
        protected BaseController(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
