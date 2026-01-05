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
    public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, List<InvoiceDTO>>
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IMapper _mapper;

        public GetInvoicesQueryHandler(IInvoiceRepository invoiceRepo, IMapper mapper)
        {
            _invoiceRepo = invoiceRepo;
            _mapper = mapper;
        }

        public async Task<List<InvoiceDTO>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceRepo.GetAllAsync(cancellationToken);
            return _mapper.Map<List<InvoiceDTO>>(invoices);
        }
    }
}
