using InvoiceSystem.Core;
using InvoiceSystem.Core.Repositories.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Application.Features.Invoices.Commands
{
    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, bool>
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IUnitOfWork _uow;

        public DeleteInvoiceCommandHandler(IInvoiceRepository invoiceRepo, IUnitOfWork uow)
        {
            _invoiceRepo = invoiceRepo;
            _uow = uow;
        }

        public async Task<bool> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(request.id, cancellationToken);

            if (invoice is null) return false;

            _invoiceRepo.Delete(invoice);
            await _uow.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
