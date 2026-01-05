using InvoiceSystem.Application.Features.Invoices.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Application.Features.Invoices.Commands
{
    public record CreateInvoiceCommand(CreateInvoiceRequest Request) : IRequest<int>;
}
