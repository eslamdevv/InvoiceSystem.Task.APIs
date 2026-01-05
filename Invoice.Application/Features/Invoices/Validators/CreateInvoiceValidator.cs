using FluentValidation;
using InvoiceSystem.Application.Features.Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Application.Features.Invoices.Validators
{
    public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceRequest>
    {
        public CreateInvoiceValidator()
        {
            RuleFor(x => x.InvoiceNumber).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Date).NotEmpty();

            RuleFor(x => x.Items).NotNull().Must(x => x.Count > 0)
                .WithMessage("Invoice must at least 1 item.");

            RuleForEach(x => x.Items).SetValidator(new CreateInvoiceItemValidator());
        }
    }

    public class CreateInvoiceItemValidator : AbstractValidator<CreateInvoiceItemRequest>
    {
        public CreateInvoiceItemValidator()
        {
            RuleFor(x => x.ItemCode).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Qty).GreaterThan(0);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
