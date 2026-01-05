using FluentValidation;
using InvoiceSystem.Application.Features.Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Application.Features.Invoices.Validators
{
    public class UpdateInvoiceValidator : AbstractValidator<UpdateInvoiceRequest>
    {
        public UpdateInvoiceValidator()
        {
            RuleFor(x => x.InvoiceNumber).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Date).NotEmpty();

            RuleFor(x => x.Items).NotNull().Must(x => x.Count > 0)
                .WithMessage("Invoice must have at least 1 item.");

            RuleForEach(x => x.Items).SetValidator(new UpdateInvoiceItemValidator());
        }
    }
    public class UpdateInvoiceItemValidator : AbstractValidator<UpdateInvoiceItemRequest>
    {
        public UpdateInvoiceItemValidator()
        {
            RuleFor(x => x.ItemCode).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Qty).GreaterThan(0);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
