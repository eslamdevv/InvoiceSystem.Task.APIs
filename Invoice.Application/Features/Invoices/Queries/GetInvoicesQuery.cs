using InvoiceSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Application.Features.Invoices.Queries
{
    public record GetInvoicesQuery() : IRequest<List<InvoiceDTO>>;
}
