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
    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, bool>
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IUnitOfWork _uow;

        public UpdateInvoiceCommandHandler(IInvoiceRepository invoiceRepo, IUnitOfWork uow)
        {
            _invoiceRepo = invoiceRepo;
            _uow = uow;
        }

        public async Task<bool> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(request.id, cancellationToken);

            if (invoice is null) return false;

            invoice.InvoiceNumber = request.Request.InvoiceNumber;
            invoice.Date = request.Request.Date;

            var incoming = request.Request.Items;

            //Update + Add
            foreach (var itemRequest in incoming)
            {
                if (itemRequest.Id.HasValue)
                {
                    var existing = invoice.Items.FirstOrDefault(x => x.Id == itemRequest.Id.Value);
                    if (existing is null) continue;

                    existing.ItemCode = itemRequest.ItemCode;
                    existing.Qty = itemRequest.Qty;
                    existing.Price = itemRequest.Price;

                }
                else
                {
                    invoice.Items.Add(new Core.Entities.InvoiceItem
                    {
                        ItemCode = itemRequest.ItemCode,
                        Qty = itemRequest.Qty,
                        Price = itemRequest.Price
                    });
                }
            }

            //Remove Deleted
            var incomingIds = incoming.Where(x => x.Id.HasValue).Select(x => x.Id!.Value).ToHashSet();
            var toRemove = invoice.Items.Where(x => x.Id !=0 && !incomingIds.Contains(x.Id)).ToList();
            foreach (var item in toRemove)
                invoice.Items.Remove(item);

            _invoiceRepo.Update(invoice);
            await _uow.SaveChangesAsync(cancellationToken);
            return true;

        }
    }
}
