using InvoiceSystem.API.ErrorsResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.API.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int code)
        {
            var response = new ApiResponse(code);

            return new ObjectResult(response)
            {
                StatusCode = code
            };
        }
    }
}
