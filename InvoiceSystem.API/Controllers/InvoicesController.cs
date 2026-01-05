using InvoiceSystem.API.ErrorsResponse;
using InvoiceSystem.Application.DTOs;
using InvoiceSystem.Application.Features.Invoices.Commands;
using InvoiceSystem.Application.Features.Invoices.Models;
using InvoiceSystem.Application.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        [EndpointSummary("Get All Invoices")]
        [ProducesResponseType(typeof(IReadOnlyList<InvoiceDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<InvoiceDTO>>> GetAll(CancellationToken ct)
            => Ok(await _mediator.Send(new GetInvoicesQuery(), ct));

        [HttpGet("{id:int}")]
        [EndpointSummary("Get Invoice By Id")]
        [ProducesResponseType(typeof(InvoiceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<InvoiceDTO>> GetById(int id, CancellationToken ct)
        {
            var invoice = await _mediator.Send(new GetInvoiceByIdQuery(id), ct);
            return invoice is null ? NotFound(new ApiResponse(StatusCodes.Status404NotFound)) : Ok(invoice);
        }

        [HttpPost]
        [EndpointSummary("Create New Invoice")]
        [ProducesResponseType(typeof(InvoiceDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult<InvoiceDTO>> Create([FromBody] CreateInvoiceRequest request, CancellationToken ct)
        {
            var id = await _mediator.Send(new CreateInvoiceCommand(request), ct);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Update Invoice")]
        [ProducesResponseType(typeof(Ok), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] UpdateInvoiceRequest request, CancellationToken ct)
        {
            var ok = await _mediator.Send(new UpdateInvoiceCommand(id, request), ct);
            return ok ?
                Ok(new
                {
                    statuscode = StatusCodes.Status200OK,
                    message = "Invoice Updated!"
                }) :
                NotFound(new ApiResponse(StatusCodes.Status404NotFound));
        }

        [HttpDelete("{id:int}")]
        [EndpointSummary("Delete Invoice")]
        [ProducesResponseType(typeof(Ok), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Delete(int id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteInvoiceCommand(id), ct);
            return ok ?
               Ok(new
               {
                   statuscode = StatusCodes.Status200OK,
                   message = "Invoice Deleted!"
               }) :
               NotFound(new ApiResponse(StatusCodes.Status404NotFound));
        }
    }
}
