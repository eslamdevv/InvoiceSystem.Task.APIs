using InvoiceSystem.Core;
using InvoiceSystem.Core.Entities;
using InvoiceSystem.Core.Repositories.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Application.Features.Invoices.Commands
{
    internal class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IUnitOfWork _uow;

        public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepo, IUnitOfWork uow)
        {
            _invoiceRepo = invoiceRepo;
            _uow = uow;
        }

        public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var r = request.Request;

            var invoice = new Invoice
            {
                InvoiceNumber = r.InvoiceNumber,
                Date = r.Date,
                Items = r.Items.Select(x => new InvoiceItem
                {
                    ItemCode = x.ItemCode,
                    Qty = x.Qty,
                    Price = x.Price
                }).ToList()
            };

            await _invoiceRepo.AddAsync(invoice, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return invoice.Id;
        }
    }
}
