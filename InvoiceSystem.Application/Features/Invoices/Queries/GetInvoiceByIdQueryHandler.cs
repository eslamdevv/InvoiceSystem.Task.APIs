using AutoMapper;
using InvoiceSystem.Application.DTOs;
using InvoiceSystem.Core.Repositories.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Application.Features.Invoices.Queries
{
    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDTO?>
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IMapper _mapper;

        public GetInvoiceByIdQueryHandler(IInvoiceRepository invoiceRepo, IMapper mapper)
        {
            _invoiceRepo = invoiceRepo;
            _mapper = mapper;
        }

        public async Task<InvoiceDTO?> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(request.id, cancellationToken);
            return invoice is null ? null : _mapper.Map<InvoiceDTO>(invoice);
        }
    }
}
